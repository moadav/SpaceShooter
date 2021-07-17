using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles regular enemies functionality
    /// </summary>
    /// <seealso cref="SpaceShooter.MyModel.DrawLogic" />
    /// <seealso cref="SpaceShooter.MyModel.IEnemySetup" />
    /// <seealso cref="SpaceShooter.MyModel.MoveEnemyUpAndDown" />
    public class Enemy : DrawLogic, IEnemySetup, MoveEnemyUpAndDown
    {
      
        private readonly CheckForEnemyCollision collision = new CheckForEnemyCollision();
        private readonly EnemyHelper enemyHelper  = new EnemyHelper();
        /// <summary>
        /// a list of  enemy x positions
        /// </summary>
        public static List<float> EnemyXposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of enemy y positions
        /// </summary>
        public static List<float> EnemyYposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of enemy x bullet positions
        /// </summary>
        public static List<float> EnemyXBulletposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of  enemy y bullet positions
        /// </summary>
        public static List<float> EnemyYBulletposisjon { get; set; } = new List<float>();

        /// <summary>
        /// Gets or sets the enemy shoot timer.
        /// </summary>
        /// <returns>
        /// The enemy shoot timer.
        /// </returns>
        private static int EnemyShootTimer { get; set; } = 60;
        /// <summary>
        /// the damage of  enemies
        /// </summary>
        public static float EnemyDamage { get; set; } = 0.4f;

        private static List<string> EnemyMovementDirection { get; set; } = new List<string>();
        public Enemy()
        {

        }
        /// <summary>
        /// Handles movement patterns of each enemy
        /// </summary>
        /// <param name="index"> specifies which enemy to handle the movement of</param>
        public void MovementPatternOfEachEnemy(int index)
        {
            MoveLeft(index);
            if (EnemyXposisjon[index] < 1600)
                IfEnemyMovementDestinationIsReached(index);
        }
        /// <summary>
        /// Moves the enemy left
        /// </summary>
        /// <param name="index"> specifies the enemy to move left </param>
        public void MoveLeft(int index)
        {
            EnemyXposisjon[index] -= 2;
        }
        /// <summary>
        /// stops the enemy from moving
        /// </summary>
        /// <param name="index"> specifies which enemy to stop from moving</param>
        public void StopMoving(int index)
        {
            EnemyXposisjon[index] += 2;
        }
        /// <summary>
        /// Checks if the enemy has reached its max X destination on the screen
        /// </summary>
        /// <param name="index"> Specifies the enemy its going to check if it has reached its destination</param>
        public void IfEnemyMovementDestinationIsReached(int index)
        {
            StopMoving(index);
            MoveEnemyUpAndDown(index);
            IfCornerHit(index);
        }
        /// <summary>
        /// Moves the enemy up and down the screen
        /// </summary>
        /// <param name="index"> The specific enemy that is going move either up or down</param>
        public void MoveEnemyUpAndDown(int index)
        {
            if (EnemyMovementDirection[index].Equals("down"))
                MoveEnemyDown(index);
            else if (EnemyMovementDirection[index].Equals("up"))
                MoveEnemyUp(index);
        }
        /// <summary>
        /// Checks if enemy has hit the bottom of the screen or the top of the screen
        /// </summary>
        /// <param name="index"> Specifies the enemy to check if it has hit the corners</param>
        public void IfCornerHit(int index)
        {
            if (EnemyYposisjon[index] > 800)
                ChangeDirectionToUp(index);
            else if (EnemyYposisjon[index] < 0)
                ChangeDirectionToDown(index);
        }
        /// <summary>
        /// Changes direction of the enemy to go up
        /// </summary>
        /// <param name="index"> Specifies the enemy to move up</param>
        public void ChangeDirectionToUp(int index)
        {
            EnemyMovementDirection[index] = "up";
        }
        /// <summary>
        /// Changes direction of the enemy to go down
        /// </summary>
        /// <param name="index"> Specifies the enemy to move down</param>
        public void ChangeDirectionToDown(int index)
        {
            EnemyMovementDirection[index] = "down";
        }
        /// <summary>
        /// Moves the enemy down
        /// </summary>
        /// <param name="index"> Specifies the enemy to move down</param>
        public void MoveEnemyDown(int index)
        {
            EnemyYposisjon[index] += 0.6f;
        }
        /// <summary>
        /// Moves the enemy up
        /// </summary>
        /// <param name="index">Specifies the enemy to move up </param>
        public void MoveEnemyUp(int index)
        {
            EnemyYposisjon[index] -= 0.6f;
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
                IfThereAreEnemiesToSpawn();
                DecreaseEnemiesToSpawn();
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
            return Levels._roundfixer.Enemies;
        }
        /// <summary>
        /// Decreases the enemies to spawn
        /// </summary>
        public void DecreaseEnemiesToSpawn()
        {
            Levels._roundfixer.Enemies--;
        }

        /// <summary>
        /// If there are more enemies to spawn, spawn them
        /// </summary>
        private void IfThereAreEnemiesToSpawn()
        {

            AddEnemyDirections(ReturnRandomYPosition.ReturnRandomY());
            AddEnemies(ReturnRandomYPosition.ReturnRandomY());
        }
        /// <summary>
        /// Adds enemies to lists
        /// </summary>
        /// <param name="startingYPosition"> the y position of the enemy </param>
        public void AddEnemies(int startingYPosition)
        {
            EnemyXposisjon.Add(1800);
            EnemyYposisjon.Add(startingYPosition);
        }
        /// <summary>
        /// Adds direction for the enemy to move
        /// </summary>
        /// <param name="startingYPosisjon"> the y position of the enemy</param>
        public void AddEnemyDirections(int startingYPosition)
        {
            if (startingYPosition < 350)
                EnemyMovementDirection.Add("down");
            else
                EnemyMovementDirection.Add("up");
        }
        /// <summary>
        /// Draws the bitmap of each enemy, and runs enemy logic.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < EnemyXposisjon.Count; i++)
            {
                DrawEnemies(bitmap, args, i);
                MovementPatternOfEachEnemy(i);
            }
        }
        /// <summary>
        /// Draws the enemies.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The specified enemies that is going to be drawn.</param>
        public override void DrawEnemies(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, EnemyXposisjon[i], EnemyYposisjon[i]);
        }
        /// <summary>
        /// Handles shoot functionality.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Shoot(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            SpawnEnemyBullets();
            for (int i = 0; i < EnemyXBulletposisjon.Count; i++)
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
            CheckIfCollision(i);
        }
        /// <summary>
        /// Checks whether there is collision happening
        /// </summary>
        /// <param name="index"> specifies the specific enemy to check if there is collision</param>
        public void CheckIfCollision(int i)
        {
            if (collision.Collision(EnemyXBulletposisjon, EnemyYBulletposisjon, i))
                IfCollision(i);
        }
        /// <summary>
        /// Draws the bullets.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">Specifies which enemy that is going to be drawn.</param>
        public override void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, EnemyXBulletposisjon[i], EnemyYBulletposisjon[i] + 110);
            BulletMovement(i);
        }
        /// <summary>
        /// The Bullets movement.
        /// </summary>
        /// <param name="i"> specifies the speed of bullets to the i parameter</param>
        public void BulletMovement(int i)
        {
            EnemyXBulletposisjon[i] -= 1;
        }
        /// <summary>
        /// method that runs if there is collision <see cref="CheckIfCollision(int)"/>
        /// </summary>
        /// <param name="index"> specifies what the happens if to the enemy</param>
        public void IfCollision(int i)
        {
            AddHitmarker(i);
            RemoveBullets(i);
            DecreasePlayerHealth(enemyHelper.Damage(EnemyDamage));
        }
        /// <summary>
        /// Adds hitmarkers to lists <see cref="Hit.GetHitXpos"/> <see cref="Hit.GetHitYpos"/>
        /// </summary>
        /// <param name="i"> specifies position of the hitmarker</param>
        public void AddHitmarker(int i)
        {
            Hit.GetHitXpos.Add(EnemyXBulletposisjon[i]);
            Hit.GetHitYpos.Add(EnemyYBulletposisjon[i]);

        }
        /// <summary>
        /// Removes bullets from lists
        /// </summary>
        /// <param name="i"> specifies the bullet that is going to be removed</param>
        public void RemoveBullets(int i)
        {
            EnemyXBulletposisjon.RemoveAt(i);
            EnemyYBulletposisjon.RemoveAt(i);
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
        /// Spawns enemy bullets and runs functionality for the bullets
        /// </summary>
        public void SpawnEnemyBullets()
        {
            for (int i = 0; i < EnemyXposisjon.Count; i++)
            {
                if (ShootTimer() == 0)
                    TimeToShoot(i);
                else
                    DecreaseShootTimer();
            }
        }
        /// <summary>
        /// Shoots the bullet
        /// </summary>
        /// <param name="i"> the i value that is going to be shoot from lists</param>
        public void TimeToShoot(int i)
        {
            AddBullets(i);
            ResetShootTimer();
        }
        /// <summary>
        /// Resets shooting timer
        /// </summary>
        public void ResetShootTimer()
        {
            EnemyShootTimer = 700;
        }
        /// <summary>
        /// Decreases the shoot timer of the enemy
        /// </summary>
        public void DecreaseShootTimer()
        {
            EnemyShootTimer -= 1;
        }
        /// <summary>
        /// Adds bullets to lists
        /// </summary>
        /// <param name="i"> adds the i position of the enemy to the lists of bullets</param>
        public void AddBullets(int i)
        {
            EnemyXBulletposisjon.Add(EnemyXposisjon[i]);
            EnemyYBulletposisjon.Add(EnemyYposisjon[i]);
        }
        /// <summary>
        /// Returns the shoot timer of the enemy
        /// </summary>
        /// <returns>
        /// and int value that is the shoot timer
        /// </returns>
        public int ShootTimer()
        {
            return EnemyShootTimer;
        }
    }
}








