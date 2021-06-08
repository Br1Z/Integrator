using IntegratorEHZ.App_Data;
using IntegratorEHZ.Models;
using IntegratorEHZ.Repositorys;
using IntegratorEHZ.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratorEHZ.ModBusData;
using IntegratorEHZ.Models.SKZ;

namespace IntegratorEHZ.Controllers
{
    

    public class HomeOldController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMediator _mediator;
        public HomeOldController(IDbContextFactory<ApplicationDBContext> contextFactory, IMediator mediator)
        {
            _context = contextFactory.CreateDbContext();
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Device> objList = _context.Devices.OrderBy(sort => sort.Id);
            return View(objList);
        }

        [HttpPost]
        public IActionResult Filtering(SearchTheRange model)
        {
        
            DateTime StartDate = model.dateFrom.Date + model.timeFrom.TimeOfDay;
            DateTime EndDate = model.dateTo.Date + model.timeTo.TimeOfDay;

            DateTime testDate = new DateTime(2021, 06, 01);
            var obj = _context.LogSKZ.Where(m => m.TimeReceivedData >= StartDate && m.TimeReceivedData <= EndDate).OrderByDescending(m=>m.TimeReceivedData);

            Console.WriteLine($"{Convert.ToString(StartDate)} : {Convert.ToString(EndDate)}");
            //Console.WriteLine(Convert.ToString(dateTimeFrom), Convert.ToString(dateTimeTo));
            return PartialView("_ViewDataJournal",obj);
        }

        [HttpGet]
        public IActionResult SearchImei(string IMEI)
        {
            var SearchList = _context.Devices.Where(s => s.IMEI.Contains(IMEI)).OrderBy(sort=>sort.Id);
            //if (!String.IsNullOrEmpty(IMEI))
            //{
            //    //SearchList =  (IOrderedQueryable<Device>)SearchList.Where(s => s.IMEI.Contains(IMEI));
            //    SearchList = (IOrderedQueryable<Device>)SearchList.Where(s => s.IMEI.Contains(IMEI));
            //}
            return PartialView("_ListDevice", SearchList);

        }



        public IActionResult TabDevice(int? Id)
        {
            var deviceData = _context.DataSKZ.Include(d => d.device).FirstOrDefault(x => x.deviceId == Id);
            var Journal = _context.LogSKZ.Where(d => d.Id_device == Id);
            //bool IsDeviceConnected = _mediator.CheckingTheConnectionById((int)id);
            if (_mediator.CheckingTheConnectionById((int)Id) == true)
            {
                ViewBag.IsDeviceConnected = "led-green";
            }
            else
            {
                ViewBag.IsDeviceConnected = "led-red";
            }
            return PartialView("_TabDevice", new AboutDevices{ dataSKZ = deviceData, logSKZs = Journal }) ;
        }
        [HttpGet]
        public IActionResult DeviceData(int? Id)
        {
            var deviceData = _context.DataSKZ.Include(d => d.device).FirstOrDefault(x => x.deviceId == Id);
            if (deviceData== null)
            {
                return NotFound();
            }
            return PartialView("_DeviceData", deviceData);
        }

        public IActionResult Devices()
        {
            IEnumerable<Device> deviceList = _context.Devices;
            return View(deviceList);
        }


