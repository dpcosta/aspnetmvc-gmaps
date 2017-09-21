using Alura.GoogleMaps.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Alura.GoogleMaps.Web.Geocoding;

namespace Alura.GoogleMaps.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //coordenadas quaisquer para mostrar o mapa
            var coordenadas = new Coordenada("São Paulo", "-23.64340873969638", "-46.730219057147224");
            return View(coordenadas);
        }

        public JsonResult Localizar(HomeViewModel model)
        {
            Debug.WriteLine(model);

            //usar geocoding para obter as coordenadas da localização:
            Coordenada coordLocal = ObterCoordenadasDaLocalizacao(model.Endereco);

            //buscar no banco de dados os aeroportos próximos daquela localização
            //atualmente está gerando uma lista fixa
            var coordenadas = new List<Coordenada>
            {
                coordLocal,
                new Coordenada("Santos Dumont", "-22.9111438", "-43.1670642"),
                new Coordenada("Galeão", "-22.8134098", "-43.251612")

            };
            return Json(coordenadas);
        }

        private Coordenada ObterCoordenadasDaLocalizacao(string endereco)
        {
            string url = $"http://maps.google.com/maps/api/geocode/json?address={endereco}";
            Debug.WriteLine(url);

            var coord = new Coordenada("Não Localizado", "-10", "-10");
            var response = new WebClient().DownloadString(url);
            var googleGeocode = JsonConvert.DeserializeObject<GoogleGeocodeResponse>(response);
            //Debug.WriteLine(googleGeocode);

            if (googleGeocode.status == "OK")
            {
                coord.Nome = googleGeocode.results[0].formatted_address;
                coord.Latitude = googleGeocode.results[0].geometry.location.lat;
                coord.Longitude = googleGeocode.results[0].geometry.location.lng;
            }

            return coord;
        }
    }
}