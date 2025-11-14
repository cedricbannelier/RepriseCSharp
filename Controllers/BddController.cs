using Microsoft.AspNetCore.Mvc;
using MonSiteMvc.Services;
using MonSiteMvc.Models;

namespace MonSiteMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BddController : ControllerBase
    {
        private readonly IGestionBdd _gestionBdd;
        public BddController(IGestionBdd gestionBdd) => _gestionBdd = gestionBdd;

        // POST api/bdd/test
        [HttpPost("test")]
        public async Task<IActionResult> TesterConnexion()
        {
            //Délai artificiel pour faire durer le chargement
            await Task.Delay(1000); // 2000 ms = 2 secondes

            var (succes, message) = await _gestionBdd.TesterConnexionAsync();
            return Ok(new { succes, message });
        }

        // POST api/bdd/insert
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] InsertDto dto)
        {
            if (dto == null) return BadRequest(new { succes = false, message = "Données manquantes" });

            var (succes, message) = await _gestionBdd.InsertAsync(dto);
            if (succes) return Ok(new { succes, message });
            return BadRequest(new { succes, message });
        }
    }
}
