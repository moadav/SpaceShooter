

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles the death functionality
    /// </summary>
    public class DeathScreen
    {

        /// <summary>
        /// Displays the results of the player
        /// </summary>
        private void Results()
        {
            GameHud.DeathScreen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            GameHud.DisplayTotalPoints();
        }
        /// <summary>
        /// Runs method if player is dead
        /// </summary>
        private void PlayerIsDead()
        {           
            Results();
            GameTimer.StopTimers();
        }
        /// <summary>
        /// Checks is the player is dead
        /// </summary>
        /// <returns>
        /// a boolean value that return true if dead and false if not
        /// </returns>
        /// <example>
        /// <code>
        /// if(ifDead())
        /// Do something
        /// </code>
        /// </example>
        public bool IfDead()
        {

            if (CurrentPlayerHealth() > 4)
            {
                PlayerIsDead();
                return true;
            }
            else
            {
                PlayerIsAlive();
                return false;
            }
        }
        /// <summary>
        /// gets the current player health
        /// </summary>
        /// <returns>
        /// a value of the players current health
        /// </returns>
        private double CurrentPlayerHealth()
        {
            return GameHud.HealthBar.Offset;
        }
        /// <summary>
        /// If the player is alive
        /// </summary>
        private void PlayerIsAlive()
        {
            GameHud.DeathScreen.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
           
        }
        /// <summary>
        /// Restarts the game back to level 1 settings
        /// </summary>
        public void RestartGame()
        {
            GameTimer.StartTimers();
            Levels.ResetToLevel1();
        }
    }
}
