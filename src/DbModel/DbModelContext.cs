using CbrFT.DbModel.Entity;
using System.Data.Entity;

namespace CbrFT.DbModel
{
    public class DbModelContext : DbContext
    {
        public DbModelContext() : base("CbrFTConnection") { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyValue> CurrencyValues { get; set; }
    }
}
