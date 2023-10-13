using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "USER,ADMIN")]
    public class FotoController : Controller
    {
        public IActionResult Index()
        {
            List<Foto> fotos = new List<Foto>();

            try
            {
                using (FotoContext db = new FotoContext())
                {
                    fotos = db.Fotos.Include(foto => foto.Categorie).ToList<Foto>();
                    return View("Index", fotos);

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
            using (FotoContext db = new FotoContext())
            {
                Foto? fotoTrovata = db.Fotos.Where(foto => foto.Id == id).Include(foto => foto.Categorie).FirstOrDefault();

                if (fotoTrovata == null)
                {
                    return NotFound($"La foto non è stata trovata");
                }
                else
                {
                    return View("Details", fotoTrovata);
                }
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using(FotoContext db = new FotoContext())
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Categoria> databaseAllCategories = db.Categorie.ToList();

                foreach(Categoria categoria in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem { Text = categoria.Nome, Value = categoria.Id.ToString() });
                }

                FotoFormModel model = new FotoFormModel();
                model.Foto = new Foto();
                model.Categorie = allCategoriesSelectList;
                return View("Create", model);
            }
            
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
               using (FotoContext db = new FotoContext())
               {
                    List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                    List<Categoria> databaseAllCategories = db.Categorie.ToList();

                    foreach (Categoria categoria in databaseAllCategories)
                    {
                        allCategoriesSelectList.Add(new SelectListItem { Text = categoria.Nome, Value = categoria.Id.ToString() });
                    }

                    data.Categorie = allCategoriesSelectList;

                    return View("Create", data);
                   
               }
            }
            using (FotoContext db = new FotoContext())
            {
                Foto FotoToCreate = new Foto();
                FotoToCreate.Titolo = data.Foto.Titolo;
                FotoToCreate.Descrizione = data.Foto.Descrizione;
                FotoToCreate.FotoUrl = data.Foto.FotoUrl;
                FotoToCreate.Visibilità = data.Foto.Visibilità;

                FotoToCreate.Categorie = new List<Categoria>();


                if(data.CategorieSelezionateId != null)
                {
                    foreach (string selectedCategoriesId in data.CategorieSelezionateId)
                    {
                        int selectedIntCategoriesId = int.Parse(selectedCategoriesId);

                        Categoria? categoria = db.Categorie.Where(m => m.Id == selectedIntCategoriesId).FirstOrDefault();

                        if(categoria != null)
                        {
                            FotoToCreate.Categorie.Add(categoria);
                        }
                    }
                }

                this.SetImageFileFromFormFile(data, FotoToCreate);

                db.Fotos.Add(FotoToCreate);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            using(FotoContext db = new FotoContext())
            {
                Foto? fotoDaModificare = db.Fotos.Where(foto => foto.Id == id).Include(foto => foto.Categorie).FirstOrDefault();

                if(fotoDaModificare == null)
                {
                    return NotFound("La foto che vuoi modificare non è stata trovata");
                }
                else
                {
                    List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                    List<Categoria> databaseAllCategories = db.Categorie.ToList();

                    foreach (Categoria categoria in databaseAllCategories)
                    {
                        allCategoriesSelectList.Add(new SelectListItem
                        {
                            Text = categoria.Nome,
                            Value = categoria.Id.ToString(),
                            Selected = fotoDaModificare.Categorie.Any(categoriaAssociata => categoriaAssociata.Id == categoria.Id)
                        });
                    }

                    FotoFormModel model = new FotoFormModel();
                    model.Foto = fotoDaModificare;
                    model.Categorie = allCategoriesSelectList;
                    return View(model);
                }
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using(FotoContext db = new FotoContext())
                {
                    List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                    List<Categoria> databaseAllCategories = db.Categorie.ToList();

                    foreach (Categoria categoria in databaseAllCategories)
                    {
                        allCategoriesSelectList.Add(new SelectListItem
                        {
                            Text = categoria.Nome,
                            Value = categoria.Id.ToString()
                            
                        });
                    }

                    data.Categorie = allCategoriesSelectList;

                    return View("Update", data);
                }
            }

            using(FotoContext db = new FotoContext())
            {
                Foto? fotoDaModificare = db.Fotos.Where(foto => foto.Id == id).Include(foto => foto.Categorie).FirstOrDefault();

                if(fotoDaModificare != null)
                {
                    fotoDaModificare.Categorie.Clear();

                    if(data.CategorieSelezionateId != null)
                    {
                        foreach (string selectedCategoriesId in data.CategorieSelezionateId)
                        {
                            int selectedIntCategoriesId = int.Parse(selectedCategoriesId);

                            Categoria? categoria = db.Categorie.Where(m => m.Id == selectedIntCategoriesId).FirstOrDefault();

                            if(categoria != null)
                            {
                                fotoDaModificare.Categorie.Add(categoria);
                            }
                        }
                    }


                    fotoDaModificare.Titolo = data.Foto.Titolo;
                    fotoDaModificare.Descrizione = data.Foto.Descrizione;
                    fotoDaModificare.Visibilità = data.Foto.Visibilità;

                    if(data.FotoFormFile != null)
                    {
                        MemoryStream stream = new MemoryStream();
                        data.FotoFormFile.CopyTo(stream);
                        fotoDaModificare.FotoFile = stream.ToArray();
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                    
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using(FotoContext db = new FotoContext())
            {
                Foto fotoDaEliminare = db.Fotos.Where(foto => foto.Id == id).FirstOrDefault();
                if(fotoDaEliminare != null)
                {
                    db.Fotos.Remove(fotoDaEliminare);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }




        private void SetImageFileFromFormFile(FotoFormModel formData, Foto foto)
        {
            if(formData.FotoFormFile == null)
            {
                return;
            }

            MemoryStream stream = new MemoryStream();
            formData.FotoFormFile.CopyTo(stream);
            foto.FotoFile = stream.ToArray();

        }
    }
}
