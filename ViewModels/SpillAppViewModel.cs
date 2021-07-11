using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SpaceShooter.DataCRUD;
using SpaceShooter.MyModel;
using Windows.UI.Xaml;


namespace SpaceShooter.ViewModels
{
    /// <summary>
    /// class that helps the SpillApp Page abstract its methods to
    /// </summary>
    public class SpillAppViewModel
    {
        readonly DeathScreen Death = new DeathScreen();
        readonly Shop shop = new Shop();
        /// <summary>
        /// Spawns the portal.
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="shopPortal">The shop portal.</param>
        private void SpawnPortal(CanvasDrawEventArgs args, CanvasBitmap shopPortal)
        {
            if (Shop.Store == false)
                shop.Draw(shopPortal, args);
        }
        /// <summary>
        /// method that runs when all the enemies are eliminated, which runs the shoptimer textblock and starts the countdown <see cref="Shop.ShopTimer"/>
        /// </summary>
        public void IfAllEnemiesDead()
        {
            if (Shop.Store == false)
            {
                EndRound();
                GameHud.AshopTimer.Visibility = Visibility.Visible;
                Shop.EndOfRoundCountDownStarter();

            }
        }
        /// <summary>
        /// Starts the next round.
        /// </summary>
        private void StartNextRound()
        {
            Levels.NextLevel();
            GameHud.RoundCounter.Visibility = Visibility.Visible;
            GameHud.AshopTimer.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Restarts the end of round timer.
        /// </summary>
        private void RestartEndOfRoundTimer()
        {
            if (Shop.StartShopTimer == false)
            {
                Shop.StartShopTimer = true;
                Shop.ShopTimer.Stop();
            }
        }
        /// <summary>
        /// End of round logic, spawns portal as long at there is time left, and starts next round if timer rounds out
        /// </summary>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="shopPortal">The shop portal.</param>
        public void EndOfRoundTimerLogic(CanvasDrawEventArgs args, CanvasBitmap shopPortal)
        {
            if (Levels._roundfixer.ShopTimer > 0)
                SpawnPortal(args, shopPortal);
            else
            {
                RestartEndOfRoundTimer();
                StartNextRound();
            }
        }
        /// <summary>
        /// Restarts the game back to level 1
        /// </summary>
        public void Restart_Game()
        {
            Death.RestartGame();
            ClearLists();
        }
        /// <summary>
        /// resets all lists
        /// </summary>
        public void EndRound()
        {
            GameHud.RoundCounter.Visibility = Visibility.Collapsed;
            ClearLists();
        }

        /// <summary>
        /// Clears all the lists used by the game
        /// </summary>
        private void ClearLists()
        {
            Hindrance.ThrowablesXposisjon.Clear();
            Hindrance.ThrowablesYposisjon.Clear();
            Enemy.EnemyXBulletposisjon.Clear();
            Enemy.EnemyYposisjon.Clear();
            Enemy.EnemyXposisjon.Clear();
            Enemy.EnemyYBulletposisjon.Clear();
            SpecialEnemy.SpesialEnemyXBulletposisjon.Clear();
            SpecialEnemy.SpesialEnemyYBulletposisjon.Clear();
            SpecialEnemy.SpesialEnemyXposisjon.Clear();
            SpecialEnemy.SpesialEnemyYposisjon.Clear();
            Player.XBulletPosisjon.Clear();
            Player.YBulletPosisjon.Clear();
            Hit.GetHitXpos.Clear();
            Hit.GetHitYpos.Clear();
            EliteEnemy.EliteEnemyXBulletposisjon.Clear();
            EliteEnemy.EliteEnemyYBulletposisjon.Clear();
            EliteEnemy.EliteEnemyXposisjon.Clear();
            EliteEnemy.EliteEnemyYposisjon.Clear();
        }

        /// <summary>
        /// Updates the score asynchronous if the user has a totalscore higher than its own record.
        /// </summary>
        public async void UpdateScoreAsync()
        {
            if (GameHud.Score > LoginViewModel.Loggedin.Totalscore)
            {
                LoginViewModel.Loggedin.Totalscore = GameHud.Score;
                await ApiData.UpdateUsersAsync(LoginViewModel.Loggedin);
            }
            
        }
    }
}
