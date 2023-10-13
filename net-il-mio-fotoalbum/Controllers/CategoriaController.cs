using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            List<Categoria> categorie = new List<Categoria>();

            try
            {
                using (FotoContext db = new FotoContext())
                {
                    categorie = db.Categorie.ToList<Categoria>();

                    return View("Index", categorie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using(FotoContext db = new FotoContext())
            {
                Categoria? categoriaTrovata = db.Categorie.Where(categoria => categoria.Id == id).FirstOrDefault();

                if(categoriaTrovata == null)
                {
                    return NotFound($"La categoria non è stata trovata");
                }
                else
                {
                    return View("Details", categoriaTrovata);
                }
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            Categoria nuovaCategoria = new Categoria();

            return View("Create", nuovaCategoria);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            using(FotoContext db = new FotoContext())
            {
                db.Categorie.Add(categoria);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            using(FotoContext db = new FotoContext())
            {
                Categoria categoriaDaModificare = db.Categorie.Where(categoria => categoria.Id == id).FirstOrDefault();

                if(categoriaDaModificare == null)
                {
                    return NotFound("La categoria che vuoi modificare non è stata trovata");
                }
                else
                {
                    Categoria categoria = categoriaDaModificare;

                    return View("Update", categoria);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Categoria categoria)
        {
            using(FotoContext db = new FotoContext())
            {
                Categoria categoriaDaModificare = db.Categorie.Where(categoria => categoria.Id == id).FirstOrDefault();

                if(categoriaDaModificare != null)
                {
                    categoriaDaModificare.Nome = categoria.Nome;

                    db.SaveChanges();

                    return RedirectToAction("Index");   
                }
                else
                {
                    return NotFound("Non è stata trovata la categoria da aggiornare");
                }
            }       
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using(FotoContext db = new FotoContext())
            {
                Categoria categoriaDaEliminare = db.Categorie.Where(categoria => categoria.Id == id).FirstOrDefault();

                if(categoriaDaEliminare != null)
                {
                    db.Categorie.Remove(categoriaDaEliminare);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La categoria da eliminare non è stata trovata");
                }
            }
        }

    }
}
