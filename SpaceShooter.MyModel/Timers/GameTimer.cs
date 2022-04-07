using System;
using Windows.UI.Xaml;

namespace SpaceShooter.MyModel
{
    /// <summary>
    ///  The GameTimer class uses DispatchTimers to run methods, that starts the game functionality 
    /// </summary>
 
    public class GameTimer
    {
        readonly Hindrance throwable = new Hindrance();
        readonly EliteEnemy eliteEnemy = new EliteEnemy();
        readonly SpecialEnemy aspecialEnemy = new SpecialEnemy();
        readonly Enemy enemy = new Enemy();

        /// <summary>
        ///  a dispatchtimer that calls method that spawns hindrances to the screen <see cref="Hindrance.SpawnAsteroid(object, object)"/>
        /// </summary>
        private static readonly DispatcherTimer throwableTimer = new DispatcherTimer();
        /// <summary>
        ///  a dispatchtimer that calls method that spawns enemies to the screen <see cref="Enemy.SpawnEnemies(object, object)"/>
        /// </summary>
        private static readonly DispatcherTimer enemyTimer = new DispatcherTimer();
        /// <summary>
        ///  a dispatchtimer that calls method that spawns special enemies to the screen <see cref="SpecialEnemy.SpawnEnemies(object, object)"/>
        /// </summary>
        private static readonly DispatcherTimer specialEnemyTimer = new DispatcherTimer();
        /// <summary>
        /// a dispatchtimer that calls method that spawns elite enemies to the screen <see cref="EliteEnemy.SpawnEnemies(object, object)"/>
        /// </summary>
        private static readonly DispatcherTimer eliteEnemyTimer = new DispatcherTimer();

        private readonly Random spawnTimer = new Random();

        /// <summary> 
        ///  Instansiates the DispatchTimers and starts the timers. that draws bitmaps to screen, <see cref="DrawLogic"/>
        /// </summary>
        public void InstansiateAndStartTimers()
        {
            if (!Levels.RoundOver())
            {
                StartHindranceTimer();
                StartEnemyTimer();
                StartSpecialEnemyTimer();
                StartEliteEnemyTimer();
            }
        }
        /// <summary>
        /// Starts the dispatchtimer that references the hindrance timer.
        /// </summary>
        private void StartHindranceTimer()
        {
            throwableTimer.Tick += throwable.SpawnAsteroid;
            throwableTimer.Interval = new TimeSpan(0, 0, 0, 0, spawnTimer.Next(1000, 5000));
            throwableTimer.Start();
        }
        /// <summary>
        /// Starts the dispatchtimer that references the enemy timer.
        /// </summary>
        private void StartEnemyTimer()
        {
            enemyTimer.Tick += enemy.SpawnEnemies;
            enemyTimer.Interval = new TimeSpan(0, 0, 0, 0, spawnTimer.Next(1000, 2000));
            enemyTimer.Start();
        }
        /// <summary>
        /// Starts the dispatchtimer that references the special enemy timer.
        /// </summary>
        private void StartSpecialEnemyTimer()
        {
            specialEnemyTimer.Tick += aspecialEnemy.SpawnEnemies;
            specialEnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, spawnTimer.Next(1000, 3000));
            specialEnemyTimer.Start();
        }
        /// <summary>
        /// Starts the dispatchtimer that references the elite enemy timer.
        /// </summary>
        private void StartEliteEnemyTimer()
        {
            eliteEnemyTimer.Tick += eliteEnemy.SpawnEnemies;
            eliteEnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, spawnTimer.Next(1000, 4000));
            eliteEnemyTimer.Start();
        }

        /// <summary> 
        /// stops the timers.
        /// </summary>
        public static void StopTimers()
        {
            throwableTimer.Stop();
            enemyTimer.Stop();
            specialEnemyTimer.Stop();
            eliteEnemyTimer.Stop();
        }

        /// <summary> 
        ///  starts the timers
        /// </summary>
        public static void StartTimers()
        {
            throwableTimer.Start();
            enemyTimer.Start();
            specialEnemyTimer.Start();
            eliteEnemyTimer.Start();
        }
    }
}
