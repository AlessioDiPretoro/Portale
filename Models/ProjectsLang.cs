using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("ProjectsLang")]
    public partial class ProjectsLang
    {
        public int Id { get; set; }
        public int LanguagesId {  get; set; }
        public int ProjectsId {  get; set; }

        public virtual Languages Languages { get; set; } = null!;
        public virtual Projects Projects { get; set; } = null!;
    }
}
