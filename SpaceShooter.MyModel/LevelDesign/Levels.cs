

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles the level design
    /// </summary>
    public static class Levels
    {
        /// <summary>
        /// Uses a RoundSetup <see cref="RoundSetup"/> to handle round functionality
        /// </summary>
        public static RoundSetup _roundfixer;

        private static int enemies, specialEnemies, round, eliteEnemies, shoptimer;
        /// <summary>
        /// Checks whether the round is over or not
        /// </summary>
        /// <returns>
        /// Returns true if the round is over, and false if it is not
        /// </returns>
        public static bool RoundOver()
        {
            if (RemainingEnemiesOnRound() == 0)
            {
                Shop.Portal = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// checks the total enemies left
        /// </summary>
        /// <returns>
        /// returns an int value of how much more enemies there are on the screen
        /// </returns>
        /// <example>
        /// <code>
        /// if there are 2 enemies 1 special enemy and 0 elite enemies
        /// this method will return 3;
        /// </code>
        /// </example>
        private static int RemainingEnemiesOnRound()
        {
            return _roundfixer.TotalEnemies;
        }
        /// <summary>
        /// Resets to level1.
        /// </summary>
        public static void ResetToLevel1()
        {
            ResetRound();
            ResetHealthAndScore();
            ResetEnemies();
        }
        /// <summary>
        /// Resets the round back to 1
        /// </summary>
        private static void ResetRound()
        {
            round = 1;
        }
        /// <summary>
        /// Resets the enemies setup <see cref="RoundSetup"/> back to level 1 settings
        /// </summary>
        private static void ResetEnemies()
        {
            _roundfixer = new RoundSetup(1, 2, 0, 0, 10);
        }
        /// <summary>
        /// Resets the health and score.
        /// </summary>
        private static void ResetHealthAndScore()
        {
            GameHud.HealthBar.Offset = 0;
            GameHud.Score = 0;
        }
        /// <summary>
        /// Increases the enemies.
        /// </summary>
        /// <param name="enemies">Takes in the round which multiples the enemies for the next round</param>
        private static void IncreaseEnemies(int round)
        {
            enemies = 2 * round;
            if (round > 4)
                specialEnemies = round / 2;
            if (round > 8)
                eliteEnemies = round / 4;
        }
        /// <summary>
        /// Resets the shop timer.
        /// </summary>
        /// <param name="shoptimer">The shoptimer.</param>
        private static void ResetShopTimer(int shoptimer)
        {
            Levels.shoptimer = shoptimer;
        }
        /// <summary>
        /// Increases the round.
        /// </summary>
        /// <param name="round">The round.</param>
        private static void IncreaseRound(int round)
        {
            Levels.round = round;
        }
        /// <summary>
        /// Changes the round to the next round.
        /// </summary>
        public static void NextLevel()
        {
            PrepareForNextRound();
            _roundfixer = new RoundSetup(round, enemies, specialEnemies, eliteEnemies, shoptimer);
        }
        /// <summary>
        /// Prepares for next round.
        /// </summary>
        private static void PrepareForNextRound()
        {
            IncreaseRound(round + 1);
            IncreaseEnemies(round);
            ResetShopTimer(10);
        }
    }
}
