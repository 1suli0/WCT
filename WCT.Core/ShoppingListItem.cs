namespace WCT.Core
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        /*
         * Product price is also entered here, in case that
         * sometimes in the future product price changes.
         * That way, all the lists that existed prior to the change
         * will not be affected.
         */
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