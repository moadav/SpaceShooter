using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// abstract class that helps with the drawing logic
    /// </summary>
    public abstract class DrawLogic
    {
        /// <summary>
        /// Draws the bullets.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">Specifies which enemy that is going to be drawn.</param>
        public abstract void DrawBullets(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i);
        /// <summary>
        /// How the shooting logic is of the enemy
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The shooting logic of the specified enemy.</param>
        public abstract void ShootLogic(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i);
        /// <summary>
        /// Shoots an bitmap bullet
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public abstract void Shoot(CanvasBitmap bitmap, CanvasDrawEventArgs args);
        /// <summary>
        /// Handles shoot functionality.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public abstract void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args);
        /// <summary>
        /// Draws the enemies.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">The specified enemies that is going to be drawn.</param>
        public abstract void DrawEnemies(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i);

    }
}
