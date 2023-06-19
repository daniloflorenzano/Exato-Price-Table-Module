using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Interfaces;
using Exato_Price_Table_Module.Interfaces.Item;

namespace Exato_Price_Table_Module.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IRepository _repository;

        public ItemService(IRepository repository)
        {
            _repository = repository;
        }

        public void CreateItem(Item item, Guid tableExternalId)
        {
            var existingTable = _repository.GetPriceTableByExternalId(tableExternalId);
            if (existingTable is null)
                throw new Exception("Tabela de preço não encontrada");

            item.TableId = existingTable.Id;

            _repository.CreateItem(item, tableExternalId);
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
