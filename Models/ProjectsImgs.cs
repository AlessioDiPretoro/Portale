using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("ProjectsImgs")]
    public partial class ProjectsImgs
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int ProjectsId { get; set; }

        public virtual Projects Projects { get; set; } = null!;
    }
}
