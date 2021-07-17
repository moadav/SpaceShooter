using System;
namespace SpaceShooter.MyModel
{
    /// <summary>
    /// Class that handles user-defined exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <exception cref="CollisionsDetectedException">Thrown when collision method is not working <see cref="Player.Collision(int)"/> 
    [Serializable]
    
    public class CollisionsDetectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionsDetectedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CollisionsDetectedException(string message)
             : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionsDetectedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public CollisionsDetectedException(string message, Exception inner)
        : base(message, inner) { }

    }
}
