using IntegratorEHZ.Repositorys;
using IntegratorEHZ.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Hubs
{
    
    public class IntegratorHub : Hub
    {
        private readonly IRepositoryProtocolData _repository;
        private IMemoryCache _cache;
        private IHubContext<IntegratorHub> _hubContext;
        private IMediator _mediator;

        public IntegratorHub(IRepositoryProtocolData repository, IMemoryCache cache, IHubContext<IntegratorHub> hubContext, IMediator mediator)
        {
            _repository = repository;
            _cache = cache;
            _hubContext = hubContext;
            _mediator = mediator;
        }

        //public async Task GetDevice(string id)
        //{
        //    var jsonData = _repository.GetDataOnASingleDevice(Int32.Parse(id));
        //    bool IsDeviceConnected = _mediator.CheckingTheConnectionById(Int32.Parse(id));
        //    await Clients.All.SendAsync("SendDevice", _cache.Get("DeviceSKZ").ToString(), IsDeviceConnected);
        //}
    }
}
