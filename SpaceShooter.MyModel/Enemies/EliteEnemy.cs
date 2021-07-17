using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;
namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles the elite enemy functionality
    /// </summary>
    /// <seealso cref="SpaceShooter.MyModel.DrawLogic" />
    /// <seealso cref="SpaceShooter.MyModel.IEnemySetup" />
    /// <seealso cref="SpaceShooter.MyModel.MoveEnemyUpAndDown" />
    public class EliteEnemy : DrawLogic, IEnemySetup, MoveEnemyUpAndDown, ShootAtTarget
    {
        
        private readonly CheckForEnemyCollision collision = new CheckForEnemyCollision();
     
        private readonly EnemyHelper helperClass = new EnemyHelper();
        /// <summary>
        /// a list of elite enemy x bullet positions
        /// </summary>
        public static List<float> EliteEnemyXBulletposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of elite enemy y bullet positions
        /// </summary>
        public static List<float> EliteEnemyYBulletposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of elite enemy x positions
        /// </summary>
        public static List<float> EliteEnemyXposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of elite enemy y positions
        /// </summary>
        public static List<float> EliteEnemyYposisjon { get; set; } = new List<float>();
        /// <summary>
        /// the damage of elite enemies
        /// </summary>
        public static float EliteEnemyDamage { get; set; } = 0.6f;
        /// <summary>
        /// Gets the elite enemy shoot dir.
        /// </summary>
        /// <returns>
        /// a list containing The elite enemy shoot direction.
        /// </returns>
        private List<float> EliteEnemyShootDir { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the elite enemy shoot timer.
        /// </summary>
        /// <returns>
        /// The elite enemy shoot timer.
        /// </returns>
        private int EliteEnemyShootTimer { get; set; } = 620;

        private static List<string> EliteEnemyMovementDirection { get; set; } = new List<string>();

        /// <summary>
        /// Draws the bitmap of each enemy, and runs enemy logic.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < EliteEnemyXposisjon.Count; i++)
                EachEliteEnemyLogic(bitmap, args, i);
        }
        /// <summary>
        /// Runs functionality for each elite enemy
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The i index </param>
        private void EachEliteEnemyLogic(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            DrawEnemies(bitmap, args, i);
            MovementPatternOfEachEnemy(i);
        }
        /// <summary>
        /// Draws the enemies.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The specified enemies that is going to be drawn.</param>
        public override void DrawEnemies(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, EliteEnemyXposisjon[i], EliteEnemyYposisjon[i]);
        }
        /// <summary>
        /// Handles shoot functionality.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Shoot(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {

            SpawnEnemyBullets();
            for (int i = 0; i < EliteEnemyXBulletposisjon.Count; i++)
                ShootLogic(bitmap, args, i);
        }
        /// <summary>
        /// How the shooting logic is of the enemy
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The shooting logic of the specified enemy.</param>
        public override void ShootLogic(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            DrawBullets(bitmap, args, i);
            ShootAtPlayer(i);
            CheckIfCollision(i);
        }
        /// <summary>
        /// Draws the bullets.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">Specifies which enemy that is going to be drawn.</param>
        public override void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, EliteEnemyXBulletposisjon[i], EliteEnemyYBulletposisjon[i] + 110);
            BulletMovement(i);
        }
        /// <summary>
        /// The Bullets movement.
        /// </summary>
        /// <param name="i"> specifies the speed of bullets to the i parameter</param>
        public void BulletMovement(int i)
        {
            EliteEnemyXBulletposisjon[i] -= 1;
        }
        /// <summary>
        /// Shoots directly at the players position
        /// </summary>
        /// <param name="i"> the value of y bullet position distance the bullet should go to reach the player</param>
        public void ShootAtPlayer(int i)
        {
            EliteEnemyYBulletposisjon[i] -= EliteEnemyShootDir[i];
        }
        /// <summary>
        /// Checks whether there is collision happening
        /// </summary>
        /// <param name="index"> specifies the specific enemy to check if there is collision</param>
        public void CheckIfCollision(int index)
        {
            if (collision.Collision(EliteEnemyXBulletposisjon, EliteEnemyYBulletposisjon, index))
                IfCollision(index);
        }
        /// <summary>
        /// method that runs if there is collision <see cref="CheckIfCollision(int)"/>
        /// </summary>
        /// <param name="index"> specifies what the happens if to the enemy</param>
        public void IfCollision(int index)
        {
            AddHitmarker(index);
            RemoveBullets(index);
            DecreasePlayerHealth(helperClass.Damage(EliteEnemyDamage));
            RemoveShootingDirection(index);
        }
        /// <summary>
        /// Removes the shooting direction.
        /// </summary>
        /// <param name="i">Takes in a int i"</param>
        public void RemoveShootingDirection(int i)
        {
            EliteEnemyShootDir.RemoveAt(i);
        }
        /// <summary>
        /// Decreases player health
        /// </summary>
        /// <param name="decreaseHealthAmount"> the float amount of how much damage the player has taken</param>
        public void DecreasePlayerHealth(float decreaseHealthAmount)
        {
            GameHud.HealthBar.Offset += decreaseHealthAmount;
        }
        /// <summary>
        /// Removes bullets from lists
        /// </summary>
        /// <param name="i"> specifies the bullet that is going to be removed</param>
        public void RemoveBullets(int i)
        {
            EliteEnemyXBulletposisjon.RemoveAt(i);
            EliteEnemyYBulletposisjon.RemoveAt(i);
        }
        /// <summary>
        /// Adds hitmarkers to lists <see cref="Hit.GetHitXpos"/> <see cref="Hit.GetHitYpos"/>
        /// </summary>
        /// <param name="i"> specifies position of the hitmarker</param>
        public void AddHitmarker(int i)
        {
            Hit.GetHitXpos.Add(EliteEnemyXBulletposisjon[i]);
            Hit.GetHitYpos.Add(EliteEnemyYBulletposisjon[i]);
        }
        /// <summary>
        /// Spawns enemy bullets and runs functionality for the bullets"/>
        /// </summary>
        public void SpawnEnemyBullets()
        {
            for (int i = 0; i < EliteEnemyXposisjon.Count; i++)
                ShootTimer(i);
        }
        /// <summary>
        /// The shootimer of elite enemy
        /// </summary>
        /// <param name="i">The i index</param>
        private void ShootTimer(int i)
        {
            if (ShootTimer() == 0)
                TimeToShoot(i);
            else
                DecreaseShootTimer();
        }
        /// <summary>
        /// Returns the shoot timer of the enemy
        /// </summary>
        /// <returns>
        /// and int value that is the shoot timer
        /// </returns>
        public int ShootTimer()
        {
            return EliteEnemyShootTimer;
        }
        /// <summary>
        /// Decreases the shoot timer of the enemy
        /// </summary>
        public void DecreaseShootTimer()
        {
            EliteEnemyShootTimer -= 1;
        }
        /// <summary>
        /// Shoots the bullet
        /// </summary>
        /// <param name="i"> the i value that is going to be shoot from lists</param>
        public void TimeToShoot(int index)
        {
            AddBullets(index);
            AddShootingDirection(index);
            ResetShootTimer();
        }
        /// <summary>
        /// Resets shooting timer
        /// </summary>
        public void ResetShootTimer()
        {
            EliteEnemyShootTimer = 1000;
        }
        /// <summary>
        /// Adds bullets to lists
        /// </summary>
        /// <param name="i"> adds the i position of the enemy to the lists of bullets</param>
        public void AddBullets(int i)
        {
            EliteEnemyXBulletposisjon.Add(EliteEnemyXposisjon[i]);
            EliteEnemyYBulletposisjon.Add(EliteEnemyYposisjon[i]);
        }
        /// <summary>
        /// adds shooting directions to a list
        /// </summary>
        /// <param name="index"> the index of enemy y position list</param>
        public void AddShootingDirection(int index)
        {
            EliteEnemyShootDir.Add(helperClass.ShootAtTarget((EliteEnemyYposisjon[index] - Player.defaultPlayerCoords.StartPosisjonY + 250) / 1000));
        }
        /// <summary>
        /// Handles movement patterns of each enemy
        /// </summary>
        /// <param name="index"> specifies which enemy to handle the movement of</param>
        public void MovementPatternOfEachEnemy(int index)
        {
            StopMoving(index);
            if (EliteEnemyXposisjon[index] < 1600)
                IfEnemyMovementDestinationIsReached(index);
        }
        /// <summary>
        /// stops the enemy from moving
        /// </summary>
        /// <param name="index"> specifies which enemy to stop from moving</param>
        public void StopMoving(int index)
        {
            EliteEnemyXposisjon[index] -= 2;
        }
        /// <summary>
        /// Checks if the enemy has reached its max X destination on the screen
        /// </summary>
        /// <param name="index"> Specifies the enemy its going to check if it has reached its destination</param>
        public void IfEnemyMovementDestinationIsReached(int index)
        {
            MoveLeft(index);
            MovementLogic(index);
        }
        /// <summary>
        /// The movement logic of elite enemy
        /// </summary>
        /// <param name="index">The index.</param>
        private void MovementLogic(int index)
        {
            MoveEnemyUpAndDown(index);
            IfCornerHit(index);
        }
        /// <summary>
        /// Moves the enemy up and down the screen
        /// </summary>
        /// <param name="index"> The specific enemy that is going move either up or down</param>
        public void MoveEnemyUpAndDown(int index)
        {
            if (EliteEnemyMovementDirection[index].Equals("down"))
                MoveEnemyDown(index);
            else if (EliteEnemyMovementDirection[index].Equals("up"))
                MoveEnemyUp(index);
        }
        /// <summary>
        /// Moves the enemy down
        /// </summary>
        /// <param name="index"> Specifies the enemy to move down</param>
        public void MoveEnemyDown(int index)
        {
            EliteEnemyYposisjon[index] += 0.6f;
        }
        /// <summary>
        /// Moves the enemy up
        /// </summary>
        /// <param name="index">Specifies the enemy to move up </param>
        public void MoveEnemyUp(int index)
        {
            EliteEnemyYposisjon[index] -= 0.6f;
        }
        /// <summary>
        /// Checks if enemy has hit the bottom of the screen or the top of the screen
        /// </summary>
        /// <param name="index"> Specifies the enemy to check if it has hit the corners</param>
        public void IfCornerHit(int index)
        {
            if (EliteEnemyYposisjon[index] > 800)
                ChangeDirectionToUp(index);
            else if (EliteEnemyYposisjon[index] < 0)
                ChangeDirectionToDown(index);
        }
        /// <summary>
        /// Changes direction of the enemy to go up
        /// </summary>
        /// <param name="index"> Specifies the enemy to move up</param>
        public void ChangeDirectionToUp(int index)
        {
            EliteEnemyMovementDirection[index] = "up";
        }
        /// <summary>
        /// Changes direction of the enemy to go down
        /// </summary>
        /// <param name="index"> Specifies the enemy to move down</param>
        public void ChangeDirectionToDown(int index)
        {
            EliteEnemyMovementDirection[index] = "down";
        }

        /// <summary>
        /// Spawns enemies to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpawnEnemies(object sender, object e)
        {
            if (EnemiesToSpawn() > 0)
            {
                IfThereEliteEnemiesToSpawn();
                DecreaseEnemiesToSpawn();
                AddEnemies(ReturnRandomYPosition.ReturnRandomY());
            }
        }

        /// <summary>
        /// Checks if there are more enemies to spawn
        /// </summary>
        /// <returns>
        /// an int value of how many enemies there are left to spawn
        /// </returns>
        public int EnemiesToSpawn()
        {
            return Levels._roundfixer.EliteEnemy;
        }
        /// <summary>
        /// Decreases the enemies to spawn
        /// </summary>
        public void DecreaseEnemiesToSpawn()
        {
            Levels._roundfixer.EliteEnemy--;
        }
        /// <summary>
        /// If there are enemies to spawn, add direction for the enemy
        /// </summary>
        private void IfThereEliteEnemiesToSpawn()
        {
            AddEnemyDirections(ReturnRandomYPosition.ReturnRandomY());
        }
        /// <summary>
        /// Adds enemies to lists
        /// </summary>
        /// <param name="startingYPosition"> the y position of the enemy </param>
        public void AddEnemies(int startingYPosition)
        {
            EliteEnemyXposisjon.Add(1800);
            EliteEnemyYposisjon.Add(ReturnRandomYPosition.ReturnRandomY());
        }
        /// <summary>
        /// Adds direction for the enemy to move
        /// </summary>
        /// <param name="startingYPosisjon"> the y position of the enemy</param>
        public void AddEnemyDirections(int startingYPosisjon)
        {
            if (startingYPosisjon < 350)
                EliteEnemyMovementDirection.Add("down");
            else
                EliteEnemyMovementDirection.Add("up");
        }
        /// <summary>
        /// Moves the enemy left
        /// </summary>
        /// <param name="index"> specifies the enemy to move left </param>
        public void MoveLeft(int index)
        {
            EliteEnemyXposisjon[index] += 2;
        }

    }
}
