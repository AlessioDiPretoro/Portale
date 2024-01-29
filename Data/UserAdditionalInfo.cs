using Microsoft.AspNetCore.Identity;
using Portale.Models;
namespace Portale.Data
{
    public class UserAdditionalInfo : IdentityUser
    {
        public string? Nation { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Cap { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}
