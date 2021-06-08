using IntegratorEHZ.App_Data;
using IntegratorEHZ.Hubs;
using IntegratorEHZ.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Repositorys
{
    public class RepositorySKZ : IRepositoryProtocolData
    {
        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        private IHubContext<IntegratorHub> _hubContext;
        private IMemoryCache _cache;
        private string connectionString="";

        public RepositorySKZ(IConfiguration configuration, IDbContextFactory<ApplicationDBContext> contextFactory, IHubContext<IntegratorHub> hubContext, IMemoryCache cache)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _contextFactory = contextFactory;
            _hubContext = hubContext;
            _cache = cache;
        }
        public async Task ListenNotification()
        {
            NpgsqlConnection connDB = new NpgsqlConnection(connectionString);
            await connDB.OpenAsync();
            connDB.Notification += EventNotificationToTheDB;
            await using (var cmd = new NpgsqlCommand("LISTEN notifydataskz;", connDB))
            {
                cmd.ExecuteNonQuery();
            }
            while (true)
                connDB.Wait();
        }

        public void EventNotificationToTheDB(object sender, NpgsqlNotificationEventArgs e)
        {
            _hubContext.Clients.All.SendAsync("GetAllDevices", this.GetAllDeviceData());
        }

        public string GetAllDeviceData()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                List<DataSKZ> ListOfDevicesSKZ = context.DataSKZ.Include(p => p.device).OrderBy(sort => sort.Id).ToList();
                _cache.Set("DevicesSKZ", SerializeObjectToJson(ListOfDevicesSKZ));
            }
            return _cache.Get("DevicesSKZ").ToString();
        }

        public string GetDataOnASingleDevice(int idToFind)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var deviceSKZ = context.DataSKZ.FirstOrDefault(device => device.deviceId == idToFind);
                _cache.Set("DeviceSKZ", SerializeObjectToJson(deviceSKZ));
            }
            return _cache.Get("DeviceSKZ").ToString();
        }

        public string SerializeObjectToJson(object dataToJson)
        {
            try
            {
                return JsonConvert.SerializeObject(dataToJson);
            }
            catch (Exception){ return null; }
        }
    }
}
