
namespace SpaceShooter.MyModel
{

    /// <summary>
    /// Handles shop item functionality
    /// </summary>
    /// <seealso cref="SpaceShooter.MyModel.Item" />
    public class ShopItem : Item
    {
        private static readonly int HealAmount = 2;
        private static int ArmourPackBuylimit { get; set; } = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        public ShopItem(string name, int price)
            : base(name, price)
        { }
        /// <summary>
        /// Heals half the health of the player.
        /// </summary>
        /// <returns>
        /// boolean value, true if the player has enough score and false if not
        /// </returns>
        public static bool HealHalfHealth()
        {
            if (ReturnPlayerCurrency() >= 5)
            {
                HealOnlyHalf();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns the players currency.
        /// </summary>
        /// <returns>
        /// a value of how much the player has in cash
        /// </returns>
        private static int ReturnPlayerCurrency()
        {
            return GameHud.Score;
        }
        /// <summary>
        /// method that will heal half the players health
        /// </summary>
        private static void HealOnlyHalf()
        {
            ReturnNewHealth(ReturnHalfHealth());
        }
        /// <summary>
        /// Returns half health of the player
        /// </summary>
        /// <returns>
        /// a value of how much health player has left if the player heals half health
        /// </returns>
        private static double ReturnHalfHealth()
        {
            return GameHud.HealthBar.Offset - HealAmount;
        }
        /// <summary>
        /// Returns the new health.
        /// </summary>
        /// <param name="currentHealth">The healing amount current health.</param>
        private static void ReturnNewHealth(double currentHealth)
        {
            while (true)
                if (currentHealth >= 0)
                {
                    SetHalfHealth(currentHealth);
                    break;
                }
                else
                    currentHealth += 0.01;
        }
        /// <summary>
        /// heals the player
        /// </summary>
        /// <param name="halfHealth">The half health.</param>
        /// <returns>
        /// returns the players new health
        /// </returns>
        private static double SetHalfHealth(double halfHealth)
        {
            return GameHud.HealthBar.Offset = halfHealth;
        }
        /// <summary>
        /// Buys a armour pack.
        /// </summary>
        /// <returns>
        /// boolean value, true if the player has enough score and false if not
        /// </returns>
        public static bool BuyArmour()
        {
            if (ReturnPlayerCurrency() >= 50 && ReturnArmourPackBuyLimit() != 0)
            {
                ArmourUpdate();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns the armour pack buy limit.
        /// </summary>
        /// <returns>
        /// value indicating how much packs left the player has
        /// </returns>
        private static int ReturnArmourPackBuyLimit()
        {
            return ArmourPackBuylimit;
        }
        /// <summary>
        /// Runs a method which will reduce the enemies damage
        /// </summary>
        private static void ArmourUpdate()
        {
            DecreaseEnemyDamage(0.01f);
            DecreaseArmourPackBuyLimit();
        }
        /// <summary>
        /// Decreases the armour pack buy limit.
        /// </summary>
        private static void DecreaseArmourPackBuyLimit()
        {
            ArmourPackBuylimit -= 1;
        }
        /// <summary>
        /// Decreases the enemy damage.
        /// </summary>
        /// <param name="decreaseAmount">The decrease amount.</param>
        private static void DecreaseEnemyDamage(float decreaseAmount)
        {
            SpecialEnemy.SpecialEnemyDamage -= decreaseAmount;
            Enemy.EnemyDamage -= decreaseAmount;
            EliteEnemy.EliteEnemyDamage -= decreaseAmount;
            Hindrance.HindranceDamage -= decreaseAmount;
        }



    }
}
