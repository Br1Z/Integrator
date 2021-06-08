using IntegratorEHZ.App_Data;
using IntegratorEHZ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TestController(IDbContextFactory<ApplicationDBContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        // GET: TestController
        public ActionResult Index()
        {
            IEnumerable<Device> objList = _context.Devices.OrderBy(sort => sort.Id);
            return View(objList);
        }
        [HttpGet]
        public ActionResult Search(string IMEI, string Protocol)
        {          
            //Console.WriteLine("IMEI", "SKZ");
            var SearchList = from m in _context.Devices select m;
            if (!String.IsNullOrEmpty(IMEI))
            {
                SearchList = SearchList.Where(s => s.IMEI.Contains(IMEI));
            }
            if (!String.IsNullOrEmpty(Protocol))
            {
                SearchList = SearchList.Where(s => s.Internal_Protocol.Contains(Protocol));
            }
            return PartialView("_Search",SearchList);
            //return PartialView("_Search");

        }

    }
}
