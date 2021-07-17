using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.Generic;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles the Hit functionality
    /// </summary>
    public class Hit
    {
        /// <summary>
        /// Gets or sets the getHitXpos.
        /// </summary>
        /// <returns>
        /// a list with x float values of desired x position
        /// </returns>
        /// <example>
        /// <code>
        /// getHitXpos.Add(21.2f);
        /// </code>
        /// </example>
        public static List<float> GetHitXpos { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the getHitYpos.
        /// </summary>
        /// <returns>
        /// a list with y float values of desired y positions
        /// </returns>
        /// <example>
        /// <code>
        /// getHitYpos.Add(51.2f);
        /// </code>
        /// </example>
        public static List<float> GetHitYpos { get; set; } = new List<float>();
        /// <summary>
        /// Gets or sets the boom disappearance timer.
        /// </summary>
        /// <value>
        /// The boom disappearance timer.
        /// </value>
        private int BoomDisappearanceTimer { get; set; } = 60;


        /// <summary>
        /// Draws the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap that is going to be drawn.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        public void Draw(CanvasBitmap bitmap, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < GetHitXpos.Count; i++)
            {
                if (BombTimer() > 0)
                    DrawBomb(bitmap, args, i);
                else
                    ResetBombTimer(i);
            }
        }
        /// <summary>
        /// Resets the bomb timer.
        /// </summary>
        /// <param name="i">The i index </param>
        private void ResetBombTimer(int i)
        {
            RemoveBomb(i);
            BoomDisappearanceTimer = 60;
        }
        /// <summary>
        /// Removes the bomb.
        /// </summary>
        /// <param name="i">The i index</param>
        private void RemoveBomb(int i)
        {
            GetHitXpos.RemoveAt(i);
            GetHitYpos.RemoveAt(i);
        }
        /// <summary>
        /// gets the bomb disappearance timer
        /// </summary>
        /// <returns>
        /// an int value that returns the value of  bomb disappearance timer
        /// </returns>
        private int BombTimer()
        {
            return BoomDisappearanceTimer;
        }
        /// <summary>
        /// Draws the bomb.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="args">The <see cref="CanvasDrawEventArgs"/> instance containing the event data.</param>
        /// <param name="i">takes in a index of which index to draw</param>
        private void DrawBomb(CanvasBitmap bitmap, CanvasDrawEventArgs args, int i)
        {
            args.DrawingSession.DrawImage(bitmap, GetHitXpos[i] - 35, GetHitYpos[i] - 83);
            DecreaseBombTimer();
        }
        /// <summary>
        /// Decreases the bomb timer.
        /// </summary>
        private void DecreaseBombTimer()
        {
            BoomDisappearanceTimer -= 1;
        }
    }
}
