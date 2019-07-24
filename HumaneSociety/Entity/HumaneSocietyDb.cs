using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HumaneSociety.Entity
{
    public partial class HumaneSocietyDb : DbContext
    {
        public HumaneSocietyDb()
        {
        }

        public HumaneSocietyDb(DbContextOptions<HumaneSocietyDb> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Adoptions> Adoptions { get; set; }
        public virtual DbSet<AnimalShots> AnimalShots { get; set; }
        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<DietPlans> DietPlans { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Shots> Shots { get; set; }
        public virtual DbSet<USStates> Usstates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=HumaneSociety;User Id=sa;Password=Passw0rd**");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__Addresse__091C2AFB1A1B27A0");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsstateId).HasColumnName("USStateId");

                entity.HasOne(d => d.Usstate)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UsstateId)
                    .HasConstraintName("FK__Addresses__USSta__4BAC3F29");
            });

            modelBuilder.Entity<Adoptions>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.AnimalId })
                    .HasName("AdoptionId");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Adoptions)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Adoptions__Anima__52593CB8");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Adoptions)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Adoptions__Clien__5165187F");
            });

            modelBuilder.Entity<AnimalShots>(entity =>
            {
                entity.HasKey(e => new { e.AnimalId, e.ShotId })
                    .HasName("AnimalShotId");

                entity.Property(e => e.DateReceived).HasColumnType("date");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.AnimalShots)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnimalSho__Anima__45F365D3");

                entity.HasOne(d => d.Shot)
                    .WithMany(p => p.AnimalShots)
                    .HasForeignKey(d => d.ShotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnimalSho__ShotI__46E78A0C");
            });

            modelBuilder.Entity<Animals>(entity =>
            {
                entity.HasKey(e => e.AnimalId)
                    .HasName("PK__Animals__A21A73076987CAD2");

                entity.Property(e => e.AdoptionStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Demeanor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Animals__Categor__3C69FB99");

                entity.HasOne(d => d.DietPlan)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.DietPlanId)
                    .HasConstraintName("FK__Animals__DietPla__3D5E1FD2");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Animals__Employe__3E52440B");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Categori__19093A0B930A6916");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PK__Clients__E67E1A24D3F1ADF1");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Clients__Address__4E88ABD4");
            });

            modelBuilder.Entity<DietPlans>(entity =>
            {
                entity.HasKey(e => e.DietPlanId)
                    .HasName("PK__DietPlan__D256E10A8A3FAF77");

                entity.Property(e => e.FoodType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Employee__7AD04F11334BD1EE");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__Rooms__3286393942480C3C");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__Rooms__AnimalId__412EB0B6");
            });

            modelBuilder.Entity<Shots>(entity =>
            {
                entity.HasKey(e => e.ShotId)
                    .HasName("PK__Shots__66C5A78F6B6B5E86");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usstates>(entity =>
            {
                entity.HasKey(e => e.UsstateId)
                    .HasName("PK__USStates__C574BEDD200CE911");

                entity.ToTable("USStates");

                entity.Property(e => e.UsstateId).HasColumnName("USStateId");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
