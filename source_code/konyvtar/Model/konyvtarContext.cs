using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace konyvtar.Model
{
    public partial class konyvtarContext : DbContext
    {
        public konyvtarContext()
        {
        }

        public konyvtarContext(DbContextOptions<konyvtarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;database=konyvtar");

                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("nationality");

                entity.Property(e => e.Pob)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("pob");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.HasIndex(e => e.Authorid, "authorid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("int(11)")
                    .HasColumnName("amount");

                entity.Property(e => e.Authorid)
                    .HasColumnType("int(11)")
                    .HasColumnName("authorid");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(10000)")
                    .HasColumnName("description");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("genre");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("img");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("isbn");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("language");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("publisher");

                entity.Property(e => e.Releasedate)
                    .HasColumnType("date")
                    .HasColumnName("releasedate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("book_ibfk_1");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");

                entity.HasIndex(e => e.Bookid, "bookid");

                entity.HasIndex(e => e.Bookid, "bookid_2");

                entity.HasIndex(e => e.Userid, "userid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bookid)
                    .HasColumnType("int(11)")
                    .HasColumnName("bookid");

                entity.Property(e => e.Enddate)
                    .HasColumnType("date")
                    .HasColumnName("enddate");

                entity.Property(e => e.Startdate)
                    .HasColumnType("date")
                    .HasColumnName("startdate");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Bookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_ibfk_1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("password");

                entity.Property(e => e.Privilige)
                    .HasColumnType("int(11)")
                    .HasColumnName("privilige");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("email");

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("idcard");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
