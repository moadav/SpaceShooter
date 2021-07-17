using System;

namespace SpaceShooter.MyModel
{
    /// <summary>
    /// A class with generics to help classes with to use methods easier
    /// </summary>
    public class EnemyHelper
    {
        /// <summary>
        /// Takes in a T of type IConvertible to return the desired damage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="damage">The amount of damage.</param>
        /// <returns>
        /// a type of IConvertible T
        /// </returns>
        /// <example>
        /// <code>
        /// EnemyHelper helper = new EnemyHelper
        /// var damage = helper.Damage<float>(0.2f):
        /// </code>
        /// </example>
        
        public T Damage<T>(T damage) where T : IConvertible
        {
            return damage;
        }

        /// <summary>
        /// Takes in a T of type IConvertible to return the value that is the direction of the player
        /// </summary>
        /// <typeparam name="T1"> the type of IConvertible.</typeparam>
        /// <param name="value">The value to the player.</param>
        /// <returns>
        /// a type of IConvertible T1
        /// </returns>
        /// /// <example>
        /// <code>
        /// EnemyHelper helper = new EnemyHelper
        /// var playerspot = 21f;
        /// var damage = helper.ShootAtTarget<float>(playerspot):
        /// </code>
        /// </example>
        public T1 ShootAtTarget<T1>(T1 value) where T1 : IConvertible
        {
            return value;
        }


    }
}
