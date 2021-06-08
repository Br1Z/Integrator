using IntegratorEHZ.App_Data;
using IntegratorEHZ.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMediator _mediator;

        public HomeController(IDbContextFactory<ApplicationDBContext> contextFactory, IMediator mediator)
        {
            _context = contextFactory.CreateDbContext();
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DevicesList()
        {
            var List = _context.Devices.OrderBy(x => x.Id);
            return PartialView("_DevicesList",List);
        }
    }
}
