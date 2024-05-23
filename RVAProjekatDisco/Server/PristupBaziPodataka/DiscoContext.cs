using Common.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.PristupBaziPodataka
{
    public class DiscoContext : DbContext
    {
        public DiscoContext() : base("dbConnection2024")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DiscoContext, Konfiguracija>());
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 10 * 60;     // 10 minuta
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Pesma> Pesme { get; set; }
        public DbSet<Plejlista> Plejliste { get; set; }
    }
}
