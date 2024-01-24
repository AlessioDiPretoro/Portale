using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
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
        public virtual DbSet<Languages> Languages { get; set; } = null!;
        public virtual DbSet<ProjectsLang> ProjectsLang { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectsLang>()
                .HasKey(pl => new { pl.ProjectsId, pl.LanguagesId });

            modelBuilder.Entity<ProjectsLang>()
                .HasOne(pl => pl.Projects)
                .WithMany(p => p.ProjectsLangs)
                .HasForeignKey(pl => pl.ProjectsId);

            modelBuilder.Entity<ProjectsLang>()
                .HasOne(pl => pl.Languages)
                .WithMany(l => l.ProjectsLangs)
                .HasForeignKey(pl => pl.LanguagesId);
        }
    }
}
