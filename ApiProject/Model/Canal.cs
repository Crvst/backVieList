using System.ComponentModel.DataAnnotations;

namespace ApiProject.Model
{
    public class Canal
    {
        [Key]
        public int Id { get; set; }
        public string url { get; set; }
        public string icon_url { get; set; }
        public string nombre { get; set; }
        public string group_title { get; set; }
        public ICollection<PlaylistCanales> PlaylistCanales { get; set; }
    }
}
