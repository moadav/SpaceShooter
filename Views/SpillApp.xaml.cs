using Microsoft.Graphics.Canvas;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Threading.Tasks;
using SpaceShooter.MyModel;
using SpaceShooter.ViewModels;
using Windows.UI.Xaml.Navigation;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceShooter
{
    /// <summary>
    /// This page handles the bitmaps to draw on screen and functionality to make the game run
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class SpillApp : Page
    {
        /// <summary>
        /// different values containing the bitmaps to draw to screen <see cref="CanvasBitmap"/>
        /// </summary>
        private CanvasBitmap GameScreenBackground, GamePlayer, Bullet, Asteroid, Enemy1, BulletExplosion, ShopPortal, specialEnemy, EliteEnemy;
        /// <summary>
        /// Gets the spillappviewmodel.
        /// </summary>
        /// <returns>
        /// The spillAppViewModel.
        /// </returns>
        public SpillAppViewModel SpillAppViewModel { get; } = new SpillAppViewModel();

        readonly Player player = new Player();
        readonly Enemy enemy = new Enemy();
        readonly Hindrance throwable = new Hindrance();
        readonly Hit Hit = new Hit();
        readonly Shop shop = new Shop();
        readonly SpecialEnemy AspecialEnemy = new SpecialEnemy();
        readonly DeathScreen Death = new DeathScreen();
        readonly EliteEnemy eliteEnemy = new EliteEnemy();
        readonly GameTimer _GameTimers = new GameTimer();
        /// <summary>
        /// Returns to main page.
        /// </summary>
        private void ReturnToMainPage()
        {
            Frame.GoBack();
        }
        public SpillApp()
        {
            InitializeComponent();
            DataContext = SpillAppViewModel;
            Window.Current.CoreWindow.KeyDown += player.PlayerMovement;
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            InitiateTimers();
        }
        /// <summary>
        /// Initiates the timers.
        /// </summary>
        private void InitiateTimers()
        {
            Shop.ShopCountdownInitiate();
            _GameTimers.InstansiateAndStartTimers();
        }
        /// <summary>
        /// Mains the canvas page create resources.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs"/> instance containing the event data.</param>
        private void MainCanvasPage_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateAsyncResources(sender).AsAsyncAction());
        }
        /// <summary>
        /// Creates the asynchronous resources.
        /// </summary>
        /// <param name="sender">The sender.</param>
       private async Task CreateAsyncResources(CanvasControl sender)
        {
            GameScreenBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/2dspacebackground.jpg"));
            GamePlayer = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/Player.png"));
            Bullet = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/Bullet.png"));
            Asteroid = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/Asteroid.png"));
            Enemy1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/EnemyOcta.png"));
            BulletExplosion = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/HitMarker.png"));
            ShopPortal = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/Portal.png"));
            specialEnemy = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/Enemy2.png"));
            EliteEnemy = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/image/EliteEnemy.png"));
        }
        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void DrawBackground(CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(GameScreenBackground, -210, -50);
        }
        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void DrawPlayer(CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(GamePlayer, Player.defaultPlayerCoords.StartPosisjonX - 310, Player.defaultPlayerCoords.StartPosisjonY - 320);
        }
        /// <summary>
        /// Readies up the game elements.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void ReadyUpTheGameElements(CanvasDrawEventArgs args)
        {
            DrawBackground(args);
            DrawPlayer(args);
            GameHud.GetDisplayElements(Score, ShopTimer, RoundCounter);
            GameHud.GetBars(GreenBar, AmmoBar);
            GameHud.GetDeathScreen(DeathScreen, DeathScreenText);
            GameHud.GetShopNotificationMessage(ShopNotification);
            GameHud.AmmoRegenerate();
            GameHud.DisplayPoints();
            GameHud.ShopTimer();
            shop.Shop_Store(ShopMenu);
        }
        /// <summary>
        /// Draws the bitmaps and starts game functionality.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void DrawBitmapsAndStartGameFunctionality(CanvasDrawEventArgs args)
        {
            enemy.Draw(Enemy1, args);
            enemy.Shoot(Bullet, args);
            throwable.Draw(Asteroid, args);
            player.DrawBullets(Bullet, args);
            Hit.Draw(BulletExplosion, args);
            eliteEnemy.Draw(EliteEnemy, args);
            eliteEnemy.Shoot(Bullet, args);
            AspecialEnemy.Draw(specialEnemy, args);
            AspecialEnemy.Shoot(Bullet, args);
        }
        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void RunGame(CanvasDrawEventArgs args)
        {
            DisplayVisibility();
            GameHud.DisplayRoundRequirements();
            DrawBitmapsAndStartGameFunctionality(args);
        }
        /// <summary>
        /// Fixes the visiblity of some GameHud variables <see cref="GameHud"/>
        /// </summary>
        private void DisplayVisibility()
        {
            GameHud.AshopTimer.Visibility = Visibility.Collapsed;
            GameHud.RoundCounter.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// The main game functionality
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        private void Game(CanvasDrawEventArgs args)
        {
            if (!Death.IfDead())
            {
                if (!Levels.RoundOver())
                    RunGame(args);
                else
                {
                    SpillAppViewModel.IfAllEnemiesDead();
                    SpillAppViewModel.EndOfRoundTimerLogic(args, ShopPortal);
                }
            }
            else
            {
                SpillAppViewModel.UpdateScoreAsync();
                SpillAppViewModel.EndRound();
            }
        }
        /// <summary>
        /// This method runs all methods necessary to create the game and draw all objects
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public void MainCanvasPage_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            ReadyUpTheGameElements(args);
            Game(args);
            MainCanvasPage.Invalidate();
        }
    }
}
