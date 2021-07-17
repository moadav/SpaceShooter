namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Abstract class that handles item functionality
    /// </summary>
    public abstract class Item
    {


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>
        /// a string which is The name.
        /// </returns>
        private string Name { get; }
        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <returns>
        /// a int which is the price.
        /// </returns>
        private int Price { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        public Item(string name, int price)
        {
            Name = name;
            Price = price;

        }
        /// <summary>
        /// Returns the name of item.
        /// </summary>
        /// <returns>
        /// a string value that is the name of the bought item
        /// </returns>
        public string ReturnNameOfItem()
        {
            return Name;
        }
        /// <summary>
        /// Returns the price of item.
        /// </summary>
        /// <returns>
        /// an int value that is the price of the bought item
        /// </returns>
        public int ReturnPriceOfItem()
        {
            return Price;
        }
    }
}
