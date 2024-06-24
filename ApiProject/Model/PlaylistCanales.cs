using System.ComponentModel.DataAnnotations;

namespace ApiProject.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization; // Importa este espacio de nombres

    public class PlaylistCanales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlaylistId { get; set; }

        [Required]
        public int CanalId { get; set; }

        [ForeignKey("PlaylistId")]
        [JsonIgnore] // Esta propiedad no será serializada
        public Playlist? Playlist { get; set; }

        [ForeignKey("CanalId")]
        [JsonIgnore] // Esta propiedad no será serializada
        public Canal? Canal { get; set; }
    }

}
