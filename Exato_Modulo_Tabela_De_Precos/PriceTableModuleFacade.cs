using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Interfaces;
using Exato_Price_Table_Module.Repositories;
using Exato_Price_Table_Module.Services.PriceTable;
using Microsoft.EntityFrameworkCore;

namespace Exato_Price_Table_Module
{
    public sealed class PriceTableModuleFacade : IFacade
    {
        private readonly DbContext _dbContext;
        private readonly IRepository _repository;

        public PriceTableModuleFacade(DbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new Repository(dbContext);
        }

        public decimal CalculatePrice(Guid priceTableExternalId, List<int> purchasedItemsIds)
        {
            var priceTableService = new PriceTableService(_repository);

            var table = priceTableService.GetPriceTableByExternalId(priceTableExternalId);

            return table.CalculatePrice(purchasedItemsIds);
        }
        
        public void CreatePriceTable(PriceTable priceTable)
        {
            var priceTableService = new PriceTableService(_repository);

            priceTableService.CreatePriceTable(priceTable);
        }
        
        public void UpdatePriceTable(PriceTable priceTable)
        {
            var priceTableService = new PriceTableService(_repository);

            priceTableService.UpdatePriceTable(priceTable);
        }
        
        public PriceTable? GetPriceTableByExternalId(Guid externalId)
        {
            var priceTableService = new PriceTableService(_repository);

            try
            {
                return priceTableService.GetPriceTableByExternalId(externalId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public List<PriceTable> GetAllPriceTables()
        {
            var priceTableService = new PriceTableService(_repository);

            return priceTableService.GetPriceTables();
        }
        
        public void DeletePriceTable(PriceTable priceTable)
        {
            var priceTableService = new PriceTableService(_repository);

            priceTableService.DeletePriceTable(priceTable);
        }
    }
}
