using IntegratorEHZ.App_Data;
using IntegratorEHZ.ModBusData;
using IntegratorEHZ.ModBusData.InternalProtocols;
using IntegratorEHZ.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;
using IntegratorEHZ.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;

namespace IntegratorEHZ.Server
{
    public delegate void ExecHandler(int id, byte[] request);
    public delegate bool CheckingTheConnection(int id);
    public interface IMediator
    {
        event ExecHandler ExecHandler;
        event CheckingTheConnection CheckingTheConnection;
        bool CheckingTheConnectionById(int id);
        void SendingRequest(int id, byte[] request);
    }

    /// <summary>
    /// Посредник для того чтобы отправлять запросы из контроллеров
    /// </summary>
    public class MediatorServ : IMediator
    {
        public event ExecHandler ExecHandler;
        public event CheckingTheConnection CheckingTheConnection;

        public bool CheckingTheConnectionById(int id)
        {
            if (this.CheckingTheConnection == null)
                return false;
            return this.CheckingTheConnection(id);
        }
        public void SendingRequest(int id, byte[] request)
        {
            if (this.ExecHandler == null)
                return;
            this.ExecHandler(id, request);
        }
    }
    
    public class EHZ : BackgroundService
    {
        private readonly ILogger<EHZ> _logger;
        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        private readonly IRepositoryProtocolData _repository;
        private readonly IHubContext<IntegratorHub> _hubContext;
        private IMediator _mediator;
        private EHZServ server;
        public EHZ(ILogger<EHZ> logger, IDbContextFactory<ApplicationDBContext> contextFactory, IRepositoryProtocolData repository, IMediator mediator, IHubContext<IntegratorHub> hubContext)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _repository = repository;
            _mediator = mediator;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int port = 5050;
            server = new EHZServ(IPAddress.Any, port, _logger,_contextFactory,_hubContext);
            server.Start();
            _ = _repository.ListenNotification();
            _mediator.ExecHandler += this.SendRequest;
            _mediator.CheckingTheConnection += this.CheckingTheConnection;
            _logger.LogInformation($"Server Start on {IPAddress.Any}:{port}");
            await Task.CompletedTask;
        }


