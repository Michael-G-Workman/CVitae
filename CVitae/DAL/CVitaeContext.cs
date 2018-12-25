using CVitae.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CVitae.DAL
{
    public class CVitaeContext : DbContext
    {
        public CVitaeContext() : base("CVitaeContext")
        {
        }

        public DbSet<ContactCategory> ContactCategories { get; set; }
        public DbSet<EmailContact> EmailContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}