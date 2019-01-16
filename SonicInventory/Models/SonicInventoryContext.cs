namespace SonicInventory.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SonicInventoryContext : DbContext
    {
        public SonicInventoryContext()
            : base("name=SonicInventory")
        {
        }

        public virtual DbSet<designer> Designers { get; set; }
        public virtual DbSet<fabricator> Fabricators { get; set; }
        public virtual DbSet<part> Parts { get; set; }
        public virtual DbSet<project> Projects { get; set; }
        public virtual DbSet<status> Status { get; set; }
        public virtual DbSet<subAssembly> SubAssemblies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<designer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<fabricator>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<part>()
                .Property(e => e.drawingNo)
                .IsUnicode(false);

            modelBuilder.Entity<part>()
                .Property(e => e.drawingType)
                .IsUnicode(false);

            modelBuilder.Entity<part>()
                .Property(e => e.detailNo)
                .IsUnicode(false);

            modelBuilder.Entity<part>()
                .Property(e => e.revNo)
                .IsUnicode(false);

            //modelBuilder.Entity<part>()
            //    .Property(e => e.designer_id)
            //    .IsUnicode(false);

            //modelBuilder.Entity<part>()
            //    .Property(e => e.fabricator_id)
            //    .IsUnicode(false);

            modelBuilder.Entity<part>()
                .Property(e => e.file)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<subAssembly>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<subAssembly>()
                .Property(e => e.Abbreviation)
                .IsUnicode(false);
        }
    }
}
