namespace Exato_Price_Table_Module.Entities
{
    public sealed class Item
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal InitialValue { get; set; }
        public decimal Credits { get; set; }
        public int AmountFrom { get; set; }
        public int AmountTo { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int TableId { get; set; }
        public PriceTable PriceTable { get; set; }
    }
}