        public void SendRequest(int id, byte[] request)
        {
            server.FindSession(id).HandlerForSendingASingleRequest(request);
        }
        public bool CheckingTheConnection(int id)
        {

            if (server.FindSession(id) != null)
            {
                return server.FindSession(id).IsConnected;
            }
            else
            {
                return false;
            }

        }
    }

    public class EHZServ : TcpServer
    {
        private readonly ILogger<EHZ> _logger;
        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        private readonly IHubContext<IntegratorHub> _hubContext;
        public EHZServ(IPAddress address, int port, ILogger<EHZ> logger, IDbContextFactory<ApplicationDBContext> contextFactory, IHubContext<IntegratorHub> hubContext) : base(address, port)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _hubContext = hubContext;
        }

        protected override TcpSession CreateSession()
        {
            return new EHZSession(this, _logger, _contextFactory,_hubContext);
        }
        protected override void OnError(SocketError error)
        {
            _logger.LogError($"TCP server caught an error with code {error}");
        }
        /// <summary>
        /// Использовать для просмотра всех подключенных устройств по id в базе данных 
        /// </summary>
        public void ShowSessionByID()
        {
            foreach (var item in SessionsDeviceIdList)
            {
                Console.WriteLine(item.Key);
            }
        }
    }

    public class EHZSession : TcpSession
    {
        private readonly ILogger<EHZ> _logger;
        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        private readonly IHubContext<IntegratorHub> _hubContext;
        private bool IsIdentified { get; set; }
        
        private MBData device;
        private System.Timers.Timer timer = new System.Timers.Timer();

        private EventWaitHandle ResponseDataDelay = new AutoResetEvent(false);
        private SurveyData _surveyData;
        private Channel<SurveyData> SurveyDeviceChannel;
                
        public EHZSession(TcpServer server, ILogger<EHZ> logger, IDbContextFactory<ApplicationDBContext> contextFactory, IHubContext<IntegratorHub> hubContext) : base(server)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _hubContext = hubContext;

            _surveyData = new SurveyData();
            SurveyDeviceChannel = Channel.CreateUnbounded<SurveyData>();
        }

        protected override void OnConnecting()
        {
            byte[] buffer = new byte[1024];
            long received = Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None, out SocketError ec);
            String receivedMsg = Encoding.UTF8.GetString(buffer, 0, (int)received);
            if (receivedMsg.Length == 15)
            {
                Identification(receivedMsg);
            }
            else if (receivedMsg.StartsWith("AT$IMEI"))
            {
                receivedMsg = receivedMsg.Substring(8, 15);
                Identification(receivedMsg);
            }
        }
        private void Identification(String identifier)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var deviceInTheList = context.Devices.SingleOrDefault(d => d.IMEI == identifier);
                if (deviceInTheList != null)
                {
                    IsIdentified = true;
                    ProtocolSelection(deviceInTheList.Internal_Protocol);
                    device.IdDevice = deviceInTheList.Id;
                    device.ImeiDevice = deviceInTheList.IMEI;
                    DeviceIdCurrentSession = deviceInTheList.Id;
                }
            }
        }
        private void ProtocolSelection(string internalProtocol)
        {
            switch (internalProtocol)
            {
                case "SKZ":
                    {
                        device = new SKZProtocol(_contextFactory);
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void OnConnected()
        {
            if (!IsIdentified)
                Disconnect();
            else
            {
                _logger.LogInformation($"Устройство с {Id} подключено. Id устройства {device.IdDevice}, Id potoka {Thread.CurrentThread.ManagedThreadId}");
                _hubContext.Clients.All.SendAsync("OnConnectionIndication",true);

                string datatime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string servAnwser = datatime + ",MOD=DAT";
                byte[] sent = Encoding.ASCII.GetBytes(servAnwser);
                SendAsync(sent);

                _ = ReadChannel();

                //Таймер для опроса 
                timer.Interval = 30000;
                _ = TestSurvayAsync();
                timer.Elapsed += SurvayDevice;
                timer.Enabled = true;
            }
        }
        /// <summary>
        /// Опрос устройства по таймеру 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SurvayDevice(object sender, ElapsedEventArgs e)
        {
            foreach (var request in device.GetListOfDevicePollingRequests())
            {
                //Console.WriteLine(BitConverter.ToString(request));
                _surveyData.Request = request;
                SendAsync(request);
                ReceiveAsync();
                ResponseDataDelay.WaitOne();
            }
        }
        /// <summary>
        /// Опрос устройства сразу при поключении
        /// </summary>
        /// <returns></returns>
        public async Task TestSurvayAsync() 
        { 
            await Task.Run(()=> { SurvayDevice(null, null); }); 
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string msg = BitConverter.ToString(buffer, (int)offset, (int)size);
            if (msg == "B5-BC-BD-BE-BF")
            {
                _logger.LogInformation($"Keep alive");
            }
            else
            {
                byte[] bufferForAnwser = new byte[(int)size];
                System.Buffer.BlockCopy(buffer, 0, bufferForAnwser, 0, (int)size);
                _logger.LogInformation($"Id Thread{Thread.CurrentThread.ManagedThreadId}: Msg from Device (OnReceived) {Id} = {msg}");
                _surveyData.Response = bufferForAnwser;
                SurveyDeviceChannel.Writer.TryWrite((SurveyData)_surveyData.Clone());
                ResponseDataDelay.Set();
            }
        }

        /// <summary>
        /// Асинхронный метод чтения из канала SurveyDeviceChannel
        /// </summary>
        /// <returns></returns>
        private async Task ReadChannel()
        {
            SurveyData BuffsureyData = new SurveyData();
            while (await SurveyDeviceChannel.Reader.WaitToReadAsync())
            {
                SurveyDeviceChannel.Reader.TryRead(out BuffsureyData);
                //BuffsureyData.PrintExchange();
                device.ResponseProcessing(BuffsureyData.Request, BuffsureyData.Response);
            }
        }

        protected override void OnDisconnected()
        {
            timer.Enabled = false;
            _logger.LogInformation($"Устройство с {Id} отключено.");
            _hubContext.Clients.All.SendAsync("OnConnectionIndication", false);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Tcp server Error {error}");
        }

        public override void HandlerForSendingASingleRequest(byte[] requestSend)
        {
            _surveyData.Request = requestSend;
            Console.WriteLine(BitConverter.ToString(requestSend));
            SendAsync(requestSend);
            ReceiveAsync();
            ResponseDataDelay.WaitOne();
        }
    }


   



}
