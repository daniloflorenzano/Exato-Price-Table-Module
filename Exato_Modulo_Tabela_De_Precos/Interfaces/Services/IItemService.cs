namespace Exato_Price_Table_Module.Interfaces.Item
{
    internal interface IItemService
    {
        public void CreateItem(Entities.Item item, Guid tableExternalId);
        public void UpdateItem(Entities.Item item, Guid tableExternalId);
        public void DeleteItem(Entities.Item item, Guid tableExternalId);
        public Entities.Item GetItemByExternalId(Guid externalId, Guid tableExternalId);
    }
}
