using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Server
{
    public class SurveyData
    {
        static int count;
        public Byte[] Request { get; set; }
        public Byte[] Response { get; set; }
        public int deviceid { get; set; }
        public string Imei { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        

        public void PrintExchange()
        {
            if (Response[1] != Request[1])
            {
                Console.WriteLine($"{++count}| {DateTime.Now}");
                Console.WriteLine("\t||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
                Console.WriteLine($"\t|\t\tОтвет не относиться к запросу                        |");
                Console.WriteLine("\t\t==========================================================");
                Console.WriteLine($"\t\tDevice ID({Imei}), Запрос = {BitConverter.ToString(Request)}");
                Console.WriteLine($"\t\tDevice ID({Imei}), Ответ = {BitConverter.ToString(Response)}");
                Console.WriteLine("\t\t===========================================================");
                Console.WriteLine("\t||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now}");
                Console.WriteLine("==========================================================");
                Console.WriteLine($"Device ID({Imei}), Запрос = {BitConverter.ToString(Request)}");
                Console.WriteLine($"Device ID({Imei}), Ответ = {BitConverter.ToString(Response)}");
                Console.WriteLine("===========================================================");
            }
        }
            
    }
}
