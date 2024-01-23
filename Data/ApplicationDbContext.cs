using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portale.Models;

namespace Portale.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
        public virtual DbSet<Projects> Projects { get; set; } = null!;
        public virtual DbSet<ProjectsImgs> ProjectsImgs { get; set; } = null!;


    }
}
