using KazalisteFranic.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazalisteFranic.DAL
{
    public class KazalisteManagerDbContext : IdentityDbContext<AppUser>
    {
        public KazalisteManagerDbContext(DbContextOptions<KazalisteManagerDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Glumac> Glumci { get; set; }
        public DbSet<Redatelj> Redatelji { get; set; }
        public DbSet<Predstava> Predstave { get; set; }
        public DbSet<Akademija> Akademije { get; set; }

        public DbSet<GlumacPredstava> GlumacPredstave { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Akademija>().HasData(new Akademija { Id = 1, Naziv = "Akademija Dramske Umjetnosti u Zagrebu", Grad = "Zagreb" });
            modelBuilder.Entity<Akademija>().HasData(new Akademija { Id = 2, Naziv = "Akademija Dramske Umjetnosti u Splitu", Grad = "Split" });
            modelBuilder.Entity<Akademija>().HasData(new Akademija { Id = 3, Naziv = "Libertas", Grad = "Rijeka" });
            modelBuilder.Entity<Akademija>().HasData(new Akademija { Id = 4, Naziv = "Akademija za umjetnost i kulturu", Grad = "Osijek" });

            modelBuilder.Entity<Glumac>().HasData(new Glumac { Id = 1, 
                                                            Ime = "Karlo", 
                                                            Prezime = "Franić", 
                                                            Citat = "Nije ti ovo Diznilend",
                                                            Spol = 'M',
                                                            AkademijaId = 1 });

            modelBuilder.Entity<Redatelj>().HasData(new Redatelj
            {
                Id = 1,
                Ime = "Martin",
                Prezime = "Scorsese",
                Spol = 'M',
                AkademijaId = 1
            });
            modelBuilder.Entity<Redatelj>().HasData(new Redatelj
            {
                Id = 2,
                Ime = "Rene",
                Prezime = "Medvešek",
                Spol = 'M',
                AkademijaId = 1
            });
            modelBuilder.Entity<Redatelj>().HasData(new Redatelj
            {
                Id = 3,
                Ime = "Saša",
                Prezime = "Anočić",
                Spol = 'M',
                AkademijaId = 4
            });
            modelBuilder.Entity<Redatelj>().HasData(new Redatelj
            {
                Id = 4,
                Ime = "Dana",
                Prezime = "Budisavljević",
                Spol = 'Ž',
                AkademijaId = 3
            });

        }
    }
}
