using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class FotoFormModel
    {
        public Foto? Foto { get; set; }
        public IFormFile? FotoFormFile { get; set; }

        //proprietà per select multipla
        public List<SelectListItem>? Categorie { get; set; }
        public List<string>? CategorieSelezionateId { get; set; }

    }
}
