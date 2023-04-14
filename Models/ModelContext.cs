using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FURNITURE.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutUsZ> AboutUsZs { get; set; }
        public virtual DbSet<BankZ> BankZs { get; set; }
        public virtual DbSet<CategoryZ> CategoryZs { get; set; }
        public virtual DbSet<ContactUsZ> ContactUsZs { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<HomePageZ> HomePageZs { get; set; }
        public virtual DbSet<LoginZ> LoginZs { get; set; }
        public virtual DbSet<OrderZ> OrderZs { get; set; }
        public virtual DbSet<PaymentZ> PaymentZs { get; set; }
        public virtual DbSet<ProductOrderZ> ProductOrderZs { get; set; }
        public virtual DbSet<ProductZ> ProductZs { get; set; }
        public virtual DbSet<RoleZ> RoleZs { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<UserAccountZ> UserAccountZs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR17_User87;PASSWORD=Testyaman87;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER87")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<AboutUsZ>(entity =>
            {
                entity.ToTable("ABOUT_US_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Paragraph1)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH1");

                entity.Property(e => e.Paragraph2)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH2");

                entity.Property(e => e.Paragraph3)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH3");
            });

            modelBuilder.Entity<BankZ>(entity =>
            {
                entity.ToTable("BANK_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");
            });

            modelBuilder.Entity<CategoryZ>(entity =>
            {
                entity.ToTable("CATEGORY_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<ContactUsZ>(entity =>
            {
                entity.ToTable("CONTACT_US_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("GALLERY");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<HomePageZ>(entity =>
            {
                entity.ToTable("HOME_PAGE_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Image)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Image2)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE2");

                entity.Property(e => e.Logo)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("LOGO");

                entity.Property(e => e.Paragraph)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH");

                entity.Property(e => e.Phone)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONE");

                entity.Property(e => e.Text1)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");
            });

            modelBuilder.Entity<LoginZ>(entity =>
            {
                entity.ToTable("LOGIN_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Passwordd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginZs)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ROLE_ID_Z_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginZs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_ID_Z_FK");
            });

            modelBuilder.Entity<OrderZ>(entity =>
            {
                entity.ToTable("ORDER_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderZs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_ID_Z");
            });

            modelBuilder.Entity<PaymentZ>(entity =>
            {
                entity.ToTable("PAYMENT_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.PayDate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAY_DATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentZs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_ID_Z_PAYMENT");
            });

            modelBuilder.Entity<ProductOrderZ>(entity =>
            {
                entity.ToTable("PRODUCT_ORDER_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrderZs)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ORDER_ID_Z");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrderZs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCT_ID_Z");
            });

            modelBuilder.Entity<ProductZ>(entity =>
            {
                entity.ToTable("PRODUCT_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Image)
                    .HasMaxLength(220)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("NAME_");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Value)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VALUE_");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductZs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("CATEGORY_ID_Z_FK");
            });

            modelBuilder.Entity<RoleZ>(entity =>
            {
                entity.ToTable("ROLE_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICES");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USER_ID_Z_TESTI");
            });

            modelBuilder.Entity<UserAccountZ>(entity =>
            {
                entity.ToTable("USER_ACCOUNT_Z");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Phone)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONE");
            });

            modelBuilder.HasSequence("EMP_SEQ").IncrementsBy(5);

            modelBuilder.HasSequence("EMPLOYEE_SEQ").IncrementsBy(2);

            modelBuilder.HasSequence("YAMAN").IncrementsBy(2);

            modelBuilder.HasSequence("YAMANN").IncrementsBy(2);

            modelBuilder.HasSequence("YAMANNN").IncrementsBy(2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
