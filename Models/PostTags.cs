using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("PostTags")]
    public partial class PostTags
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int PostId { get; set; }

        public virtual Tags Tags { get; set; } = null!;
        public virtual Posts Posts { get; set; } = null!;
    }
}
