using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("Tags")]
    public partial class Tags
    {
        public Tags()
        {

            PostTags = new HashSet<PostTags>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public ICollection<PostTags> PostTags { get; set; }

    }
}
