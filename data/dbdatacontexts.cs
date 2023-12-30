using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.models;

namespace Vaccinatedapi.data
{
    public class dbdatacontexts:DbContext
    {
        public dbdatacontexts(DbContextOptions<dbdatacontexts> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<hospital>()
                .HasOne(e => e.hospital_Type)
                .WithMany(e => e.hospitals)
                .HasForeignKey(e => e.type_id)
                .IsRequired();
            modelBuilder.Entity<hospital>()
                .HasOne(e => e.city)
                .WithMany(e => e.hospitals)
                .HasForeignKey(e => e.city_id)
                .IsRequired();
            modelBuilder.Entity<hospital>()
                .HasMany(e => e.kids)
                .WithOne(e => e.hospital)
                .HasForeignKey(e => e.host_id)
                .IsRequired();
            modelBuilder.Entity<parents>()
                .HasMany(e => e.kids)
                .WithOne(e => e.father)
                .HasForeignKey(e => e.father_id);
            modelBuilder.Entity<dose>()
                .HasMany(e => e.vaccines)
                .WithOne(e => e.dose)
                .HasForeignKey(e => e.dose_id);
            //modelBuilder.Entity<kids>()
            //    .HasMany(e => e.parents)
            //    .WithMany(e => e.kids)
            //    .UsingEntity<parents_kids>(

            //     l => l.HasOne<parents>().WithMany().HasForeignKey(e => e.parents_id),
            //     r => r.HasOne<kids>().WithMany().HasForeignKey(e => e.kids_id));
            modelBuilder.Entity<vaccine>()
                .HasMany(e => e.kids)
                .WithMany(e => e.Vaccines)
                .UsingEntity<kid_vaccine>(
                 l => l.HasOne<kids>().WithMany().HasForeignKey(e => e.kids_Id),
                 r => r.HasOne<vaccine>().WithMany().HasForeignKey(e => e.vaccines_Id));
        }


        public DbSet<parents>  parents { get; set; }
        public DbSet<hospital>  hospitals { get; set; }
        public DbSet<hospital_type>  hospital_Types { get; set; }
        public DbSet<cities> cities { get; set; }
        public DbSet<dose>  doses { get; set; }
        public DbSet<kids>  kids { get; set; }
        public DbSet<parents_kids>  parents_Kids { get; set; }
        public DbSet<vaccine> vaccine { get; set; }
        public DbSet<advices> advices { get; set; }
        public DbSet<kid_vaccine> kid_Vaccines { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<notify>notifies { get; set; }


    }
}
