using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Messaggio
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato dell'email non valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il messaggio è obbligatorio")]
        public string TestoMessaggio { get; set; }

        public Messaggio() { }
    }
}
