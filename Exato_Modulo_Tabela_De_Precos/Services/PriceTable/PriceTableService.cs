using Exato_Price_Table_Module.Interfaces;
using Exato_Price_Table_Module.Interfaces.Services;

namespace Exato_Price_Table_Module.Services.PriceTable
{
    public class PriceTableService : IPriceTableService
    {
        private readonly IRepository _repository;

        public PriceTableService(IRepository repository)
        {
            _repository = repository;
        }

        public void CreatePriceTable(Entities.PriceTable priceTable)
        {
            _repository.CreatePriceTable(priceTable);
        }

        public void UpdatePriceTable(Entities.PriceTable priceTable)
        {
            var existentTable = _repository.GetPriceTableByExternalId(priceTable.ExternalId);
            if (existentTable is null)
                throw new Exception("Price table not found");

            priceTable.UpdateDate = DateTime.Now;

            _repository.UpdatePriceTable(priceTable);
        }

        public void DeletePriceTable(Entities.PriceTable priceTable)
        {
            var existentTable = _repository.GetPriceTableByExternalId(priceTable.ExternalId);
            if (existentTable is null)
                throw new Exception("Price table not found");

            priceTable.Active = false;
            priceTable.Deleted = true;
            priceTable.DeletionDate = DateTime.Now;
            
            _repository.DeletePriceTable(priceTable);
        }

        public Entities.PriceTable GetPriceTableByExternalId(Guid externalId)
        {
            var priceTable = _repository.GetPriceTableByExternalId(externalId);
            if (priceTable is null)
                throw new Exception("Price table not found");

            return priceTable;
        }

        public List<Entities.PriceTable> GetPriceTables()
        {
            throw new NotImplementedException();
        }
    }
}
