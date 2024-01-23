using Microsoft.AspNetCore.Identity;

namespace Portale.Models
{
    public partial class Projects
    {
        public Projects() {

            ProjectsImgs = new HashSet<ProjectsImgs>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Github { get; set; }
        public string? UrlDeploy { get; set; }

        public virtual ICollection<ProjectsImgs> ProjectsImgs { get; set; }
    }
}
