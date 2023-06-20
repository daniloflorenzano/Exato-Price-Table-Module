using Exato_Price_Table_Module.Entities;
using Exato_Price_Table_Module.Enums;
using NUnit.Framework;

namespace Tests.CoreTests
{
    internal class PriceTableTests
    {
        [Test]
        public void CalculatePrice_Of_Fixed_Price_Table()
        {
            PriceTable table = new PriceTable()
            {
                PrecificationType = PrecificationTypeEnum.FixedPrice,
                Items = new List<Item>()
                {
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Item 1",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 1,
                        InitialValue = 1.0m
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Item 2",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 2,
                        InitialValue = 2.0m
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Item 3",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 3,
                        InitialValue = 3.0m
                    }
                }
            };

            var purchasedItemsIds = new List<int>() { 1, 1, 2, 2, 2, 3, 3, 3, 3, 3 };

            var price = table.CalculatePrice(purchasedItemsIds);

            Assert.That(price, Is.EqualTo(23.0m));
        }
    }
}
