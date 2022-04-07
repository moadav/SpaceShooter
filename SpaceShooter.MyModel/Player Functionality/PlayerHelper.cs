using System;
using System.Collections.Generic;


namespace SpaceShooter.MyModel
{
    public class PlayerHelper
    {
        /// <summary>
        /// Handles the player collisions against the enemies
        /// </summary>
        /// <param name="costs">The total score u get</param>
        /// <param name="bulletX"> a list containing The bulletx positions</param>
        /// <param name="bulletY"> a list containing The bullety positions</param>
        /// <param name="positionX">The positionx of the enemy.</param>
        /// <param name="positionY">The positiony of the enemy</param>
        /// <param name="index">The index in which enemy to check for collision.</param>
        /// <param name="xRightCorner"> Collision border for the x right corner.</param>
        /// <param name="xLeftCorner">Collision border for the x left corner.</param>
        /// <param name="yRightCorner">Collision border for The y right corner.</param>
        /// <param name="yLeftCorner">Collision border for the y left corner.</param>
        /// <returns></returns>
        public bool CollisionHandler(int costs, List<float> bulletX, List<float> bulletY, float positionX, float positionY, int index, int xRightCorner, int xLeftCorner, int yRightCorner, int yLeftCorner)
        {
            if (bulletX[index] >= positionX + xRightCorner & bulletX[index] <= positionX + xLeftCorner & bulletY[index] >= positionY + yRightCorner & bulletY[index] <= positionY + yLeftCorner)
            {
                AddHitMarkers(positionX + 90, positionY + 80);
                IncreaseScore(costs);
                DecreaseTotalEnemies();
                return true;
            }
            return false;
        }

        private readonly Action DecreaseTotalEnemies = () =>
         {
             Levels._roundfixer.TotalEnemies--;
         };


        private readonly Action<float, float> AddHitMarkers = (xPos, yPos) =>
          {
              Hit.GetHitXpos.Add(xPos);
              Hit.GetHitYpos.Add(yPos);
          };

  
        private readonly Action<int> IncreaseScore = costs =>
         {
             GameHud.Score += costs;
         };

    }
}
