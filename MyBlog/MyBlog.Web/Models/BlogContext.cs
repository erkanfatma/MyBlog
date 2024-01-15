using Microsoft.EntityFrameworkCore;

namespace MyBlog.Web.Models {
    public class BlogContext : DbContext {
        public BlogContext() { }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(
            "Server=DESKTOP-62BQC87\\SQLEXPRESS;Database=BlogDb;" +
            "TrustServerCertificate=True;Trusted_Connection=True;Encrypt=False"
               );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ArticleTag>()
                .HasKey(x => new { x.ArticleId, x.TagId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
