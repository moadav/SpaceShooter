using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;


namespace SpaceShooter.MyModel
{
    /// <summary>
    /// interface to handle 
    /// </summary>
    interface IHindrance
    {
        /// <summary>
        /// Spawns the asteroid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void SpawnAsteroid(object sender, object e);

        /// <summary>
        /// Draws the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args);
        /// <summary>
        /// Draws the specific hindrance object.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i"> returns an int value that specifies which object to draw from lists <see cref="Hindrance.ThrowablesXposisjon"/> <see cref="Hindrance.ThrowablesYposisjon"/></param>
        void DrawHindrance(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i);
    }
}
