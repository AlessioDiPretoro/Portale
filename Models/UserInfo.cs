using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Portale.Models
{
    public class UserInfo
    {
        public UserInfo() {

            Posts = new HashSet<Posts>();
        }
        [Key]
        public int Id { get; set; }
        public string? Nation { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Cap { get; set; }

        public string IdentityId { get; set; } = null!;
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
