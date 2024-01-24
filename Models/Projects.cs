using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("Projects")]
    public partial class Projects
    {
        public Projects()
        {

            ProjectsImgs = new HashSet<ProjectsImgs>();
            ProjectsLangs = new HashSet<ProjectsLang>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Github { get; set; }
        public string? UrlDeploy { get; set; }

        public virtual ICollection<ProjectsImgs> ProjectsImgs { get; set; }
        public virtual ICollection<ProjectsLang> ProjectsLangs { get; set; }
    }
}
