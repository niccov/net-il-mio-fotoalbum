using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("foto")]
    public class Foto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("titolo")]
        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il titolo non può avere più di 50 caratteri")]
        public string Titolo { get; set; }

        [Column("descrizione")]
        [DefaultValue("")]
        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [StringLength(200, ErrorMessage = "La descriz<ione non può avere più di 200 caratteri")]
        public string? Descrizione { get; set; }

        public string? FotoUrl {  get; set; }

        public byte[]? FotoFile { get; set; }

        public string FotoSrc => FotoFile is null ? (FotoUrl is null ? "" : FotoUrl) : $"data:image/png;base64,{Convert.ToBase64String(FotoFile)}";

        [Required(ErrorMessage = "La visibilità è obbligatoria")]
        public bool Visibilità {  get; set; }

        public List<Categoria> Categorie {  get; set; }

        //costruttore
        public Foto() { }

    }


}
