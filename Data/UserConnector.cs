using Microsoft.AspNetCore.Identity;
using Portale.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Data
{
    public class UserConnector : IdentityUser
    {
        public virtual UserInfo UserInfo { get; set; }

    }
}
