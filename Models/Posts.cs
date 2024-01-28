using Microsoft.AspNetCore.Identity;
using Portale.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    [Table("Posts")]
    public partial class Posts
    {
        public Posts()
        {
            PostImgs = new HashSet<PostImgs>();
            PostTags = new HashSet<PostTags>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<PostImgs> PostImgs { get; set; }
        public virtual ICollection<PostTags> PostTags { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserAdditionalInfo User { get; set; } = null!;
    }
}
