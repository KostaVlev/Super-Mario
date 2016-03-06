namespace SuperMario.Bonuses.Coins
{
    /// <summary>
    /// Gold coin bonus that the player will gather.
    /// </summary>
    public class GoldCoin : Bonus
    {
        public const char DEFAULT_CHAR = '$';
        private const int DEFAULT_BONUS_POINTS = 200;

        public GoldCoin(int bonusPoints = DEFAULT_BONUS_POINTS, char displayAsChar = DEFAULT_CHAR)
            : base(bonusPoints, displayAsChar)
        {
        }
    }
}
