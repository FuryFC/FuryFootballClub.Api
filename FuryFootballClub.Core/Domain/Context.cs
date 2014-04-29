using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuryFootballClub.Core.Migrations;
using System.Data.Common;

namespace FuryFootballClub.Core.Domain
{
    class Context : DbContext
    {
        /* 
         * In order to have this work with appharbor, we need to have a funky configuration
         */
        private static DbConnection GetConnection()
        {
            var contextConfig = System.Configuration.ConfigurationManager.ConnectionStrings["context"];
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(contextConfig.ProviderName);
            DbConnection dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
            return dbConnection;
        }

        public DbSet<MatchFixture> MatchFixtures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        public Context():
            base(GetConnection(),true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }
    }
}
