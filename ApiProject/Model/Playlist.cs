using System.ComponentModel.DataAnnotations;

namespace ApiProject.Model
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Id_Usuario { get; set; }
        public ICollection<PlaylistCanales> PlaylistCanales { get; set; }

    }
}
