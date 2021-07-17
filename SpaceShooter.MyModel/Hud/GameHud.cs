using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles the GameHud
    /// </summary>
    public static class GameHud
    {
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <returns>
        /// an int, which is The score for the player.
        /// </returns>
        /// <example>
        /// <code>
        /// Score = 4;
        /// </code>
        /// </example>
        public static int Score { get; set; }
        /// <summary>
        /// Gets the Canvas element for Death screen
        /// </summary>
        /// <returns>
        /// a Canvas <see cref="Canvas"/> 
        /// </returns>
        public static Canvas DeathScreen { get; private set; } = new Canvas();
        /// <summary>
        /// Gets the GradientStop element for the ammobar
        /// </summary>
        /// <returns>
        /// a GradientStop <see cref="GradientStop"/> 
        /// </returns>
        public static GradientStop AmmoBar { get; private set; } = new GradientStop();
        /// <summary>
        /// Gets the GradientStop element for the HealthBar
        /// </summary>
        /// <returns>
        /// a GradientStop <see cref="GradientStop"/> 
        /// </returns>
        public static GradientStop HealthBar { get; private set; } = new GradientStop();
        /// <summary>
        /// Gets the TextBlock element for the current Score Interface
        /// </summary>
        /// <returns>
        /// a TextBlock <see cref="TextBlock"/> 
        /// </returns>
        private static TextBlock DeathScreenScore { get; set; } = new TextBlock();
        /// <summary>
        /// Gets the TextBlock element for the Death Score Interface
        /// </summary>
        /// <returns>
        /// a TextBlock <see cref="TextBlock"/> 
        /// </returns>
        private static TextBlock TheScorePoint { get; set; } = new TextBlock();
        /// <summary>
        /// Gets the TextBlock element for the  shop timer Interface
        /// </summary>
        /// <returns>
        /// a TextBlock <see cref="TextBlock"/> 
        /// </returns>
        public static TextBlock AshopTimer { get; set; } = new TextBlock();
        /// <summary>
        /// Gets the TextBlock element for the round counter Interface
        /// </summary>
        /// <returns>
        /// a TextBlock <see cref="TextBlock"/> 
        /// </returns>
        public static TextBlock RoundCounter { get; private set; } = new TextBlock();
        /// <summary>
        /// Gets the TextBlock element for the the Shop notification
        /// </summary>
        /// <returns>
        /// a TextBlock <see cref="TextBlock"/> 
        /// </returns>
        public static TextBlock ShopNotification { get; private set; } = new TextBlock();
        /// <summary>
        /// Gets the display elements that says if you have bought and element or not.
        /// </summary>
        /// <param name="Score"> The TextBlock element for score <see cref="TheScorePoint"/>.</param>
        /// <param name="shoptimer"> The TextBlock element for shop timer <see cref="AshopTimer"/>.</param>
        /// <param name="roundcounter"> The TextBlock element for round counter <see cref=" RoundCounter"/>.</param>
        public static void GetDisplayElements(TextBlock Score, TextBlock shoptimer, TextBlock roundcounter)
        {
            TheScorePoint = Score;
            AshopTimer = shoptimer;
            RoundCounter = roundcounter;
        }
        /// <summary>
        /// Gets the bars.
        /// </summary>
        /// <param name="healthBar">The health bar. <see cref="AmmoBar"/> </param>
        /// <param name="ammobar">The ammobar. <see cref="HealthBar"/> </param>
        public static void GetBars(GradientStop healthBar, GradientStop ammobar)
        {
            AmmoBar = ammobar;
            HealthBar = healthBar;
        }
        /// <summary>
        /// Gets the Elements for Death screen.
        /// </summary>
        /// <param name="aDeathScreen"> Canvas element for displaying the death screen <see cref="DeathScreen"/></param>
        /// <param name="aDeathScreenScore">TextBlock for death screen score. <see cref="DeathScreenScore"/></param>
        public static void GetDeathScreen(Canvas aDeathScreen, TextBlock aDeathScreenScore)
        {
            DeathScreenScore = aDeathScreenScore;
            DeathScreen = aDeathScreen;
        }

        /// <summary>
        /// Gets the shop notification message TextBlock.
        /// </summary>
        /// <param name="shopMessage">The shop message. </param>
        public static void GetShopNotificationMessage(TextBlock shopMessage) =>        
            ShopNotification = shopMessage;


        /// <summary>
        /// Displays the total points TextBlock.
        /// </summary>
        public static void DisplayTotalPoints() =>
            DeathScreenScore.Text = $"Your total points: {Score}";

        /// <summary>
        /// Recolors the Ammo bar.
        /// </summary>
        public static void AmmoRegenerate()
        {
            if (AmmoBar.Offset > 0.01)
                AmmoBar.Offset -= 0.006;

        }
        /// <summary>
        /// Reduces the shoptimer by 1 .<see cref="Shop.ShopCountdownInitiate()"/>
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public static void ShopCountdown(object sender, object e) =>
   
            Levels._roundfixer.ShopTimer -= 1;

        /// <summary>
        /// Displays the shoptimer TextBlock <see cref="AshopTimer"/>.
        /// </summary>
        public static void ShopTimer() =>
            AshopTimer.Text = $"You have {Levels._roundfixer.ShopTimer} seconds to enter the portal.";
 

        /// <summary>
        /// Displays the points.
        /// </summary>
        public static void DisplayPoints() => TheScorePoint.Text = $"Score : {Score}";
        /// <summary>
        /// Displays the round requirements TextBlock.
        /// </summary>
        public static void DisplayRoundRequirements() =>
        RoundCounter.Text = $"Round: {Levels._roundfixer.Round} Defeat all the enemies: {Levels._roundfixer.TotalEnemies}";

    }
}
