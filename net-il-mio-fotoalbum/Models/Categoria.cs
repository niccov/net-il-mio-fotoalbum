using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio")]
        [MaxLength(100, ErrorMessage = "La massima lunghezza del nome è di 100 caratteri")]
        public string Nome { get; set; }

        public List<Foto>? Fotos { get; set; }

        public Categoria() { }
    }
}
