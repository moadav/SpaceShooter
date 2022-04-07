

namespace SpaceShooter.MyModel
{
    /// <summary>
    ///   This class handles the round setup
    /// </summary>
    public struct RoundSetup
    {
        /// <summary>
        ///  Gets or sets the round. 
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of what the current round is
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// Round = 4;
        /// </code>
        /// </example>
        public int Round { get; set; }
        /// <summary>
        ///  Gets or sets the enemies. 
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of how many current enemies that is going to be drawn to the screen
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// Enemies = 4;
        /// </code>
        /// </example>
        public int Enemies { get; set; }
        /// <summary>
        ///  Gets or sets the specialEnemies. 
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of how many SpecialEnemies that is going to be drawn to the screen
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// SpecialEnemies = 4;
        /// </code>
        /// </example>
        public int SpecialEnemies { get; set; }
        /// <summary>
        /// Gets or sets the totalEnemies. 
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of how many more enemies there are alive
        /// </returns>

        public int TotalEnemies { get; set; }
        /// <summary>
        ///  Gets or sets the end of round timer.
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of how many seconds the countdown should start from
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// ShopTimer = 10;
        /// </code>
        /// </example>
        public int ShopTimer { get; set; }
        /// <summary>
        ///  Gets or sets the eliteEnemies.
        /// </summary>
        /// 
        /// <returns>
        /// Returns an int of how many more eliteEnemies should be drawn
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// EliteEenemy = 4;
        /// </code>
        /// </example>
        public int EliteEnemy { get; set; }
        /// <summary>
        /// Constructor that handles how many different amount of enemies there should be drawn and how many seconds the shoptimer should have
        /// </summary>
        /// <param name="round"> what the round value should be.</param>
        /// <param name="enemies"> How many enemies there are in the specified round <see cref="Round"/> </param>
        /// <param name="specialenemies"> How many specialEnemies there are in the specified round <see cref="Round"/> </param>
        /// <param name="eliteEnemies"> How many eliteEnemies there are in the specified round <see cref="Round"/> </param>
        /// <param name="shoptimer"> Specifies the timelimit of how long the player can wait before timer for the portal <see cref="Shop.ShopCountdownInitiate()"/> runs out <see cref="Round"/> </param>
        /// <example>
        /// <code>
        /// RoundSetup = new RoundSetup(4,4,4,4,10);
        /// </code>
        /// </example>

        public RoundSetup(int round, int enemies, int specialenemies, int eliteEnemies, int shoptimer)
        {
            Enemies = enemies;
            SpecialEnemies = specialenemies;
            Round = round;
            EliteEnemy = eliteEnemies;
            TotalEnemies = this.Enemies + SpecialEnemies + EliteEnemy;
            ShopTimer = shoptimer;

        }
    }
}
