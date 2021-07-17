
namespace SpaceShooter.MyModel
{
    public interface MoveEnemyUpAndDown
    {
        /// <summary>
        /// Changes the direction to up.
        /// </summary>
        /// <param name="index">Specifies which enemy to move up </param>
        void ChangeDirectionToUp(int index);
        /// <summary>
        /// Changes the direction to down.
        /// </summary>
        /// <param name="index">Specifies which enemy to move down</param>
        void ChangeDirectionToDown(int index);
        /// <summary>
        /// Moves the enemy down.
        /// </summary>
        /// <param name="index">Specifies which enemy to move down</param>
        void MoveEnemyDown(int index);
        /// <summary>
        /// Moves the enemy up.
        /// </summary>
        /// <param name="index">Specifies which enemy to move up</param>
        void MoveEnemyUp(int index);
        /// <summary>
        /// If the enemy hit corner.
        /// </summary>
        /// <param name="index">Specifies if enemy hit corner .</param>
        void IfCornerHit(int index);
        /// <summary>
        /// Moves the enemy up or down.
        /// </summary>
        /// <param name="index">Specifies the enemy to move up or down<</param>
        void MoveEnemyUpAndDown(int index);
        /// <summary>
        /// Ifs the enemy movement destination is reached.
        /// </summary>
        /// <param name="index">Checks if the specified enemy has reached his destination</param>
        void IfEnemyMovementDestinationIsReached(int index);
        /// <summary>
        /// Adds the enemy's directions.
        /// </summary>
        /// <param name="startingYPosition">The starting y position.</param>
        void AddEnemyDirections(int startingYPosition);
    }
}
