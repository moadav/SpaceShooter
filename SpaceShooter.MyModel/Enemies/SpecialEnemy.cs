using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// class that handles the spesial enemy functionality
    /// </summary>
    /// <seealso cref="SpaceShooter.MyModel.DrawLogic" />
    /// <seealso cref="SpaceShooter.MyModel.IEnemySetup" />
    /// <seealso cref="SpaceShooter.MyModel.ShootAtTarget" />
    public class SpecialEnemy : DrawLogic, IEnemySetup, ShootAtTarget
    {
        private readonly CheckForEnemyCollision collision  = new CheckForEnemyCollision();
        private readonly EnemyHelper helperClass = new EnemyHelper();
        /// <summary>
        /// a list of spesial enemy x bullet positions
        /// </summary>
        public static List<float> SpesialEnemyXBulletposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of spesial enemy y bullet positions
        /// </summary>
        public static List<float> SpesialEnemyYBulletposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of spesial enemy x positions
        /// </summary>
        public static List<float> SpesialEnemyXposisjon { get; set; } = new List<float>();
        /// <summary>
        /// a list of spesial enemy y positions
        /// </summary>
        public static List<float> SpesialEnemyYposisjon { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the spesial enemyshoot dir.
        /// </summary>
        /// <returns>
        /// A list of which direction to shoot.
        /// </returns>
        private static List<float> SpesialEnemyShootDir { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the spesialenemyshoottimer.
        /// </summary>
        /// <returns>
        /// the time left to shoot
        /// </returns>
        private static int Spesialenemyshoottimer { get; set; } = 720;
        /// <summary>
        /// the damage of spesial enemies
        /// </summary>
        public static float SpecialEnemyDamage { get; set; } = 0.5f;
        /// <summary>
        /// Spawns enemies to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpawnEnemies(object sender, object e)
        {
            if (EnemiesToSpawn() > 0)
            {
                AddEnemies(ReturnRandomYPosition.ReturnRandomY());
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
            return Levels._roundfixer.SpecialEnemies;
        }
        /// <summary>
        /// Adds enemies to lists
        /// </summary>
        /// <param name="startingYPosition"> the y position of the enemy </param>
        public void AddEnemies(int startingYPosition)
        {

            SpesialEnemyXposisjon.Add(1800);
            SpesialEnemyYposisjon.Add(startingYPosition);
        }
        /// <summary>
        /// Decreases the enemies to spawn
        /// </summary>
        public void DecreaseEnemiesToSpawn()
        {
            Levels._roundfixer.SpecialEnemies--;
        }
        /// <summary>
        /// Draws the bitmap of each enemy, and runs enemy logic.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            SpawnEnemyBullets();
            for (int i = 0; i < SpesialEnemyXposisjon.Count; i++)
                DrawEnemies(bitmap, args, i);
        }
        /// <summary>
        /// Draws the enemies.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The specified enemies that is going to be drawn.</param>
        public override void DrawEnemies(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, SpesialEnemyXposisjon[i], SpesialEnemyYposisjon[i]);
            MovementPatternOfEachEnemy(i);
        }
        /// <summary>
        /// Handles shoot functionality.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public override void Shoot(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < SpesialEnemyXBulletposisjon.Count; i++)
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
        /// Checks whether there is collision happening
        /// </summary>
        /// <param name="index"> specifies the specific enemy to check if there is collision</param>
        public void CheckIfCollision(int i)
        {
            if (collision.Collision(SpesialEnemyXBulletposisjon, SpesialEnemyYBulletposisjon, i))
                IfCollision(i);
        }
        /// <summary>
        /// method that runs if there is collision <see cref="CheckIfCollision(int)"/>
        /// </summary>
        /// <param name="index"> specifies what the happens if to the enemy</param>
        public void IfCollision(int i)
        {
            DecreasePlayerHealth(helperClass.Damage(SpecialEnemyDamage));
            AddHitmarker(i);
            RemoveBullets(i);
            RemoveShootingDirection(i);
        }
        /// <summary>
        /// Removes the shooting direction.
        /// </summary>
        /// <param name="i">Takes in a int i</param>
        public void RemoveShootingDirection(int i)
        {
            SpesialEnemyShootDir.RemoveAt(i);
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
        /// Adds hitmarkers to lists <see cref="Hit.GetHitXpos"/> <see cref="Hit.GetHitYpos"/>
        /// </summary>
        /// <param name="i"> specifies position of the hitmarker</param>
        public void AddHitmarker(int i)
        {
            Hit.GetHitXpos.Add(SpesialEnemyXBulletposisjon[i]);
            Hit.GetHitYpos.Add(SpesialEnemyYBulletposisjon[i]);
        }
        /// <summary>
        /// Removes bullets from lists
        /// </summary>
        /// <param name="i"> specifies the bullet that is going to be removed</param>
        public void RemoveBullets(int i)
        {
            SpesialEnemyXBulletposisjon.RemoveAt(i);
            SpesialEnemyYBulletposisjon.RemoveAt(i);
        }
        /// <summary>
        /// Draws the bullets.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">Specifies which enemy that is going to be drawn.</param>
        public override void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, SpesialEnemyXBulletposisjon[i], SpesialEnemyYBulletposisjon[i] + 110);
            BulletMovement(i);
        }
        /// <summary>
        /// Shoots directly at the players position
        /// </summary>
        /// <param name="i"> the value of y bullet position distance the bullet should go to reach the player</param>
        public void ShootAtPlayer(int i)
        {
            SpesialEnemyYBulletposisjon[i] -= SpesialEnemyShootDir[i];
        }
        /// <summary>
        /// The Bullets movement.
        /// </summary>
        /// <param name="i"> specifies the speed of bullets to the i parameter</param>
        public void BulletMovement(int i)
        {
            SpesialEnemyXBulletposisjon[i] -= 1;
        }
        /// <summary>
        /// Spawns enemy bullets and runs functionality for the bullets"/>
        /// </summary>
        public void SpawnEnemyBullets()
        {
            for (int i = 0; i < SpesialEnemyXposisjon.Count; i++)
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
            AddShootingDirection(i);
            ResetShootTimer();
        }
        /// <summary>
        /// Resets shooting timer
        /// </summary>
        public void ResetShootTimer()
        {
            Spesialenemyshoottimer = 1240;
        }
        /// <summary>
        /// Returns the shoot timer of the enemy
        /// </summary>
        /// <returns>
        /// and int value that is the shoot timer
        /// </returns>
        public int ShootTimer()
        {
            return Spesialenemyshoottimer;
        }
        /// <summary>
        /// adds shooting directions to a list
        /// </summary>
        /// <param name="index"> the index of enemy y position list</param>
        public void AddShootingDirection(int i)
        {
            SpesialEnemyShootDir.Add(helperClass.ShootAtTarget((SpesialEnemyYposisjon[i] - Player.defaultPlayerCoords.StartPosisjonY + 250) / 1000));
        }
        /// <summary>
        /// Adds bullets to lists.
        /// </summary>
        /// <param name="i">adds the bullet of index i.</param>
        public void AddBullets(int i)
        {
            SpesialEnemyXBulletposisjon.Add(SpesialEnemyXposisjon[i]);
            SpesialEnemyYBulletposisjon.Add(SpesialEnemyYposisjon[i]);
        }
        /// <summary>
        /// Decreases the shoot timer of the enemy
        /// </summary>
        public void DecreaseShootTimer()
        {
            Spesialenemyshoottimer -= 1;
        }
        /// <summary>
        /// Moves the enemy left
        /// </summary>
        /// <param name="index"> specifies the enemy to move left </param>
        public void MoveLeft(int value)
        {
            SpesialEnemyXposisjon[value] -= 2;
        }
        /// <summary>
        /// Handles movement patterns of each enemy
        /// </summary>
        /// <param name="index"> specifies which enemy to handle the movement of</param>
        public void MovementPatternOfEachEnemy(int index)
        {
            MoveLeft(index);
            if (SpesialEnemyXposisjon[index] < 1600)
                StopMoving(index);
        }
        /// <summary>
        /// stops the enemy from moving
        /// </summary>
        /// <param name="index"> specifies which enemy to stop from moving</param>
        public void StopMoving(int index)
        {
            SpesialEnemyXposisjon[index] += 2;
        }
    }
}
