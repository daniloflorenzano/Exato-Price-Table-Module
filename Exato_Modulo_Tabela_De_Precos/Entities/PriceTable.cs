using Exato_Price_Table_Module.Enums;

namespace Exato_Price_Table_Module.Entities
{
    public sealed class PriceTable
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletionDate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public PrecificationTypeEnum PrecificationType { get; set; }
        public List<Item> Items { get; set; }

        public decimal CalculatePrice(List<int> purchasedItemsIds)
        {
            if (PrecificationType is PrecificationTypeEnum.FixedPrice)
                return CalculatePriceFromFixedPrecificationType(purchasedItemsIds);

            if (PrecificationType is PrecificationTypeEnum.NonCumulativeRanges)
                return CalculatePriceFromNonCumulativeRangesPrecificationType(purchasedItemsIds);

            if (PrecificationType is PrecificationTypeEnum.CumulativeRanges)
                return CalculatePriceFromCumulativeRangesPrecificationType(purchasedItemsIds);

            throw new Exception($"Precification type {PrecificationType} not implemented.");
        }

        private decimal CalculatePriceFromFixedPrecificationType(List<int> purchasedItemsIds)
        {
            var price = 0.0m;

            foreach (var purchasedItemId in purchasedItemsIds)
            {
                var item = Items.FirstOrDefault(i => i.ProductId == purchasedItemId);

                if (item == null)
                    throw new Exception($"Item with id {purchasedItemId} not found in price table {Name}.");

                price += item.InitialValue;
            }

            return price;
        }

        private decimal CalculatePriceFromNonCumulativeRangesPrecificationType(List<int> purchasedItemsIds)
        {
            var price = 0.0m;

            var allProductsExistentInTable = Items
                .Select(i => i)
                .Where(i => purchasedItemsIds.Contains(i.ProductId))
                .ToList();

            foreach (var product in allProductsExistentInTable)
            {

                var productInPurchasedItemsCount = purchasedItemsIds
                    .Select(i => i)
                    .Count(i => i == product.ProductId);

                for (var i = 0; i < productInPurchasedItemsCount; i++)
                {
                    var actualItemAmount = i + 1;

                    if (actualItemAmount < product.AmountFrom)
                        continue;


                    if (actualItemAmount >= product.AmountFrom && actualItemAmount <= product.AmountTo)
                        price += product.InitialValue;

                    else if (actualItemAmount >= product.AmountFrom && product.AmountTo is null)
                        price += product.InitialValue;
                    
                    else
                        break;
                }
            }

            return price;
        }

        private decimal CalculatePriceFromCumulativeRangesPrecificationType(List<int> purchasedItemsIds)
        {
            var allProductsExistentInTable = Items
                .Select(i => i)
                .Where(i => purchasedItemsIds.Contains(i.ProductId))
                .ToList();

            var pricesByProduct = new List<decimal>();
            var productsAlreadyCalculated = 0;
            int? lastProductCalculatedId = null;

            foreach (var product in allProductsExistentInTable)
            {
                if (lastProductCalculatedId == product.ProductId)
                    pricesByProduct.RemoveAt(pricesByProduct.Count - 1);

                var productInPurchasedItemsCount = purchasedItemsIds
                    .Select(i => i)
                    .Count(i => i == product.ProductId);

                

                for (var i = 0; i < productInPurchasedItemsCount; i++)
                {
                    var actualItemAmount = i + 1;

                    if (actualItemAmount < product.AmountFrom)
                        continue;

                    if (lastProductCalculatedId != product.ProductId)
                        productsAlreadyCalculated = 0;


                    if (actualItemAmount >= product.AmountFrom && actualItemAmount <= product.AmountTo)
                    {
                        productsAlreadyCalculated++;
                        lastProductCalculatedId = product.ProductId;
                    }


                    else if (actualItemAmount >= product.AmountFrom && product.AmountTo is null)
                    {
                        productsAlreadyCalculated++;
                        lastProductCalculatedId = product.ProductId;
                    }

                    else
                        break;
                }

                pricesByProduct.Add(productsAlreadyCalculated * product.InitialValue);
            }

            return pricesByProduct.Sum();
        }
    }
}
