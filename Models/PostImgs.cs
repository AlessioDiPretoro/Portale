using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("PostImgs")]
    public partial class PostImgs
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int PostId { get; set; }

        public virtual Posts Posts { get; set; } = null!;
    }
}
