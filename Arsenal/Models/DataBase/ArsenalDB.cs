namespace Arsenal.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ArsenalDB : DbContext
    {
        public ArsenalDB()
            : base("name=ArsenalDB")
        {
        }

        public virtual DbSet<delivery> delivery { get; set; }
        public virtual DbSet<history> history { get; set; }
        public virtual DbSet<inventarisation> inventarisation { get; set; }
        public virtual DbSet<issue> issue { get; set; }
        public virtual DbSet<item> item { get; set; }
        public virtual DbSet<loader> loader { get; set; }
        public virtual DbSet<_operator> _operator { get; set; }
        public virtual DbSet<storage> storage { get; set; }
        public virtual DbSet<type> type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<history>()
                .Property(e => e.Actions)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Properities)
                .IsUnicode(false);

            modelBuilder.Entity<item>()
                .Property(e => e.Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<loader>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<loader>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<loader>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<_operator>()
                .HasMany(e => e.history)
                .WithRequired(e => e._operator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<storage>()
                .HasMany(e => e.delivery)
                .WithRequired(e => e.storage)
                .HasForeignKey(e => new { e.Storage_ID, e.Item_ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<storage>()
                .HasMany(e => e.inventarisation)
                .WithRequired(e => e.storage)
                .HasForeignKey(e => new { e.Storage_ID, e.Item_ID });

            modelBuilder.Entity<storage>()
                .HasMany(e => e.issue)
                .WithRequired(e => e.storage)
                .HasForeignKey(e => new { e.Storage_ID, e.Item_ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<type>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<type>()
                .HasMany(e => e.item)
                .WithRequired(e => e.type)
                .HasForeignKey(e => e.Type_ID);
        }
    }
}
