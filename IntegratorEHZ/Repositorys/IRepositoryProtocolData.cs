using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Repositorys
{
    public interface IRepositoryProtocolData
    {
        Task ListenNotification();
        public string GetAllDeviceData();
        public string GetDataOnASingleDevice(int idToFind);
        public void EventNotificationToTheDB(object sender, NpgsqlNotificationEventArgs e);
        public String SerializeObjectToJson(Object alarmspeed);
    }
}