        public IActionResult CreateDevice()
        {
            return View();
        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                var isExistsImei = _context.Devices.SingleOrDefault(d => d.IMEI == device.IMEI);
                if (isExistsImei == null)
                {
                    _context.Devices.Add(device);
                    _context.SaveChanges();
                    return RedirectToAction("Devices");
                }
                else
                {
                    ModelState.AddModelError("IMEI","Устройство с таким IMEI уже существует");
                    return View(device);
                }
                
            }
            return View(device);
        }
        //Get delete
        public IActionResult Delete(int? id)
        {
           if (id == null || id == 0)
           {
               return NotFound();
           }
            var obj = _context.Devices.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Devices.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Devices.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Devices");
        }


        //Get Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.Devices.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Device device)
        {
            if (ModelState.IsValid)
            {
                var isExistsImei = _context.Devices.SingleOrDefault(d => d.IMEI == device.IMEI);
                if (isExistsImei == null)
                {
                    _context.Devices.Update(device);
                    _context.SaveChanges();
                    return RedirectToAction("Devices");
                }
                else
                {
                    ModelState.AddModelError("IMEI", "Устройство с таким IMEI уже существует");
                    return View(device);
                }

            }
            return View(device);
        }

        [HttpGet]
        public IActionResult UpdatingRegister(int? Id)
        {
            var DeviceDataSkz = _context.DataSKZ.Include(d => d.device).FirstOrDefault(x => x.deviceId == Id);
            if (DeviceDataSkz == null)
            {
                return NotFound();
            }
            RegisterUpdateFormData DeviceRegisters = new RegisterUpdateFormData();
            DeviceRegisters.Id = DeviceDataSkz.Id;
            DeviceRegisters.SetOutputCurrent = DeviceDataSkz.SetOutputCurrent;
            DeviceRegisters.SetSumPotential = DeviceDataSkz.SetSumPotential;
            DeviceRegisters.SetPolPotential = DeviceDataSkz.SetPolPotential;
            DeviceRegisters.SetStabControlMode = DeviceDataSkz.SetStabControlMode;
            DeviceRegisters.DistantPowerControl = DeviceDataSkz.DistantPowerControl;




            return View(DeviceRegisters);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatingRegister(RegisterUpdateFormData model)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => {
                   
                    List<Byte> RequestHoldeingRegList = new List<Byte>(new byte[] { 0x01, 0x10, 0x00, 0x81, 0x00, 0x04, 0x08 });
                    AddReg((UInt16)model.SetOutputCurrent, ref RequestHoldeingRegList);
                    AddReg((Int16)model.SetSumPotential, ref RequestHoldeingRegList);
                    AddReg((Int16)model.SetPolPotential, ref RequestHoldeingRegList);
                    AddReg((UInt16)model.SetStabControlMode, ref RequestHoldeingRegList);

                    List<Byte> RequestCoilRegList = new List<Byte>(new byte[] { 0x01, 0x05, 0x00, 0x81 });
                    AddReg(model.DistantPowerControl, ref RequestCoilRegList);
                    
                    Byte[] HoldingReg = RequestGeneration(RequestHoldeingRegList);
                    Byte[] CoilReg = RequestGeneration(RequestCoilRegList);

                    //Console.WriteLine(BitConverter.ToString(HoldingReg));
                    //Console.WriteLine(BitConverter.ToString(CoilReg));
                    
                    _mediator.SendingRequest(model.Id, HoldingReg);
                    _mediator.SendingRequest(model.Id, CoilReg);
                });
                return Ok();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatingRegisterCustom(RegisterUpdateFormData model)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => {

                    var list = Enumerable.Range(0, model.ADU.Length / 2).Select(i => model.ADU.Substring(i * 2, 2)).ToList();
                    string request = string.Join(" ", list);
                    Byte[] buffer = request.Split(' ').Select(item => Convert.ToByte(item, 16)).ToArray();

                    _mediator.SendingRequest(model.Id, buffer);
                });
                return Ok();
            }
            return View(model);
        }

        public void AddReg(Int16 Param, ref List<Byte> RequestHoldeingRegList)
        {
            byte Hi = (byte)(Param >> 8);
            byte Lo = (byte)(Param & 0xFF);
            RequestHoldeingRegList.Add(Hi);
            RequestHoldeingRegList.Add(Lo);
        }
        public void AddReg(UInt16 Param, ref List<Byte> RequestHoldeingRegList)
        {
            byte Hi = (byte)(Param >> 8);
            byte Lo = (byte)(Param & 0xFF);
            RequestHoldeingRegList.Add(Hi);
            RequestHoldeingRegList.Add(Lo);
        }
        public void AddReg(bool Param, ref List<Byte> RequestHoldeingRegList)
        {
            byte Hi;
            if (Param)
            {
                Hi = 0xFF;
            }
            else
            {
                Hi = 0x00;
            }
            RequestHoldeingRegList.Add(Hi);
            RequestHoldeingRegList.Add(0x00);
        }

        public Byte[] RequestGeneration(List<Byte> RequsetByteList)
        {
            Byte[] RequestWithoutCrc = RequsetByteList.ToArray();
            UInt16 CRC = MBData.ModRTU_CRC(RequestWithoutCrc, RequestWithoutCrc.Length);// В результате вычисления Crc байты изменины местами
            byte CRC_Hi = (byte)(CRC >> 8);
            byte CRC_Lo = (byte)(CRC & 0xff);
            RequsetByteList.Add(CRC_Lo);
            RequsetByteList.Add(CRC_Hi);
            return RequsetByteList.ToArray();
        }
    }
}
