
namespace SpaceShooter.MyModel
{
    /// <summary>
    /// interface to handle common Enemy functionality
    /// </summary>
    public interface IEnemySetup
    {
        /// <summary>
        /// Decreases the shoot timer of the enemy
        /// </summary>
        void DecreaseShootTimer();
        /// <summary>
        /// Resets the shoot timer.
        /// </summary>
        void ResetShootTimer();
        /// <summary>
        /// Adds the bullets.
        /// </summary>
        /// <param name="i">adds the bullet of index i.</param>
        void AddBullets(int i);
        /// <summary>
        /// Returns the shoot timer of the enemy
        /// </summary>
        /// <returns>
        /// and int value that is the shoot timer
        /// </returns>
        int ShootTimer();
        /// <summary>
        /// Adds hitmarkers to lists <see cref="Hit.GetHitXpos"/> <see cref="Hit.GetHitYpos"/>
        /// </summary>
        /// <param name="i"> specifies position of the hitmarker</param>
        void AddHitmarker(int i);
        /// <summary>
        /// Removes bullets from lists
        /// </summary>
        /// <param name="i"> specifies the bullet that is going to be removed</param>
        void RemoveBullets(int i);
        /// <summary>
        /// Decreases player health
        /// </summary>
        /// <param name="decreaseHealthAmount"> the float amount of how much damage the player has taken</param>
        void DecreasePlayerHealth(float decreaseHealthAmount);
        /// <summary>
        /// The Bullets movement.
        /// </summary>
        /// <param name="i"> specifies the speed of bullets to the i parameter</param>
        void BulletMovement(int i);
        /// <summary>
        /// Shoots the bullet
        /// </summary>
        /// <param name="i"> the i value that is going to be shoot from lists</param>
        void TimeToShoot(int i);
        /// <summary>
        /// Adds the enemies.
        /// </summary>
        /// <param name="startingYPosition">The starting y position of the enemy.</param>
        void AddEnemies(int startingYPosition);
        /// <summary>
        /// Decreases the enemies that should spawn.
        /// </summary>
        void DecreaseEnemiesToSpawn();
        /// <summary>
        /// specifies the amount of enemies that is going to spawn.
        /// </summary>
        /// <returns>
        /// a int value of how many enemies are spawning
        /// </returns>
        /// <example>
        /// <code>
        /// var shootimer = 0;
        ///  shootTimer(){
        ///  enemies = 21
        ///  return enemies ; 
        /// }
        /// </code>
        /// </example>
        int EnemiesToSpawn();
        /// <summary>
        /// Spawns the enemies.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void SpawnEnemies(object sender, object e);
        /// <summary>
        /// The movement pattern of each enemy
        /// </summary>
        /// <param name="index">Specifies the movement of a enemy.</param>
        void MovementPatternOfEachEnemy(int index);
        /// <summary>
        /// Moves to left.
        /// </summary>
        /// <param name="index"> specifies a enemy's movementpattern</param>
        void MoveLeft(int index);
        /// <summary>
        /// Stops moving.
        /// </summary>
        /// <param name="index">the specified enemy stops moving.</param>
        void StopMoving(int index);
        /// <summary>
        /// method that runs if there is collision <see cref="CheckIfCollision(int)"/>
        /// </summary>
        /// <param name="index"> specifies what the happens if to the enemy</param>
        void IfCollision(int i);
        /// <summary>
        /// Checks whether there is collision happening
        /// </summary>
        /// <param name="index"> specifies the specific enemy to check if there is collision</param>
        void CheckIfCollision(int i);
        /// <summary>
        /// Spawns enemy bullets and runs functionality for the bullets"/>
        /// </summary>
        void SpawnEnemyBullets();

    }
}
