using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using Windows.UI.Core;

namespace SpaceShooter.MyModel
{

    /// <summary>
    /// This class handles all player functionality
    /// </summary>
    public class Player
    {
        readonly PlayerHelper playerHelper = new PlayerHelper();

        /// <summary>
        /// uses a PlayerCoords to handle the starting positions of the player <see cref="PlayerCoords"/>
        /// </summary>
        public static PlayerCoords defaultPlayerCoords = new PlayerCoords(350, 350);
        /// <summary>
        /// Gets or sets the xbulletposisjon list.
        /// </summary>
        /// <returns>
        /// a list of bullets with the desired x float positions
        /// </returns>
        /// <example>
        /// <code>
        /// xBulletPosition.add(212);
        /// </code>
        /// </example>
        public static List<float> XBulletPosisjon { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the ybulletposisjon list.
        /// </summary>
        /// <returns>
        /// a list of bullets with the desired y float positions
        /// </returns>
        /// <example>
        /// <code>
        /// yBulletPosition.add(212);
        /// </code>
        /// </example>
        public static List<float> YBulletPosisjon { get; set; } = new List<float>();

        private readonly int speed = 30;
        private readonly int movebulletXPosisjon = -150;
        private readonly int movebulletYPosisjon = -170;
        public Player()
        {

        }


        /// <summary>
        /// The functionality of the player movement
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public void PlayerMovement(CoreWindow sender, KeyEventArgs e)
        {
            Playzone();
            if (e.VirtualKey == Windows.System.VirtualKey.D)
                MoveRight();
            else if (e.VirtualKey == Windows.System.VirtualKey.A)
                MoveLeft();
            else if (e.VirtualKey == Windows.System.VirtualKey.W)
                MoveUp();
            else if (e.VirtualKey == Windows.System.VirtualKey.S)
                MoveDown();
            else if (e.VirtualKey == Windows.System.VirtualKey.V)
                Shoot();
        }
        /// <summary>
        /// Moves the player left.
        /// </summary>
        private void MoveLeft()
        {
            defaultPlayerCoords.StartPosisjonX -= speed;
        }
        /// <summary>
        /// Moves the player right
        /// </summary>
        private void MoveRight()
        {
            defaultPlayerCoords.StartPosisjonX += speed;
        }
        /// <summary>
        /// Moves the player up
        /// </summary>
        private void MoveUp()
        {
            defaultPlayerCoords.StartPosisjonY -= speed;
        }
        /// <summary>
        /// Moves the player down.
        /// </summary>
        private void MoveDown()
        {
            defaultPlayerCoords.StartPosisjonY += speed;
        }

        /// <summary>
        /// Shoots bullets
        /// </summary>
        private void Shoot()
        {
            if (GameHud.AmmoBar.Offset <= 4)
            {
                ReduceAmmoBar();
                AddBullets();
            }
        }
        /// <summary>
        /// Reduces the ammo bar.
        /// </summary>
        private void ReduceAmmoBar()
        {
            GameHud.AmmoBar.Offset += 0.2;
        }
        /// <summary>
        /// Adds the bullets.
        /// </summary>
        private void AddBullets()
        {
            XBulletPosisjon.Add(defaultPlayerCoords.StartPosisjonX);
            YBulletPosisjon.Add(defaultPlayerCoords.StartPosisjonY);
        }

        /// <summary>
        /// Draws the bullets to the screen.
        /// </summary>
        /// <param name="bitmap">The bitmap which is going to be drawn </param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < XBulletPosisjon.Count; i++)
                IfShooting(bitmap, args, i);
        }
        /// <summary>
        /// Checks if the player is shooting
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The i bullet indexes.</param>
        private void IfShooting(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            Collision(i);
            DrawBullets(bitmap, args, i);
            ShootDirection(i);
        }
        /// <summary>
        /// Draws the bullets.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The i bullet indexes</param>
        private void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, XBulletPosisjon[i] + movebulletXPosisjon, YBulletPosisjon[i] + movebulletYPosisjon);
        }
        /// <summary>
        /// fixes the shoot direction of the player
        /// </summary>
        /// <param name="i">The i bullet indexes</param>
        private void ShootDirection(int i)
        {
            XBulletPosisjon[i] += 6;
            IfBulletOutOfBounds(i);
        }
        /// <summary>
        /// Max length in which the player can move around the screen
        /// </summary>
        private void Playzone()
        {
            MaxYLengthValues();
            MaxXLengthValues();
        }
        /// <summary>
        /// Fixes the max y direction the player can travel to <see cref="Playzone"/>
        /// </summary>
        private void MaxYLengthValues()
        {
            if (defaultPlayerCoords.StartPosisjonY > 1100)
                defaultPlayerCoords.StartPosisjonY = 1100;

            else if (defaultPlayerCoords.StartPosisjonY < 150)
                defaultPlayerCoords.StartPosisjonY = 150;
        }

        /// <summary>
        /// Fixes the max x direction the player can travel to <see cref="Playzone"/>
        /// </summary>
        private void MaxXLengthValues()
        {
            if (defaultPlayerCoords.StartPosisjonX > 1400)
                defaultPlayerCoords.StartPosisjonX = 1400;

            else if (defaultPlayerCoords.StartPosisjonX < 250)
                defaultPlayerCoords.StartPosisjonX = 250;
        }
        /// <summary>
        /// Checks if there are any collisions with the enemy
        /// </summary>
        /// <param name="index">The index of the enemy lists.</param>
        private void CheckIfEnemyCollision(int index)
        {
            for (int j = 0; j < Enemy.EnemyXposisjon.Count; j++)
                if (ForEveryEnemyCheckIfCollision(index, j))
                    RemoveEnemy(j);
        }
        /// <summary>
        /// Checks for every enemy that spawns, if there are any collisions
        /// </summary>
        /// <param name="index">The index of the player bullet </param>
        /// <param name="j">The index of enemy lists</param>
        /// <returns>
        /// true or false whether there have been any collisions
        /// </returns>
        private bool ForEveryEnemyCheckIfCollision(int index, int j)
        {
            return playerHelper.CollisionHandler(1, XBulletPosisjon, YBulletPosisjon, Enemy.EnemyXposisjon[j], Enemy.EnemyYposisjon[j], index, 165, 200, 140, 280);
        }
        /// <summary>
        /// Removes the enemy.
        /// </summary>
        /// <param name="i">The i index.</param>
        private void RemoveEnemy(int i)
        {
            Enemy.EnemyXposisjon.RemoveAt(i);
            Enemy.EnemyYposisjon.RemoveAt(i);

        }
        /// <summary>
        /// Checks if the bullet is out of bounds
        /// </summary>
        /// <param name="i">The i index.</param>
        private void IfBulletOutOfBounds(int i)
        {
            if (XBulletPosisjon[i] > 2100)
            {
                XBulletPosisjon.RemoveAt(i);
                YBulletPosisjon.RemoveAt(i);
            }
        }
        /// <summary>
        /// Checks if there are any collisions with special enemies
        /// </summary>
        /// <param name="index">The index of the special enemies lists</param>
        private void CheckIfSpecialEnemyCollision(int index)
        {
            for (int h = 0; h < SpecialEnemy.SpesialEnemyXposisjon.Count; h++)
                if (ForEverySpecialEnemyCheckIfCollision(index, h))
                    RemoveSpecialEnemy(h);
        }
        /// <summary>
        /// Checks if there are any collisions with every special enemy on screen
        /// </summary>
        /// <param name="index">The index for bullet</param>
        /// <param name="h">The index for special enemy lists.</param>
        /// <returns>
        /// true if there is collision and false if not
        /// </returns>
        private bool ForEverySpecialEnemyCheckIfCollision(int index, int h)
        {
            return playerHelper.CollisionHandler(2, XBulletPosisjon, YBulletPosisjon, SpecialEnemy.SpesialEnemyXposisjon[h], SpecialEnemy.SpesialEnemyYposisjon[h], index, 170, 200, 270, 375);
        }
        /// <summary>
        /// Removes the special enemy.
        /// </summary>
        /// <param name="h">The index of the lists.</param>
        private void RemoveSpecialEnemy(int h)
        {
            SpecialEnemy.SpesialEnemyXposisjon.RemoveAt(h);
            SpecialEnemy.SpesialEnemyYposisjon.RemoveAt(h);

        }
        /// <summary>
        /// Checks if there are any collisions with elite enemies collision.
        /// </summary>
        /// <param name="index">The index.</param>
        private void CheckIfEliteEnemyCollision(int index)
        {
            for (int h = 0; h < EliteEnemy.EliteEnemyXposisjon.Count; h++)
                if (ForEveryEliteEnemyCheckCollision(index, h))
                    RemoveEliteEnemy(h);
        }
        /// <summary>
        /// Checks if there are any collisions with every elite enemy on screen
        /// </summary>
        /// <param name="index">The index for bullet</param>
        /// <param name="h">The index for elite enemy lists.</param>
        /// <returns>
        /// true if there is collision and false if not
        /// </returns>
        private bool ForEveryEliteEnemyCheckCollision(int index, int h)
        {
            return playerHelper.CollisionHandler(3, XBulletPosisjon, YBulletPosisjon, EliteEnemy.EliteEnemyXposisjon[h], EliteEnemy.EliteEnemyYposisjon[h], index, 265, 355, 240, 365);
        }
        /// <summary>
        /// Removes the elite enemy.
        /// </summary>
        /// <param name="h">The h.</param>
        private void RemoveEliteEnemy(int h)
        {
            EliteEnemy.EliteEnemyXposisjon.RemoveAt(h);
            EliteEnemy.EliteEnemyYposisjon.RemoveAt(h);

        }
        /// <summary>
        /// Collision method, that checks if there are any collision with enemies on screen <see cref="IfThereIsAnyCollision(int)"/>
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="CollisionsDetectedException">
        /// Give exception if the collision method does not work
        /// </exception>
        private void Collision(int index)
        {
            try
            {
                IfThereIsAnyCollision(index);

            }
            catch (CollisionsDetectedException odex)
            {
                Console.WriteLine(odex.Message.ToString() + "MultipleCollisionsDetectedException, Collision method not working properly");
            }
        }
        /// <summary>
        /// Method to see if there is collision happening on screen
        /// </summary>
        /// <param name="index">The index of the enemies list</param>
        private void IfThereIsAnyCollision(int index)
        {
            CheckIfEnemyCollision(index);
            CheckIfSpecialEnemyCollision(index);
            CheckIfEliteEnemyCollision(index);
        }
    }
}

