using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exato_Price_Table_Module.Repositories
{
    public class Repository : IRepository
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void CreatePriceTable(PriceTable priceTable)
        {
            _context.Set<PriceTable>().Add(priceTable);
            _context.SaveChanges();
        }

        public void UpdatePriceTable(PriceTable priceTable)
        {
            _context.Entry(priceTable).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePriceTable(PriceTable priceTable)
        {
            _context.Entry(priceTable).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public PriceTable? GetPriceTableByExternalId(Guid externalId)
        {
            var table = _context.Set<PriceTable>()
                .Include(pt => pt.Items)
                .FirstOrDefault(x => x.ExternalId == externalId);
            
            return table;
        }

        public List<PriceTable> GetPriceTables()
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item, Guid tableExternalId)
        {
            _context.Set<Item>().Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Item item, Guid tableExternalId)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Item item, Guid tableExternalId)
        {
            throw new NotImplementedException();
        }

        public Item GetItemByExternalId(Guid externalId, Guid tableExternalId)
        {
            throw new NotImplementedException();
        }
    }
}
