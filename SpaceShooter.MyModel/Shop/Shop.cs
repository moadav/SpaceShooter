using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// This class handles the shop functionality
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// an event that Occurs when the player buys something or interacts from the shop <see cref="ShopEventArgs"/>.
        /// </summary>
        private static event EventHandler<ShopEventArgs> ShopEvent;
        /// <summary>
        /// A delegate that handles the shop items <see cref="ShopItem"/>
        /// </summary>
        /// <returns>
        /// a bool value, that returns true wether the player has bought an item and false if not
        /// </returns>
        private delegate bool AvailableItemsToBuy();
        /// <summary>
        /// An available item that the player can buy <see cref="AvailableItemsToBuy"/>
        /// </summary>
        private AvailableItemsToBuy HealthPack { get; set; }
        /// <summary>
        /// An available item that the player can buy <see cref="AvailableItemsToBuy"/>
        /// </summary>
        private AvailableItemsToBuy ArmourUpgrade { get; set; }

        /// <summary>
        /// Gets or sets portal.
        /// </summary>
        /// <returns>
        /// true if portal is active and false if it is not
        /// </returns>
        /// <example>
        /// <code>
        /// Portal = true, portal = false
        /// </code>
        /// </example>
        public static bool Portal { get; set; }


        /// <summary>
        /// Gets or sets store.
        /// </summary>
        /// <returns>
        /// true if store is active and false if it is not
        /// </returns>
        /// <example>
        /// <code>
        /// Store = true, store = false
        /// </code>
        /// </example>
        public static bool Store { get; set; } = false;
        /// <summary>
        /// Gets or sets StartShopTimer.
        /// </summary>
        /// <returns>
        /// StartShopTimer if store is active and false if it is not
        /// </returns>
        /// <example>
        /// <code>
        /// StartShopTimer = true, StartShopTimer = false
        /// </code>
        /// </example>
        public static bool StartShopTimer { get; set; } = true;

        /// <summary>
        /// A DispatchTimer for Shop timer
        /// </summary>
        public static DispatcherTimer ShopTimer { get; set; } = new DispatcherTimer();

        /// <summary>
        /// Instansiates all buyable items <see cref="AvailableItemsToBuy"/>.
        /// </summary>
        private void InstansiateAllBuyableItems()
        {
            HealthPack = new AvailableItemsToBuy(ShopItem.HealHalfHealth);
            ArmourUpgrade = new AvailableItemsToBuy(ShopItem.BuyArmour);
        }
        /// <summary>
        /// Initiates and starts the shop countdown <see cref="RoundSetup.ShopTimer"/> per second
        /// </summary>
        public static void ShopCountdownInitiate()
        {
            ShopTimer.Tick += GameHud.ShopCountdown;
            ShopTimer.Interval = new TimeSpan(0, 0, 1);
        }
        /// <summary>
        /// This method is run whenever the player has interacted with the shop <see cref="ShopEventArgs"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ShopEventArgs"/> instance containing the event data.</param>
        private void ItemIsBought(object sender, ShopEventArgs e)
        {
            GameHud.Score -= e.PriceOfItem;
            GameHud.ShopNotification.Text = e.Info + " Has been successfully Bought!";
        }

        public Shop()
        {

        }
        /// <summary>
        /// Checks if the round is over if it is the shop countdown starts
        /// </summary>
        public static void EndOfRoundCountDownStarter()
        {
            if (StartShopTimer)
            {
                ShopTimer.Start();
                StartShopTimer = false;
            }
        }

        /// <summary>
        /// A method that instansiates all shop items <see cref="InstansiateAllBuyableItems"/> and Checks whether to draw the store screen or not
        /// </summary>
        /// <param name="shop">Takes in Canvas as parameter to change to visibility if the player has entered the shop.</param>
        public void Shop_Store(Canvas shop)
        {
            InstansiateAllBuyableItems();
            if (Store)
            {
                GameTimer.StopTimers();
                ShowShop(shop);
            }
            else
                shop.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// a method that will turn the shop visiblity on visible <see cref="Visibility"/>
        /// </summary>
        /// <param name="shop">The shop.</param>
        private void ShowShop(Canvas shop)
        {
            shop.Visibility = Visibility.Visible;
            GameHud.AshopTimer.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Checks if the player has decided to buy Health pack, and runs event if the player has bought health pack.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Health_Pack(object sender, RoutedEventArgs e)
        {
            ShopEvent = ItemIsBought;
            if (HealthPack.Invoke())
                ShopEvent?.Invoke(this, new ShopEventArgs(new ShopItem("Health Pack", 5)));
            else
                GameHud.ShopNotification.Text = "You Cannot afford this item";
        }

        /// <summary>
        /// Checks if the player has decided to buy Damage Reduction, and runs event if the player has bought Damage Reduction.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Damage_reduction(object sender, RoutedEventArgs e)
        {
            ShopEvent = ItemIsBought;
            if (ArmourUpgrade.Invoke())
                ShopEvent?.Invoke(this, new ShopEventArgs(new ShopItem("Armour Upgrade", 50)));
            else
                GameHud.ShopNotification.Text = "You Cannot afford this item or the buylimit has been reached";

        }

        /// <summary>
        /// this method ends the shop and returns player back
        /// </summary>
        private void EndShop()
        {
            Portal = false;
            Store = false;
            Levels._roundfixer.ShopTimer = -1;
        }
        /// <summary>
        /// Starts the next round
        /// </summary>
        public void Next_Level()
        {
            EndShop();
            GameHud.ShopNotification.Text = string.Empty;
            GameTimer.StartTimers();
        }
        /// <summary>
        /// Thsi method teleports the player to the shop
        /// </summary>
        private void StartShop()
        {
            Store = true;
        }
        /// <summary>
        /// Draws the specified bitmap.
        /// </summary>
        /// <param name="bitmap">Takes in a bitmap parameter to draw a bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            DrawPortal(bitmap, args);
            if (Player.defaultPlayerCoords.StartPosisjonX >= 1300 & Player.defaultPlayerCoords.StartPosisjonY >= 620 & Player.defaultPlayerCoords.StartPosisjonY <= 900)
                IfPlayerHitPortal();
        }
        /// <summary>
        /// Checks if the player has had collision with the portal
        /// </summary>
        private void IfPlayerHitPortal()
        {
            StartShop();
            ShopTimer.Stop();

        }
        /// <summary>
        /// Draws the portal.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void DrawPortal(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(bitmap, 800, 200);
        }
    }
}
