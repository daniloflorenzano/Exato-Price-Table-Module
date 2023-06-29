using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Enums;
using Exato_Price_Table_Module.Repositories;
using Exato_Price_Table_Module.Services.ItemService;
using Exato_Price_Table_Module.Services.PriceTable;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.PersistenceTests
{
    internal class PersistenceOperationsInMemoryTests
    {
        private void StartMinimalSetupInMemory(out PriceTableService priceTableService, out ModuleContext context, out ItemService itemService)
        {
            var factory = new ConnectionFactory();
            context = factory.CreateContextForSQLite();
            var repository = new Repository(context);

            priceTableService = new PriceTableService(repository);
            itemService = new ItemService(repository);

            priceTableService.CreatePriceTable(new PriceTable()
            {
                ExternalId = Guid.NewGuid(),
                Name = "Teste",
                Active = true,
                CreationDate = DateTime.Now,
                Deleted = false,
                Description = "Primeria tabela de teste",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(30),
                PrecificationType = PrecificationTypeEnum.FixedPrice
            });
        }

        [Test]
        public void CreateTable_InMemory_Success()
        {
            StartMinimalSetupInMemory(out var priceTableService, out var context, out var itemService);


            var newPriceTable = new PriceTable()
            {
                ExternalId = Guid.NewGuid(),
                Name = "Segunda Tabela",
                Active = true,
                CreationDate = DateTime.Now,
                Deleted = false,
                Description = "Primeria tabela de teste",
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(30),
                PrecificationType = PrecificationTypeEnum.CumulativeRanges

            };

            priceTableService.CreatePriceTable(newPriceTable);

            var tables = context.PriceTables.ToList();
            Assert.That(tables.Contains(newPriceTable));
        }

        [Test]
        public void CreateItem_In_Table_In_Memory_Success()
        {
            StartMinimalSetupInMemory(out var priceTableService, out var context, out var itemService);

            var tables = context.PriceTables.ToList();

            var item = new Item()
            {
                ExternalId = Guid.NewGuid(),
                Description = "Item 1",
                CreationDate = DateTime.Now,
                Deleted = false,
                TableId = tables[0].Id
            };

            itemService.CreateItem(item, tables[0].ExternalId);

            var itemInTable = context.PriceTables.Include(pt => pt.Items).FirstOrDefault()?.Items[0];

            Assert.IsNotNull(itemInTable);
            Assert.That(item, Is.EqualTo(itemInTable));
        }

        [Test]
        public void Update_Table_In_Memory()
        {
            StartMinimalSetupInMemory(out var priceTableService, out var context, out var itemService);

            var tableToEdit = context.PriceTables.FirstOrDefault();
            
            tableToEdit.Name = "Edited Name";
            priceTableService.UpdatePriceTable(tableToEdit);
            
            var editedTable = context.PriceTables.FirstOrDefault();
            
            Assert.That(editedTable.Name, Is.EqualTo("Edited Name"));
        }
    }
}
