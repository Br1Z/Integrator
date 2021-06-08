using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Models
{
    public class LogSKZ
    {
        public int Id { get; set; }
        public int Id_device { get; set; }
        [DisplayName("Время")]
        public DateTime TimeReceivedData { get; set; }
        [DisplayName("Напряжение питающей сети 1")]
        public UInt16? PowerLineVoltage1 { get; set; }
        [DisplayName("Значение счетчика электроэнергии 1")]
        public UInt32? EnergyMeterValue1 { get; set; }
        [DisplayName("Напряжение питающей сети 2")]
        public UInt16? PowerLineVoltage2 { get; set; }
        [DisplayName("Значение счетчика электроэнергии 2")]
        public UInt32? EnergyMeterValue2 { get; set; }
        [DisplayName("Время наработки")]
        public UInt32? WorkTime { get; set; }
        [DisplayName("Время защиты сооружения")]
        public UInt32? ProtectionTime { get; set; }
        [DisplayName("Выходной ток")]
        public UInt16? OutputCurrent { get; set; }
        [DisplayName("Выходное напряжение")]
        public UInt16? OutputVoltage { get; set; }
        [DisplayName("Защитный потенциал, суммарный")]
        public Int16? SumProtectPotent { get; set; }
        [DisplayName("Защитный потенциал, поляризационный")]
        public Int16? PolProtectPotent { get; set; }
        [DisplayName("Режим управления станцией")]
        public UInt16? ControlMode { get; set; }
        [DisplayName("Состояние силового модуля 1")]
        public UInt16? PowerModuleState1 { get; set; }
        [DisplayName("Состояние силового модуля 2")]
        public UInt16? PowerModuleState2 { get; set; }
        [DisplayName("Состояние силового модуля 3")]
        public UInt16? PowerModuleState3 { get; set; }
        [DisplayName("Состояние силового модуля 4")]
        public UInt16? PowerModuleState4 { get; set; }
        [DisplayName("Состояние силового модуля 5")]
        public UInt16? PowerModuleState5 { get; set; }
        [DisplayName("Состояние силового модуля 6")]
        public UInt16? PowerModuleState6 { get; set; }
        [DisplayName("Состояние силового модуля 7")]
        public UInt16? PowerModuleState7 { get; set; }
        [DisplayName("Состояние силового модуля 8")]
        public UInt16? PowerModuleState8 { get; set; }
        [DisplayName("Состояние силового модуля 9")]
        public UInt16? PowerModuleState9 { get; set; }
        [DisplayName("Состояние силового модуля 10")]
        public UInt16? PowerModuleState10 { get; set; }
        [DisplayName("Состояние силового модуля 11")]
        public UInt16? PowerModuleState11 { get; set; }
        [DisplayName("Состояние силового модуля 12")]
        public UInt16? PowerModuleState12 { get; set; }
        [DisplayName("Несанкционированный доступ в шкаф станции (блок-бокс)")]
        public bool UnauthorisedAccess { get; set; }
        [DisplayName("Режим управления станцией")]
        public bool StationControllMode { get; set; }
        [DisplayName("Неисправность станции")]
        public bool StationFailure { get; set; }
        [DisplayName("Обрыв измерительных цепей от защищаемого сооружения или от электрода сравнения")]
        public bool LineDisconnection { get; set; }
        [DisplayName("Включение группы основных или резервных силовых модулей (СКЗ). ")]
        public bool MainOrReserved { get; set; }
        [DisplayName("Дистанционное отключение и включение силовых модулей")]
        public bool DistantPowerControl { get; set; }
        [DisplayName("Задание выходного тока")]
        public UInt16? SetOutputCurrent { get; set; }
        [DisplayName("Задание сум. потенциала")]
        public Int16? SetSumPotential { get; set; }
        [DisplayName("Задание поляр. потенциала")]
        public Int16? SetPolPotential { get; set; }
        [DisplayName("Управление режимами стабилизации станции")]
        public UInt16? SetStabControlMode { get; set; }
        [DisplayName("Ошибки")]
        public string Errors { get; set; }
    }
}
