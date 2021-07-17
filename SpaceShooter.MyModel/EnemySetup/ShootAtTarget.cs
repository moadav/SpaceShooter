namespace SpaceShooter.MyModel
{
    /// <summary>
    /// a interface class to help enemy to shoot at the player
    /// </summary>
    public interface ShootAtTarget
    {
        /// <summary>
        /// Shoots at player.
        /// </summary>
        /// <param name="i"> the value of y bullet position distance the bullet should go to reach the player</param>
        void ShootAtPlayer(int i);
        /// <summary>
        /// Shoots directly at the players position
        /// </summary>
        /// <param name="i"> the value of y bullet position distance the bullet should go to reach the player</param>
        void AddShootingDirection(int i);

        /// <summary>
        /// Removes the shooting direction.
        /// </summary>
        /// <param name="i">Takes in a int i"/></param>
        void RemoveShootingDirection(int i);


    }
}
