
namespace SpaceShooter.MyModel
{
    /// <summary>
    /// A event class that handles the shoping functionality
    /// </summary>
    public class ShopEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopEventArgs"/> class.
        /// </summary>
        /// <param name="info">The information of the item.</param>
        public ShopEventArgs(string info)
        {
            this.Info = info;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopEventArgs"/> class.
        /// </summary>
        /// <param name="info">The information of the item..</param>
        /// <param name="price">The price of the item that is bought </param>
        public ShopEventArgs(string info, int price)
        {
            this.Info = info;
            this.PriceOfItem = price;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopEventArgs"/> class.
        /// </summary>
        /// <param name="price">The price of the item that is bought </param>
        public ShopEventArgs(int price)
        {
            this.PriceOfItem = price;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopEventArgs"/> class.
        /// </summary>
        /// <param name="e"> Takes in a ShopItem <see cref="ShopItem"/></param>
        public ShopEventArgs(ShopItem e)
        {

            this.PriceOfItem = e.ReturnPriceOfItem();
            this.Info = e.ReturnNameOfItem();

        }
        /// <summary>
        /// Gets the information of the item that is bought
        /// </summary>
        /// <returns>
        /// returns a string with information about the item
        /// </returns>
        public string Info { get; }

        /// <summary>
        /// Gets the price of item.
        /// </summary>
        /// <returns>
        /// an int value of The price of item.
        /// </returns>
        public int PriceOfItem { get; }
    }
}
