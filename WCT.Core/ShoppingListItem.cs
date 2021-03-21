namespace WCT.Core
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }

        public string ShoppingListId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}