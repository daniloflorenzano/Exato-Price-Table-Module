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
            throw new NotImplementedException();
        }

        public void DeletePriceTable(Entities.PriceTable priceTable)
        {
            throw new NotImplementedException();
        }

        public Entities.PriceTable GetPriceTableByExternalId(Guid externalId)
        {
            throw new NotImplementedException();
        }

        public List<Entities.PriceTable> GetPriceTables()
        {
            throw new NotImplementedException();
        }
    }
}
