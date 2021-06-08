using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IntegratorEHZ.ModBusData
{
    public abstract class MBData
    {
        public int IdDevice { get; set; }
        public string ImeiDevice { get; set; }
        public Guid InstanceID { get; private set; }
        protected enum MBCommandCodes : byte
        {
            ReadCoilStatus = 0x01,
            ReadDiscreteInputs,
            ReadHoldingRegisters,
            ReadInputRegisters,
            ForceSingleCoil,
            PresetSingleRegister,
            ForceMultipleCoils = 0x0F,
            PresetMultipleRegisters,
            ReportSlaveId
        }
        protected enum MBErrorCodes : byte
        {
            UnknownFunction = 0x01,         // Принятый код функции не может быть обработан.
            AddressUnreachable,             // Адрес данных, указанный в запросе, недоступен.
            InvalidData,                    // Значение, содержащееся в поле данных запроса, является недопустимой величиной.
            ErrorInProcessing,              // Невосстанавливаемая ошибка имела место, пока ведомое устройство пыталось выполнить затребованное действие.
            ProcessingTakeMoreTime,         // Ведомое устройство приняло запрос и обрабатывает его, но это требует много времени. Этот ответ предохраняет ведущее устройство от генерации ошибки тайм-аута.
            Busy,                           // Ведомое устройство занято обработкой команды. Ведущее устройство должно повторить сообщение позже, когда ведомое освободится.
            CantProcess,                    // Ведомое устройство не может выполнить программную функцию, заданную в запросе. Этот код возвращается для неуспешного программного запроса, использующего функции с номерами 13 или 14. Ведущее устройство должно запросить диагностическую информацию или информацию об ошибках от ведомого.
            ParityCintrolError,             // Ведомое устройство при чтении расширенной памяти обнаружило ошибку контроля четности.
            GatewayIsNotCorrectly = 0x0A,   // Шлюз неправильно настроен или перегружен запросами.
            DeviceIsNotOnline               // Slave устройства нет в сети или от него нет ответа.
        };

        protected bool[] CoilArr;
        protected bool[] DiscreteArr;
        protected UInt16[] HoldingRegArr;
        protected UInt16[] InputRegArr;

        private readonly UInt16 CoilOffset;
        private readonly UInt16 DiscreteOffset;
        private readonly UInt16 HoldingOffset;
        private readonly UInt16 InputOffset;

        protected MBData(Byte countOfCoil, Byte countOfDiscrete, Byte countOfHoldingReg, Byte countOfInputReg, UInt16 CoilInitialRegister = 0, UInt16 DiscreteInitialRegister = 0, UInt16 HoldingInitialRegister = 0, UInt16 InputInitialRegister = 0)
        {
            this.InstanceID = Guid.NewGuid();
            CoilArr = new bool[countOfCoil];
            DiscreteArr = new bool[countOfDiscrete];
            HoldingRegArr = new UInt16[countOfHoldingReg];
            InputRegArr = new UInt16[countOfInputReg];

            CoilOffset = CoilInitialRegister;
            DiscreteOffset = DiscreteInitialRegister;
            HoldingOffset = HoldingInitialRegister;
            InputOffset = InputInitialRegister;
        }

        protected Byte[] Request { get; set; }
        protected Byte[] Response { get; set; }

        /// <summary>
        /// Обработка ответа на запрос
        /// </summary>
        /// <param name="RequestData">Запрос в байтах</param>
        /// <param name="CommandData">Ответ на запрос в байтах</param>
        /// <returns></returns>
        public void ResponseProcessing(Byte[] RequestData, Byte[] CommandData)
        {
            Request = RequestData;
            Response = CommandData;
            Byte modBusAddr = CommandData[0];
            Byte cmd = CommandData[1];

            if (CommandData.GetLength(0) < 6)
            {
                ErrorResponse(CommandData[2]);
            }
            else if (ModRTU_CRC(CommandData, CommandData.Length) != 0x00)
            {
                ErrorResponse((byte)MBErrorCodes.ParityCintrolError);
            }
            else
            {
                //сокращаем размер массива убирая CRC, модбас адрес,код функции,
                Byte[] data = new Byte[CommandData.GetLength(0) - 4];
                for (int i = 2; i < CommandData.GetLength(0) - 2; i++)
                {
                    data[i - 2] = CommandData[i];
                }

                UInt16 startAddr = Convert.ToUInt16((Request[2] << 8) + Request[3]);
                UInt16 CountOfRegisters = Convert.ToUInt16((Request[4] << 8) + Request[5]);


                switch ((MBCommandCodes)cmd)
                {
                    case MBCommandCodes.ReadCoilStatus:
                        {
                            ReadCoilStatus(startAddr, CountOfRegisters, data);
                        }
                        break;
                    case MBCommandCodes.ReadDiscreteInputs:
                        {
                            ReadDiscreteInputs(startAddr, CountOfRegisters, data);
                        }
                        break;
                    case MBCommandCodes.ReadHoldingRegisters:
                        {
                            ReadHoldingRegisters(startAddr, CountOfRegisters, data);
                        }
                        break;
                    case MBCommandCodes.ReadInputRegisters:
                        {
                            ReadInputRegisters(startAddr, CountOfRegisters, data);
                        }
                        break;
                    case MBCommandCodes.ForceSingleCoil:
                        {
                            ForceSingleCoil(startAddr, data);
                        }
                        break;
                    case MBCommandCodes.PresetSingleRegister:
                        {
                            PresetSingleRegister(startAddr, data);
                        }
                        break;
                    case MBCommandCodes.ForceMultipleCoils:
                        {
                            ForceMultipleCoils(startAddr, CountOfRegisters);
                        }
                        break;
                    case MBCommandCodes.PresetMultipleRegisters:
                        {
                            PresetMultipleRegisters(startAddr, CountOfRegisters);
                        }
                        break;
                    default:
                        {
                            ErrorResponse((byte)MBErrorCodes.UnknownFunction);
                        }
                        break;
                }
            }

        }

        void DEBUG(string reg)
        {
            Console.WriteLine("--------------");
            Console.WriteLine($"ID потока запроса когда выскакивает ошибка {Thread.CurrentThread.ManagedThreadId}, id объекта {InstanceID}");
            Console.WriteLine($"Устройство {IdDevice}");
            Console.WriteLine($"Запрос должен быть {reg} registrov : {Request[1]}");
            foreach (var item in Request)
            {
                Console.Write($"{BitConverter.ToUInt16(BitConverter.GetBytes(item))} ");
            }
            Console.WriteLine($"ID объекта {InstanceID} запрос {reg}");
            Console.WriteLine("--------------");
            Console.ReadKey();
        }

        #region Чтение регистров
        /// <summary>
        /// Чтение состояний Coil
        /// </summary>
        /// <param name="startAddr">Адрес первого регистра</param>
        /// <param name="CountOfReg">Количество регитров</param>
        /// <param name="data">PDU не включающий код функции </param>
        /// <returns></returns>
        protected void ReadCoilStatus(UInt16 startAddr, UInt16 CountOfReg, Byte[] data)
        {
            
            startAddr -= CoilOffset;
           
            if ((startAddr + CountOfReg) > CoilArr.Length)
            {
                DEBUG("Coil");
                Console.WriteLine();
                Console.WriteLine("Стартовый адрес {0} ",startAddr);

                ErrorResponse((byte)MBErrorCodes.AddressUnreachable);
                
                return;
            }

            UInt16 regOffset = 1;
            int indexArr = 0;

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    CoilArr[indexArr] = Convert.ToBoolean((data[regOffset] >> bit) & 0x01);
                    indexArr++;
                    i++;

                    if (indexArr == CountOfReg + startAddr)
                        break;
                }
                regOffset++;
            }
            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBCoildStatus();

        }

        /// <summary>
        /// Чтение состояний Discrete Inputs
        /// </summary>
        /// <param name="startAddr">Адрес первого регистра</param>
        /// <param name="CountOfReg">Количество регитров</param>
        /// <param name="data">PDU не включающий код функции </param>
        /// <returns></returns>
        protected void ReadDiscreteInputs(UInt16 startAddr, UInt16 CountOfReg, Byte[] data)
        {
            startAddr -= DiscreteOffset;

            if ((startAddr + CountOfReg) > DiscreteArr.Length)
            {
                DEBUG("DiscreteInputs");
                Console.WriteLine();
                Console.WriteLine("Стартовый адрес {0} ", startAddr);
                ErrorResponse((byte)MBErrorCodes.AddressUnreachable);
                return;
            }

            UInt16 regOffset = 1;
            int indexArr = 0;

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    DiscreteArr[indexArr] = Convert.ToBoolean((data[regOffset] >> bit) & 0x01);
                    indexArr++;
                    i++;

                    if (indexArr == CountOfReg + startAddr)
                        break;
                }
                regOffset++;
            }
            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBDiscreteInputs();
        }

        /// <summary>
        /// Чтение состояний Holding Registers
        /// </summary>
        /// <param name="startAddr">Адрес первого регистра</param>
        /// <param name="CountOfReg">Количество регитров</param>
        /// <param name="data">PDU не включающий код функции </param>
        /// <returns></returns>
        protected void ReadHoldingRegisters(UInt16 startAddr, UInt16 CountOfReg, Byte[] data)
        {
            UInt16 regOffset = 1;
            startAddr -= HoldingOffset;

            if ((startAddr + CountOfReg) > HoldingRegArr.Length)
            {
                DEBUG("HoldingRegisters");
                Console.WriteLine();
                Console.WriteLine("Стартовый адрес {0} ", startAddr);
                ErrorResponse((byte)MBErrorCodes.AddressUnreachable);
                return;
            }

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                //Console.WriteLine(Convert.ToUInt16((data[regOffset] << 8) + data[regOffset + 1]));
                HoldingRegArr[i] = Convert.ToUInt16((data[regOffset] << 8) + data[regOffset + 1]);
                regOffset += 2;
            }

            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBHoldingRegisters();
        }

        /// <summary>
        /// Чтение состояний Input Registers
        /// </summary>
        /// <param name="startAddr">Адрес первого регистра</param>
        /// <param name="CountOfReg">Количество регитров</param>
        /// <param name="data">PDU не включающий код функции </param>
        /// <returns></returns>
        protected void ReadInputRegisters(UInt16 startAddr, UInt16 CountOfReg, Byte[] data)
        {
            UInt16 regOffset = 1;

            startAddr -= InputOffset;

            if ((startAddr + CountOfReg) > InputRegArr.Length)
            {
                DEBUG("InputRegisters");
                Console.WriteLine();
                Console.WriteLine("Стартовый адрес {0} ", startAddr);
                ErrorResponse((byte)MBErrorCodes.AddressUnreachable);
                return;
            }

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                //Console.WriteLine(Convert.ToUInt16((data[regOffset] << 8) + data[regOffset + 1]));
                InputRegArr[i] = Convert.ToUInt16((data[regOffset] << 8) + data[regOffset + 1]);
                regOffset += 2;
            }

            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBInputRegisters();
        }

        /// <summary>
        /// Обработка ответа на запись coil регистра
        /// </summary>
        /// <param name="startAddr">Начальниый адрес</param>
        /// <param name="data">PDU не включающий код функции</param>
        protected void ForceSingleCoil(UInt16 startAddr, Byte[] data) 
        {
            UInt16 RegisterValue = Convert.ToUInt16((data[2] << 8) + data[3]);
            CoilArr[startAddr] = (RegisterValue == 0) ? false : true;
            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBCoildStatus();
        }
        /// <summary>
        /// Обработка ответа на запись holding регистра
        /// </summary>
        /// <param name="startAddr">Начальниый адрес</param>
        /// <param name="data">PDU не включающий код функции</param>
        protected void PresetSingleRegister(UInt16 startAddr, Byte[] data) 
        {
            UInt16 RegisterValue = Convert.ToUInt16((data[2] << 8) + data[3]);
            HoldingRegArr[startAddr] = RegisterValue;
            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBHoldingRegisters();
        }

        /// <summary>
        /// Обработка ответа на запись нескольких coil регистров
        /// </summary>
        /// <param name="startAddr">Начальный адрес</param>
        /// <param name="CountOfReg">Количество регистров</param>
        protected void ForceMultipleCoils(UInt16 startAddr, UInt16 CountOfReg) 
        {
            startAddr -= CoilOffset;

            UInt16 regOffset = 7;
            int indexArr = 0;

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    CoilArr[indexArr] = Convert.ToBoolean((Request[regOffset] >> bit) & 0x01);
                    indexArr++;
                    i++;

                    if (indexArr == CountOfReg + startAddr)
                        break;
                }
                regOffset++;
            }
                //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
                WriteToDBCoildStatus();
        }

        /// <summary>
        /// Обработка ответа на запись нескольких holding регистров
        /// </summary>
        /// <param name="startAddr">Начальный адрес</param>
        /// <param name="CountOfReg">Количество регистров</param>
        protected void PresetMultipleRegisters(UInt16 startAddr, UInt16 CountOfReg) 
        {
            UInt16 regOffset = 7;
            startAddr -= HoldingOffset;

            for (int i = startAddr; i < CountOfReg + startAddr; i++)
            {
                //Console.WriteLine(Convert.ToUInt16((data[regOffset] << 8) + data[regOffset + 1]));
                HoldingRegArr[i] = Convert.ToUInt16((Request[regOffset] << 8) + Request[regOffset + 1]);
                regOffset += 2;
            }
            //фУНКЦИЯ ДЛЯ ЗАПИСИ В БАЗУ ДАННЫХ
            WriteToDBHoldingRegisters();
        }
        #endregion

        /// <summary>
        /// Обработка кода ошибки ответа устройства.
        /// </summary>
        /// <param name="errCode">Код ошибки</param>
        /// <returns></returns>
        protected void ErrorResponse(Byte errCode)
        {
            string MsgError = "";
            UInt16 startAddr = Convert.ToUInt16((Request[2] << 8) + Request[3]);
            UInt16 CountOfRegisters = Convert.ToUInt16((Request[4] << 8) + Request[5]);
            
            switch ((MBErrorCodes)errCode)
            {
                case MBErrorCodes.UnknownFunction:
                    {
                        MsgError = "Принятый код функции ["+ Response[1] +"] не может быть обработан. Ответ: ["+ BitConverter.ToString(Response)+"]. ";
                    }
                    break;
                case MBErrorCodes.AddressUnreachable:
                    {
                        UInt16 EndRangeAddress = (ushort)(startAddr + CountOfRegisters);
                        MsgError = "Адрес данных в диапазоне ["+ startAddr + ":" + EndRangeAddress + "], указанный в запросе ["+ BitConverter.ToString(Request) + "], недоступен.";
                    }
                    break;
                case MBErrorCodes.InvalidData:
                    {
                        MsgError = "Значение, содержащееся в поле данных запроса [" + BitConverter.ToString(Request) + "], является недопустимой величиной.";
                    }
                    break;
                case MBErrorCodes.ErrorInProcessing:
                    {
                        MsgError = "Невосстанавливаемая ошибка имела место, пока устройство пыталось выполнить затребованное действие.";
                    }
                    break;
                case MBErrorCodes.ProcessingTakeMoreTime:
                    {
                        MsgError = "Устройство приняло запрос и обрабатывает его, но это требует много времени.";
                    }
                    break;
                case MBErrorCodes.Busy:
                    {
                        MsgError = "Устройство занято обработкой команды. Ведущее устройство должно повторить сообщение позже, когда ведомое освободится.";
                    }
                    break;
                case MBErrorCodes.CantProcess:
                    {
                        MsgError = "Устройство не может выполнить программную функцию, заданную в запросе. Этот код возвращается для неуспешного программного запроса, использующего функции с номерами 13 или 14. Ведущее устройство должно запросить диагностическую информацию или информацию об ошибках от ведомого.";
                    }
                    break;
                case MBErrorCodes.ParityCintrolError:
                    {
                        MsgError = "Устройство при чтении данных обнаружило ошибку контроля четности.";
                    }
                    break;
                case MBErrorCodes.GatewayIsNotCorrectly:
                    {
                        MsgError = "Шлюз неправильно настроен или перегружен запросами..";
                    }
                    break;
                case MBErrorCodes.DeviceIsNotOnline:
                    {
                        MsgError = "Устройства нет в сети или от него нет ответа.";
                    }
                    break;       
            }
            WriteToDBErrors(MsgError);
        }

        #region Функции записи в базу данных
        protected virtual void WriteToDBCoildStatus(){}
        protected virtual void WriteToDBDiscreteInputs(){}
        protected virtual void WriteToDBHoldingRegisters(){}
        protected virtual void WriteToDBInputRegisters(){}
        protected virtual void WriteToDBErrors(string ErrMessage) { }
        #endregion


        public virtual List<Byte[]> GetListOfDevicePollingRequests()
        {
            return null;
        }

        /// <summary>
        /// Функция проверки контрольной суммы CRC
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns>значение CRC</returns>
        public static UInt16 ModRTU_CRC(byte[] buf, int len)
        {
            UInt16 crc = 0xFFFF; //Стартовые данные (init), то есть значения регистров на момент начала вычислений;


            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];          //Выполняем операцию XOR с наименьшим значищем байтом.


                for (int i = 8; i != 0; i--)      // Цикл над каждым битом.
                {
                    if ((crc & 0x0001) != 0)        // Если старший бит в слове «1», то слово сдвигается влево на один разряд с последующим выполнением операции XOR c порождающим полиномом.
                    {
                        crc >>= 1;                    
                        crc ^= 0xA001;
                    }
                    else                            // Если старший бит в слове «0», то после сдвига операция XOR не выполняется.
                    {
                        crc >>= 1;     
                    }
                }
            }
            // Обратите внимание, что в этом числе поменялись местами младшие и старшие байты, поэтому используйте его соответственно (или поменяйте байты)
            return crc;
        }
    }
}
