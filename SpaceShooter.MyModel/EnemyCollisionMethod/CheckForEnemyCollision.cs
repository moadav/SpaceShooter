using System.Collections.Generic;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that helps with collision handling for enemies
    /// </summary>
    public class CheckForEnemyCollision
    {
        /// <summary>
        /// Checks if there is any collision
        /// </summary>
        /// <param name="xPosisjon">a list of x positions </param>
        /// <param name="yPosisjon">a list of y positions.</param>
        /// <param name="index">the specified element of the list.</param>
        /// <returns></returns>
        public bool Collision(List<float> xPosisjon, List<float> yPosisjon, int index)
        {
            if (xPosisjon[index] >= ReturnPlayerXposRightBorder() & xPosisjon[index] <= ReturnPlayerXposLeftBorder() & yPosisjon[index] >= ReturnPlayerYposUpBorder() & yPosisjon[index] <= ReturnPlayerYposDownBorder())
                return true;                      
            return false;
        }
        /// <summary>
        /// Returns the player xpos right border.
        /// </summary>
        /// <returns>
        /// an int value that holds the right border
        /// </returns>
        private int ReturnPlayerXposRightBorder()
        {
            return Player.defaultPlayerCoords.StartPosisjonX - 360;
        }
        /// <summary>
        /// Returns the player xpos left border.
        /// </summary>
        /// <returns>
        /// an int value that is the left border of the player
        /// </returns>
        private int ReturnPlayerXposLeftBorder()
        {
            return Player.defaultPlayerCoords.StartPosisjonX - 140;
        }

        /// <summary>
        /// Returns the player ypos up border.
        /// </summary>
        /// <returns>
        /// an int value that is the up border of the player
        /// </returns>
        private int ReturnPlayerYposUpBorder()
        {
            return Player.defaultPlayerCoords.StartPosisjonY - 310;
        }

        /// <summary>
        /// Returns the player ypos down border.
        /// </summary>
        /// <returns>
        /// an int value that is the down border of the player
        /// </returns>
        private int ReturnPlayerYposDownBorder()
        {
            return Player.defaultPlayerCoords.StartPosisjonY - 200;
        }
    }
}
