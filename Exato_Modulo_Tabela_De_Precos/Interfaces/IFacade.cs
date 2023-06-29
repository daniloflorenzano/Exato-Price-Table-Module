namespace Exato_Price_Table_Module.Interfaces
{
    internal interface IFacade
    {
        public decimal CalculatePrice(Guid priceTableExternalId, List<int> purchasedItemsIds);
    }
}
