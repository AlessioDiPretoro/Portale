using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

        public virtual DbSet<Posts> Posts { get; set; } = null!;
        public virtual DbSet<PostTags> PostTags { get; set; } = null!;
        public virtual DbSet<PostImgs> PostImgs { get; set; } = null!;
        public virtual DbSet<Tags> Tags { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfo { get; set; } = null!;

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


            modelBuilder.Entity<PostTags>()
                .HasKey(pl => new { pl.PostId, pl.TagId });

            modelBuilder.Entity<PostTags>()
                .HasOne(pl => pl.Posts)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pl => pl.PostId);

            modelBuilder.Entity<PostTags>()
                .HasOne(pl => pl.Tags)
                .WithMany(l => l.PostTags)
            .HasForeignKey(pl => pl.TagId);

            modelBuilder.Entity<Posts>()
            .HasOne(p => p.UserInfo)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
