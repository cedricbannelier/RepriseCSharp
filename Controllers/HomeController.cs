using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonSiteMvc.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration; // obligatoire pour IConfiguration
using MySqlConnector;
using MonSiteMvc.Services;

namespace MonSiteMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGestionBdd _gestionBdd;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IGestionBdd gestionBdd)
        {
            _logger = logger;
            _configuration = configuration;
            _gestionBdd = gestionBdd;
        }
        
        // GET : affiche la page (avec layout / menu / bouton)
        [HttpGet]
        public IActionResult ConnexionBdd()
        {
            return View("ConnexionBdd");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perso()
        {
            var random = new Random();
            var model = new NombreViewModel
            {
                NombreAleatoire = random.Next(1, 101)
            }; return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
