using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProjectPRNver2._0.EF
{
    public partial class ProjectPRNDbContext : DbContext
    {
        public ProjectPRNDbContext()
            : base("name=ProjectPRN")
        {
        }

        public virtual DbSet<Book_Authors> Book_Authors { get; set; }
        public virtual DbSet<Book_Category> Book_Category { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Authors>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Book_Authors)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book_Category>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Book_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.BookPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.WishLists)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Role>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPhone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserMail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WishLists)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
