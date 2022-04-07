

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// This class handles the Player's movements x and y positions 
    /// </summary>
    public struct PlayerCoords
    {
        /// <summary>
        /// Gets or sets the start posisjon x. 
        /// </summary>
        /// <returns>
        /// The start posisjon x.
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// StartPosisjonX = 324;
        /// </code>
        /// </example>
        public int StartPosisjonX { get; set; }

        /// <summary>
        /// Gets or sets the start posisjon y.
        /// </summary>
        /// <returns>
        /// The start posisjon y.
        /// </returns>
        /// 
        /// <example>
        /// <code>
        /// StartPosisjonY = 524;
        /// </code>
        /// </example>
        public int StartPosisjonY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCoords"/> struct.
        /// </summary>
        /// <param name="startPosisjonX"> Takes in the start position of x for the player <see cref="StartPosisjonX"/>.</param>
        /// <param name="startPosisjonY"> Takes in the start position of y for the player. <see cref="StartPosisjonY"/></param>
        /// <example>
        /// <code>
        /// PlayerCoords = new PlayerCoords(321,123)
        /// </code>
        /// </example>
        public PlayerCoords(int startPosisjonX, int startPosisjonY)
        {
            this.StartPosisjonX = startPosisjonX;
            this.StartPosisjonY = startPosisjonY;
        }


    }
}
