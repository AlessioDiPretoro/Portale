using Microsoft.AspNetCore.Identity;
using Portale.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string? Nation { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Cap { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId ")]

        public virtual UserConnector User { get; set; }

        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}
