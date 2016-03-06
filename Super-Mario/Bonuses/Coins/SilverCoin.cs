namespace SuperMario.Bonuses.Coins
{
    /// <summary>
    /// Silver coin bonus that the player will gather.
    /// </summary>
    public class SilverCoin : Bonus
    {
        public const char DEFAULT_CHAR = 'S';
        private const int DEFAULT_BONUS_POINTS = 100;

        public SilverCoin(int bonusPoints = DEFAULT_BONUS_POINTS, char displayAsChar = DEFAULT_CHAR)
            : base(bonusPoints, displayAsChar)
        {
        }
    }
}
