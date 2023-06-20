namespace Exato_Price_Table_Module.Interfaces.Services
{
    internal interface IPriceTableService
    {
        public void CreatePriceTable(Entities.PriceTable priceTable);
        public void UpdatePriceTable(Entities.PriceTable priceTable);
        public void DeletePriceTable(Entities.PriceTable priceTable);
        public Entities.PriceTable GetPriceTableByExternalId(Guid externalId);
        public List<Entities.PriceTable> GetPriceTables();
    }
}
