using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("Languages")]
    public partial class Languages
    {
        public Languages()
        {

            ProjectsLangs = new HashSet<ProjectsLang>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }

        public ICollection<ProjectsLang> ProjectsLangs { get; set; }

    }
}
