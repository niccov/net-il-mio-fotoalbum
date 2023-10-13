using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotoApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFotos()
        {
            using (FotoContext db = new FotoContext())
            {
                List<Foto> fotos = db.Fotos.Include(foto => foto.Categorie).ToList();

                if (fotos == null)
                {
                    return NotFound(new { message = "Nessuna foto trovata." });
                }
                return Ok(fotos);


            }
        }

        [HttpGet]
        public IActionResult GetFotosByTitle(string? search)
        {

            if (search == null)
            {
                return GetFotos();
            }
            else
            {
                using (FotoContext db = new FotoContext())
                {
                    List<Foto>? fotos = db.Fotos.Include(foto => foto.Categorie).Where(foto => foto.Titolo.ToLower().Contains(search.ToLower())).ToList();

                    if (fotos == null)
                    {
                        return NotFound(new { message = "Non ho trovato pizze con questo nome" });
                    }
                    return Ok(fotos);
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Foto newFoto)
        {
            using (FotoContext db = new FotoContext())
            {
                try
                {
                    db.Fotos.Add(newFoto);
                    db.SaveChanges();

                    return Ok();

                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Modify(int id, [FromBody] Foto updatedFoto)
        {
            using (FotoContext db = new FotoContext())
            {
                Foto? fotoToUpdate = db.Fotos.Where(Foto => Foto.Id == id).FirstOrDefault();

                if (fotoToUpdate == null)
                {
                    return NotFound();
                }

                fotoToUpdate.Titolo = updatedFoto.Titolo;
                fotoToUpdate.Descrizione = updatedFoto.Descrizione;
                fotoToUpdate.FotoUrl = updatedFoto.FotoUrl;
                fotoToUpdate.Visibilità = updatedFoto.Visibilità;
                fotoToUpdate.Categorie = updatedFoto.Categorie;

                db.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (FotoContext db = new FotoContext())
            {
                Foto? fotoDaCancellare = db.Fotos.Where(foto => foto.Id == id).FirstOrDefault();

                if (fotoDaCancellare == null)
                {
                    return NotFound();
                }

                db.Fotos.Remove(fotoDaCancellare);
                db.SaveChanges();

                return Ok();
            }
        }
    }
}
