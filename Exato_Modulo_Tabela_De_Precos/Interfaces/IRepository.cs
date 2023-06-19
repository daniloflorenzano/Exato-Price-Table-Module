using Exato_Price_Table_Module.Enums;

namespace Exato_Price_Table_Module.Interfaces
{
    public interface IRepository
    {
        public void CreatePriceTable(Entities.PriceTable priceTable);
        public void UpdatePriceTable(Entities.PriceTable priceTable);
        public void DeletePriceTable(Entities.PriceTable priceTable);
        public Entities.PriceTable? GetPriceTableByExternalId(Guid externalId);
        public List<Entities.PriceTable> GetPriceTables();

        public void CreateItem(Entities.Item item, Guid tableExternalId);
        public void UpdateItem(Entities.Item item, Guid tableExternalId);
        public void DeleteItem(Entities.Item item, Guid tableExternalId);
        public Entities.Item GetItemByExternalId(Guid externalId, Guid tableExternalId);
    }
}
