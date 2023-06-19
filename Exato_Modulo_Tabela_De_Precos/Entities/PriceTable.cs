﻿using Exato_Price_Table_Module.Enums;

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

        public decimal CalculatePrice(List<Item> items)
        {
            throw new NotImplementedException();
        }
    }
}