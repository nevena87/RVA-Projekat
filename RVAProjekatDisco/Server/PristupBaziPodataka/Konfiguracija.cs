using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.PristupBaziPodataka
{
    public class Konfiguracija : DbMigrationsConfiguration<DiscoContext>
    {
        public Konfiguracija()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DiscoDBContext";
        }
    }
}
