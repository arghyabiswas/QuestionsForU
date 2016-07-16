namespace QuestionsForU.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Prashnapatra : DbContext
    {
        public Prashnapatra()
            : base("name=PrashnapatraConnectionString")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Categoriy> Categoriys { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UploadedImage> UploadedImages { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<RecentComment> RecentComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Categoriy>()
                .HasMany(e => e.BookCategories)
                .WithRequired(e => e.Categoriy)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoriy>()
                .HasMany(e => e.Categoriy1)
                .WithOptional(e => e.Categoriy2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Categoriy>()
                .HasMany(e => e.QuestionCategories)
                .WithOptional(e => e.Categoriy)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Chapter>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Chapter>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Chapter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<History>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<History>()
                .HasMany(e => e.Categoriys)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<History>()
                .HasMany(e => e.Chapters)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<History>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<History>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<History>()
                .HasMany(e => e.UploadedImages)
                .WithRequired(e => e.History)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Histories)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Rating)
                .HasPrecision(5, 2);

            modelBuilder.Entity<QuestionType>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.QuestionType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Setting>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UploadedImage>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            modelBuilder.Entity<UploadedImage>()
                .Property(e => e.ImageExtention)
                .IsUnicode(false);

            modelBuilder.Entity<UploadedImage>()
                .Property(e => e.ProcessValue)
                .IsUnicode(false);

            modelBuilder.Entity<UploadedImage>()
                .HasMany(e => e.UploadedImages1)
                .WithOptional(e => e.UploadedImage1)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Log>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Exception)
                .IsUnicode(false);
        }
    }
}
