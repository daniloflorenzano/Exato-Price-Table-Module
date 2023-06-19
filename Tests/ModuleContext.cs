using Exato_Price_Table_Module.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public sealed class ModuleContext : DbContext
    {
        private readonly string _schemaName;

        public ModuleContext(DbContextOptions<ModuleContext> options, string schemaName) : base(options)
        {
            _schemaName = schemaName;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar o esquema padrão para todas as entidades
            modelBuilder.HasDefaultSchema(_schemaName);

            modelBuilder.Entity<PriceTable>()
                .HasMany(pt => pt.Items)
                .WithOne(i => i.PriceTable)
                .HasForeignKey(i => i.TableId);
        }


        public DbSet<PriceTable> PriceTables { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
