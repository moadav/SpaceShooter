using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class to handle Hindrance functionality
    /// </summary>
    /// <seealso cref="SpaceShooter.MyModel.IHindrance" />
    public class Hindrance : IHindrance
    {
        /// <summary>
        /// Gets or sets the throwables xposisjon.
        /// </summary>
        /// <returns>
        /// A list with desired x position float values
        /// </returns>
        /// <example>
        /// <code>
        /// ThrowablesXposisjon.Add(11.2f);
        /// </code>
        /// </example>
        public static List<float> ThrowablesXposisjon { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the throwables yposisjon.
        /// </summary>
        /// <returns>
        /// A list with desired y position float values
        /// </returns>
        /// <example>
        /// <code>
        /// ThrowablesYposisjon.Add(4.2f);
        /// </code>
        /// </example>
        public static List<float> ThrowablesYposisjon { get; set; } = new List<float>();
        private readonly int travelSpeed = 2;
        /// <summary>
        /// Gets or sets the hindrance damage.
        /// </summary>
        /// <returns>
        /// a float value which specifies the damage output
        /// </returns>
        /// <example>
        /// <code>
        /// HindranceDamage = 0.3f;
        /// </code>
        /// </example>
        public static float HindranceDamage { get; set; } = 0.8f;

        private readonly CheckForEnemyCollision collision = new CheckForEnemyCollision();

        private readonly EnemyHelper enemyHelper = new EnemyHelper();

        /// <summary>
        /// Spawns the asteroid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void SpawnAsteroid(object sender, object e)
        {
            AddAsteroid(ReturnRandomYPosition.ReturnRandomY());
        }
        /// <summary>
        /// Adds asteroid to lists
        /// </summary>
        /// <param name="startingYPosition"></param>
        private void AddAsteroid(int startingYPosition)
        {
            ThrowablesXposisjon.Add(1800);
            ThrowablesYposisjon.Add(startingYPosition);
        }
        /// <summary>
        /// Draws the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < ThrowablesXposisjon.Count; i++)
                ForEachHindrance(bitmap, args, i);
        }
        /// <summary>
        /// Checks each hindrance
        /// </summary>
        /// <param name="bitmap"> The bitmap that is going to be drawn</param>
        /// <param name="args"> takes in a event <see cref="CanvasDrawEventArgs"/> </param>
        /// <param name="i"> the i index to draw</param>
        private void ForEachHindrance(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            DrawHindrance(bitmap, args, i);
            if (collision.Collision(ThrowablesXposisjon, ThrowablesYposisjon, i))
                IfThereIsCollision(i);
        }
        /// <summary>
        /// Checks if there is collision
        /// </summary>
        /// <param name="i"> the i index to check if there is collision with</param>
        private void IfThereIsCollision(int i)
        {
            AddHitmarker(i);
            RemoveHindrance(i);
            DecreaseHealth(enemyHelper.Damage(HindranceDamage));
        }
        /// <summary>
        /// Draws the specific hindrance object.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i"> returns an int value that specifies which object to draw from lists <see cref="Hindrance.ThrowablesXposisjon"/> <see cref="Hindrance.ThrowablesYposisjon"/></param>
        public void DrawHindrance(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, ThrowablesXposisjon[i], ThrowablesYposisjon[i]);
            MoveHindranceLeft(i);
        }
        /// <summary>
        /// Decreases the player health
        /// </summary>
        /// <param name="decreaseHealth"> a float value to decrease the player health </param>
        private void DecreaseHealth(float decreaseHealth)
        {
            GameHud.HealthBar.Offset += decreaseHealth;
        }
        /// <summary>
        /// Moves the hindrance drawing left
        /// </summary>
        /// <param name="i"> the index of which hindrance</param>
        private void MoveHindranceLeft(int i)
        {
            ThrowablesXposisjon[i] -= travelSpeed;
        }
        /// <summary>
        /// Removes the hindrance from the screen
        /// </summary>
        /// <param name="i">
        /// the i index of which enemy to remove
        /// </param>
        private void RemoveHindrance(int i)
        {
            ThrowablesXposisjon.RemoveAt(i);
            ThrowablesYposisjon.RemoveAt(i);
        }
        /// <summary>
        /// Adds hitmarkers to screen
        /// </summary>
        /// <param name="i"> the index of which list positions to add</param>
        private void AddHitmarker(int i)
        {
            Hit.GetHitXpos.Add(ThrowablesXposisjon[i]);
            Hit.GetHitYpos.Add(ThrowablesYposisjon[i]);
        }

    }
}
