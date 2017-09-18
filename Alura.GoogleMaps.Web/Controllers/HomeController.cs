using Alura.GoogleMaps.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alura.GoogleMaps.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var coordenadas = new Coordenada
            {
                Latitude = "-23.64340873969638",
                Longitude = "-46.730219057147224"
            };
            return View(coordenadas);
        }
    }
}