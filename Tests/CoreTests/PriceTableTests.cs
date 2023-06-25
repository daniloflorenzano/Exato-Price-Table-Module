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
                Name = "Fixed_Price_Table",
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
                        ProductId = 1,
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
                        ProductId = 2,
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
                        ProductId = 3,
                        InitialValue = 3.0m
                    }
                }
            };

            var purchasedItemsIds = new List<int>() { 1, 1, 2, 2, 2, 3, 3, 3, 3, 3 };

            var price = table.CalculatePrice(purchasedItemsIds);

            Assert.That(price, Is.EqualTo(23.0m));
        }

        [Test]
        public void CalculatePrice_Of_Non_CumulativeRanges_Ranges_Price_Table()
        {
            PriceTable table = new PriceTable()
            {
                PrecificationType = PrecificationTypeEnum.NonCumulativeRanges,
                Items = new List<Item>()
                {
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 1: From 1 to 4",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 1,
                        ProductId = 1,
                        InitialValue = 4.0m,
                        AmountFrom = 1,
                        AmountTo = 4,
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 1: From 5 onwards",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 2,
                        ProductId = 1,
                        InitialValue = 2.0m,
                        AmountFrom = 5,
                        AmountTo = null
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 2: From 1 to 4",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 3,
                        InitialValue = 2.0m,
                        ProductId = 2,
                        AmountFrom = 1,
                        AmountTo = 4,
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 2: From 5 onwards",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 4,
                        InitialValue = 1.0m,
                        ProductId = 2,
                        AmountFrom = 5,
                        AmountTo = null
                    }
                }
            };
            var purchasedItemsIds = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2 };
            var price = table.CalculatePrice(purchasedItemsIds);
            Assert.That(price, Is.EqualTo(46m));
        }

        [Test]
        public void CalculatePrice_Of_CumulativeRanges_Ranges_Price_Table()
        {
            PriceTable table = new PriceTable()
            {
                PrecificationType = PrecificationTypeEnum.CumulativeRanges,
                Items = new List<Item>()
                {
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 1: From 1 to 4",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 1,
                        ProductId = 1,
                        InitialValue = 4.0m,
                        AmountFrom = 1,
                        AmountTo = 4,
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 1: From 5 onwards",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 2,
                        ProductId = 1,
                        InitialValue = 2.0m,
                        AmountFrom = 5,
                        AmountTo = null
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 2: From 1 to 4",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 3,
                        InitialValue = 2.0m,
                        ProductId = 2,
                        AmountFrom = 1,
                        AmountTo = 4,
                    },
                    new ()
                    {
                        ExternalId = Guid.NewGuid(),
                        Description = "Product 2: From 5 onwards",
                        CreationDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Deleted = false,
                        Id = 4,
                        InitialValue = 1.0m,
                        ProductId = 2,
                        AmountFrom = 5,
                        AmountTo = null
                    }
                }
            };
            var purchasedItemsIds = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2 };
            var price = table.CalculatePrice(purchasedItemsIds);
            Assert.That(price, Is.EqualTo(34m));
        }
    }
}
