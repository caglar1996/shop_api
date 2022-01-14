using Microsoft.EntityFrameworkCore;
using ShopApi.Db.Models;

namespace ShopApi.Db.Contexts
{
    public class SqliteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source=CaseDatabase.db");
        }

        /// <summary>
        /// Müşteri Tablosu
        /// </summary>
        public DbSet<Customer> Customer { get; set; }
        /// <summary>
        /// İndirim Tablosu
        /// </summary>
        public DbSet<Discount> Discount { get; set; }
        /// <summary>
        /// Fatura Tablosu
        /// </summary>
        public DbSet<Invoice> Invoice { get; set; }

    }
}
