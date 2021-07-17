using System;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// static class that returns random y position
    /// </summary>
    public static class ReturnRandomYPosition
    {
        /// <summary>
        /// an instance of Random <see cref="Random"/>
        /// </summary>
        /// <value>
        /// returns a Random object <see cref="Random"/>
        /// </value>
        private static Random RandomYLocation { get; } = new Random();

        /// <summary>
        /// A lamgda statement that Returns a random y int value.
        /// </summary>
        /// <returns>
        /// an int value between 0 and 700
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// int yvalue = ReturnRandomY();
        /// </code>
        /// </example>
        public static int ReturnRandomY()
        {
            return RandomYLocation.Next(0, 700);
        }
  
    }
}
