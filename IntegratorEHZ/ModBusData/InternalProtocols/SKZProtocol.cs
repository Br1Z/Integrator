using IntegratorEHZ.App_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IntegratorEHZ.ModBusData.InternalProtocols
{
    public class SKZProtocol : MBData
    {
        //необязательно использовалось для прослеживания объектов (тестирования)
        public static int count = 0;
      

        private const Byte CountOfCoil = 0x01;
        private const Byte CoilInitialRegister = 0x81;
        private const Byte CountOfDiscrete = 0x05;
        private const Byte DiscreteInitialRegister = 0x01;
        private const Byte CountOfHoldingReg = 0x04;
        private const Byte HoldingInitialRegister = 0x81;
        private const Byte CountOfInputReg = 0x1C;
        private const Byte InputInitialRegister = 0x01;

        private List<Byte[]> ListOfDevicePollingRequests = new List<Byte[]>();

        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        public SKZProtocol(IDbContextFactory<ApplicationDBContext> contextFactory) : base(CountOfCoil, CountOfDiscrete, CountOfHoldingReg, CountOfInputReg, CoilInitialRegister, DiscreteInitialRegister, HoldingInitialRegister, InputInitialRegister)
        {
            Interlocked.Increment(ref count);
            
            _contextFactory = contextFactory;
            InitializingListOfDevicePollingRequests();
        }

        private void InitializingListOfDevicePollingRequests()
        {
            byte[] request;
           
                request = new byte[] { 0x01, 0x01, 0x00, 0x81, 0x00, 0x01, 0xAD, 0xE2 };
                ListOfDevicePollingRequests.Add(request);
                request = new byte[] { 0x01, 0x03, 0x00, 0x81, 0x00, 0x04, 0x14, 0x21 };
                ListOfDevicePollingRequests.Add(request);
                request = new byte[] { 0x01, 0x04, 0x00, 0x01, 0x00, 0x0F, 0xE1, 0xCE };
                ListOfDevicePollingRequests.Add(request);
                request = new byte[] { 0x01, 0x04, 0x00, 0x10, 0x00, 0x0D, 0x30, 0x0A };
                ListOfDevicePollingRequests.Add(request);
        }
        public override List<byte[]> GetListOfDevicePollingRequests()
        {
            return ListOfDevicePollingRequests;
        }

        #region Регистры протокола
        [DisplayName("Напряжение питающей сети 1")]
        [Category("Input Registers")]
        public UInt16 PowerLineVoltage1
        {
            get
            {
                return InputRegArr[0];
            }
            set
            {
                if (ValidPowerLineVoltage(value))
                    InputRegArr[0] = value;
            }
        }

        [DisplayName("Значение счетчика электроэнергии 1")]
        [Category("Input Registers")]
        public UInt32 EnergyMeterValue1
        {
            get
            {
                return checked((UInt32)(InputRegArr[2] << 16) + InputRegArr[1]);
            }
            set
            {
                if (ValidEnergyMeterValue(value))
                {
                    InputRegArr[2] = checked((UInt16)(value >> 16));
                    InputRegArr[1] = checked((UInt16)(value & 0xFFFF));
                }
            }
        }

        [DisplayName("Напряжение питающей сети 2")]
        [Category("Input Registers")]
        public UInt16 PowerLineVoltage2
        {
            get
            {
                return InputRegArr[3];
            }
            set
            {
                if (ValidPowerLineVoltage(value))
                    InputRegArr[3] = value;
            }
        }

        [DisplayName("Значение счетчика электроэнергии 2")]
        [Category("Input Registers")]
        public UInt32 EnergyMeterValue2
        {
            get
            {
                return checked((UInt32)(InputRegArr[5] << 16) + InputRegArr[4]);
            }
            set
            {
                if (ValidEnergyMeterValue(value))
                {
                    InputRegArr[5] = checked((UInt16)(value >> 16));
                    InputRegArr[4] = checked((UInt16)(value & 0xFFFF));
                }
            }
        }

        [DisplayName("Время наработки")]
        [Category("Input Registers")]
        public UInt32 WorkTime
        {
            get
            {
                return checked((UInt32)(InputRegArr[8] << 16) + InputRegArr[7]);
            }
            set
            {
                if (ValidTime(value))
                {
                    InputRegArr[8] = checked((UInt16)(value >> 16));
                    InputRegArr[7] = checked((UInt16)(value & 0xFFFF));
                }
            }
        }

        [DisplayName("Время защиты сооружения")]
        [Category("Input Registers")]
        public UInt32 ProtectionTime
        {
            get
            {
                return checked((UInt32)(InputRegArr[10] << 16) + InputRegArr[9]);
            }
            set
            {
                if (ValidTime(value))
                {
                    InputRegArr[10] = checked((UInt16)(value >> 16));
                    InputRegArr[9] = checked((UInt16)(value & 0xFFFF));
                }
            }
        }

        [DisplayName("Выходной ток")]
        [Category("Input Registers")]
        public UInt16 OutputCurrent
        {
            get
            {
                return InputRegArr[11];
            }
            set
            {
                if (ValidOutputCurrent(value))
                    InputRegArr[11] = value;
            }
        }

        [DisplayName("Выходное напряжение")]
        [Category("Input Registers")]
        public UInt16 OutputVoltage
        {
            get
            {
                return InputRegArr[12];
            }
            set
            {
                if (ValidOutputVoltage(value))
                    InputRegArr[12] = value;
            }
        }

        [DisplayName("Защитный потенциал, суммарный")]
        [Category("Input Registers")]
        public Int16 SumProtectPotent
        {
            get
            {
                return (Int16)InputRegArr[13];
            }
            set
            {
                if (ValidSumPotential(value))
                    InputRegArr[13] = (UInt16)value;
            }
        }

        [DisplayName("Защитный потенциал, поляризационный")]
        [Category("Input Registers")]
        public Int16 PolProtectPotent
        {
            get
            {
                return (Int16)InputRegArr[14];
            }
            set
            {
                if (ValidPolPotential(value))
                    InputRegArr[14] = (UInt16)value;
            }
        }

        [DisplayName("Режим управления станцией")]
        [Category("Input Registers")]
        public UInt16 ControlMode
        {
            get
            {
                return InputRegArr[15];
            }
            set
            {
                if (ValidControlMode(value))
                    InputRegArr[15] = value;
            }
        }

        [DisplayName("Состояние силового модуля 1")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState1
        {
            get
            {
                return InputRegArr[16];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[16] = value;
            }
        }

        [DisplayName("Состояние силового модуля 2")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState2
        {
            get
            {
                return InputRegArr[17];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[17] = value;
            }
        }

        [DisplayName("Состояние силового модуля 3")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState3
        {
            get
            {
                return InputRegArr[18];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[18] = value;
            }
        }

        [DisplayName("Состояние силового модуля 4")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState4
        {
            get
            {
                return InputRegArr[19];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[19] = value;
            }
        }

        [DisplayName("Состояние силового модуля 5")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState5
        {
            get
            {
                return InputRegArr[20];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[20] = value;
            }
        }

        [DisplayName("Состояние силового модуля 6")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState6
        {
            get
            {
                return InputRegArr[21];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[21] = value;
            }
        }

        [DisplayName("Состояние силового модуля 7")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState7
        {
            get
            {
                return InputRegArr[22];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[22] = value;
            }
        }

        [DisplayName("Состояние силового модуля 8")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState8
        {
            get
            {
                return InputRegArr[23];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[23] = value;
            }
        }

        [DisplayName("Состояние силового модуля 9")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState9
        {
            get
            {
                return InputRegArr[24];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[24] = value;
            }
        }

        [DisplayName("Состояние силового модуля 10")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState10
        {
            get
            {
                return InputRegArr[25];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[25] = value;
            }
        }

        [DisplayName("Состояние силового модуля 11")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState11
        {
            get
            {
                return InputRegArr[26];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[26] = value;
            }
        }

        [DisplayName("Состояние силового модуля 12")]
        [Category("Input Registers")]
        public UInt16 PowerModuleState12
        {
            get
            {
                return InputRegArr[27];
            }
            set
            {
                if (ValidPowerModuleState(value))
                    InputRegArr[27] = value;
            }
        }

        [DisplayName("Несанкционированный доступ в шкаф станции (блок-бокс)")]
        [Category("Discrete Registers")]
        public bool UnauthorisedAccess
        {
            get
            {
                return DiscreteArr[0];
            }
            set
            {
                DiscreteArr[0] = value;
            }
        }

        [DisplayName("Режим управления станцией")]
        [Category("Discrete Registers")]
        public bool StationControllMode
        {
            get
            {
                return DiscreteArr[1];
            }
            set
            {
                DiscreteArr[1] = value;
            }
        }

        [DisplayName("Неисправность станции")]
        [Category("Discrete Registers")]
        public bool StationFailure
        {
            get
            {
                return DiscreteArr[2];
            }
            set
            {
                DiscreteArr[2] = value;
            }
        }

        [DisplayName("Обрыв измерительных цепей от защищаемого сооружения или от электрода сравнения")]
        [Category("Discrete Registers")]
        public bool LineDisconnection
        {
            get
            {
                return DiscreteArr[3];
            }
            set
            {
                DiscreteArr[3] = value;
            }
        }

        [DisplayName("Включение группы основных или резервных силовых модулей (СКЗ). ")]
        [Category("Discrete Registers")]
        public bool MainOrReserved
        {
            get
            {
                return DiscreteArr[4];
            }
            set
            {
                DiscreteArr[4] = value;
            }
        }

        [DisplayName("Дистанционное отключение и включение силовых модулей")]
        [Category("Coil Registers")]
        public bool DistantPowerControl
        {
            get
            {
                return CoilArr[0];
            }
            set
            {
                CoilArr[0] = value;
            }
        }

        [DisplayName("Задание выходного тока")]
        [Category("Holding Registers")]
        public UInt16 SetOutputCurrent
        {
            get
            {
                return HoldingRegArr[0];
            }
            set
            {
                if (ValidOutputCurrent(value))
                    HoldingRegArr[0] = value;
            }
        }

        [DisplayName("Задание сум. потенциала")]
        [Category("Holding Registers")]
        public Int16 SetSumPotential
        {
            get
            {
                return (Int16)HoldingRegArr[1];
            }
            set
            {
                if (ValidSumPotential(value))
                    HoldingRegArr[1] = (UInt16)value;
            }
        }

        [DisplayName("Задание поляр. потенциала")]
        [Category("Holding Registers")]
        public Int16 SetPolPotential
        {
            get
            {
                return (Int16)HoldingRegArr[2];
            }
            set
            {
                if (ValidPolPotential(value))
                    HoldingRegArr[2] = (UInt16)value;
            }
        }

        [DisplayName("Управление режимами стабилизации станции")]
        [Category("Holding Registers")]
        public UInt16 SetStabControlMode
        {
            get
            {
                return HoldingRegArr[3];
            }
            set
            {
                if (ValidStabControlMode(value))
                    HoldingRegArr[3] = value;
            }
        }
        #endregion

        #region Функции записи в базу данных
        /// <summary>
        /// Функция записи в базу данных CoilStatus
        /// </summary>
        protected override void WriteToDBCoildStatus()
        { 
            //Console.WriteLine($"Устройство с ID[{IdDevice}] записывает CoilStatus");
            using (var context = _contextFactory.CreateDbContext())
            {
                var result = context.DataSKZ.SingleOrDefault(item => item.device.Id == IdDevice);
                if (result != null)
                {
                    result.DistantPowerControl = DistantPowerControl;
                    result.Errors = null;
                    context.DataSKZ.Update(result);
                    context.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Функция записи в базу данных DiscreteInputs
        /// </summary>
        protected override void WriteToDBDiscreteInputs()
        {
            //Console.WriteLine($"Устройство с ID[{IdDevice}] записывает discret");
            using (var context = _contextFactory.CreateDbContext())
            {
                var result = context.DataSKZ.SingleOrDefault(item => item.device.Id == IdDevice);
                if (result != null)
                {
                    result.UnauthorisedAccess = UnauthorisedAccess;
                    result.StationControllMode = StationControllMode;
                    result.StationFailure = StationFailure;
                    result.LineDisconnection = LineDisconnection;
                    result.MainOrReserved = MainOrReserved;
                    result.Errors = null;
                    context.DataSKZ.Update(result);
                    context.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Функция записи в базу данных HoldingRegisters
        /// </summary>
        protected override void WriteToDBHoldingRegisters()
        {
            //Console.WriteLine($"Устройство с ID[{IdDevice}] записывает HoldingRegisters");
            using (var context = _contextFactory.CreateDbContext())
            {
                var result = context.DataSKZ.SingleOrDefault(item => item.device.Id == IdDevice);
                if (result != null)
                {
                    result.SetOutputCurrent = SetOutputCurrent;
                    result.SetSumPotential = SetSumPotential;
                    result.SetPolPotential = SetPolPotential;
                    result.SetStabControlMode = SetStabControlMode;
                    result.Errors = null;
                    context.DataSKZ.Update(result);
                    context.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Функция записи в базу данных Input Registers
        /// </summary>
        protected override void WriteToDBInputRegisters()
        {
            //Console.WriteLine($"Устройство с ID[{IdDevice}] записывает INPUT");
            using (var context = _contextFactory.CreateDbContext())
            {
                var result = context.DataSKZ.SingleOrDefault(item => item.device.Id == IdDevice);
                if (result != null)
                {
                    result.PowerLineVoltage1 = PowerLineVoltage1;
                    result.EnergyMeterValue1 = EnergyMeterValue1;
                    result.PowerLineVoltage2 = PowerLineVoltage2;
                    result.EnergyMeterValue2 = EnergyMeterValue2;
                    result.WorkTime = WorkTime;
                    result.ProtectionTime = ProtectionTime;
                    result.OutputCurrent = OutputCurrent;
                    result.OutputVoltage = OutputVoltage;
                    result.SumProtectPotent = SumProtectPotent;
                    result.PolProtectPotent = PolProtectPotent;
                    result.ControlMode = ControlMode;
                    result.PowerModuleState1 = PowerModuleState1;
                    result.PowerModuleState2 = PowerModuleState2;
                    result.PowerModuleState3 = PowerModuleState3;
                    result.PowerModuleState4 = PowerModuleState4;
                    result.PowerModuleState5 = PowerModuleState5;
                    result.PowerModuleState6 = PowerModuleState6;
                    result.PowerModuleState7 = PowerModuleState7;
                    result.PowerModuleState8 = PowerModuleState8;
                    result.PowerModuleState9 = PowerModuleState9;
                    result.PowerModuleState10 = PowerModuleState10;
                    result.PowerModuleState11 = PowerModuleState11;
                    result.PowerModuleState12 = PowerModuleState12;
                    result.Errors = null;
                    context.DataSKZ.Update(result);
                    context.SaveChanges();
                }
            }
        }
        protected override void WriteToDBErrors(string ErrMessage)
        {
            Console.WriteLine($"Устройство с ID[{IdDevice}] записывает ошибку: {ErrMessage}");
            using (var context = _contextFactory.CreateDbContext())
            {
                var result = context.DataSKZ.SingleOrDefault(item => item.device.Id == IdDevice);
                if (result != null)
                {
                    result.Errors = ErrMessage;
                    context.DataSKZ.Update(result);
                    context.SaveChanges();
                }
            }

        }
        #endregion

        #region Валидация данных
        private bool ValidPowerLineVoltage(UInt16 val)
        {
            if ((val >= 0) && (val <= 3000))
                return true;
            return false;
        }

        private bool ValidEnergyMeterValue(UInt32 val)
        {
            if ((val >= 0) && (val <= 9999999))
                return true;
            return false;
        }

        private bool ValidTime(UInt32 val)
        {
            if ((val >= 0) && (val <= 999999))
                return true;
            return false;
        }

        private bool ValidOutputCurrent(UInt16 val)
        {
            if ((val >= 0) && (val <= 1000))
                return true;
            return false;
        }

        private bool ValidOutputVoltage(UInt16 val)
        {
            if ((val >= 0) && (val <= 1000))
                return true;
            return false;
        }

        private bool ValidSumPotential(Int16 val)
        {
            if ((val >= -500) && (val <= 500))
                return true;
            return false;
        }

        private bool ValidPolPotential(Int16 val)
        {
            if ((val >= -500) && (val <= 500))
                return true;
            return false;
        }

        private bool ValidControlMode(UInt16 val)
        {
            if ((val >= 0) && (val <= 3))
                return true;
            return false;
        }

        private bool ValidPowerModuleState(UInt16 val)
        {
            if ((val >= 0) && (val <= 3))
                return true;
            return false;
        }

        private bool ValidStabControlMode(UInt16 val)
        {
            if ((val >= 0) && (val <= 2))
                return true;
            return false;
        }
        #endregion
    }
}
