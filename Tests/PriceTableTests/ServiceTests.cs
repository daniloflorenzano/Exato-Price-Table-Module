using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Enums;
using Exato_Price_Table_Module.Repositories;
using Exato_Price_Table_Module.Services.PriceTable;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.PriceTableTests
{
    internal class ServiceTests
    {
        [Test]
        public void CreateTable_InMemory_Success()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForSQLite();
            var _repository = new Repository(context);

            var service = new PriceTableService(_repository);

            service.CreatePriceTable(new PriceTable()
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

            var tables = context.PriceTables.ToList();
            Assert.AreEqual(1, tables.Count);
        }

        [Test]
        public void CreateItem_In_Table_In_Memory_Success()
        {
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForSQLite();
            var _repository = new Repository(context);

            var service = new PriceTableService(_repository);

            service.CreatePriceTable(new PriceTable()
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

            var tables = context.PriceTables.ToList();

            var item = new Item()
            {
                ExternalId = Guid.NewGuid(),
                Description = "Item 1",
                CreationDate = DateTime.Now,
                Deleted = false,
                TableId = tables[0].Id
            };

            _repository.CreateItem(item, tables[0].ExternalId);

            var itemInTable = context.PriceTables.Include(pt => pt.Items).FirstOrDefault()?.Items[0];

            Assert.IsNotNull(itemInTable);
            Assert.AreEqual(itemInTable, item);
        }
    }
}
