using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessaggioController : Controller
    {
        public IActionResult Index()
        {
            using(FotoContext db = new FotoContext())
            {
                List<Messaggio> messaggi = db.Messaggi.ToList();

                return View(messaggi);
            }
        }
    }
}
