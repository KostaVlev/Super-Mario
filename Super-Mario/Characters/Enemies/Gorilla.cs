namespace SuperMario.Characters.Enemies
{
    using SuperMario.Interfaces;

    /// <summary>
    /// Implemets enemy character that the player has to fight with.
    /// </summary>
    public class Gorilla : Character, IMovable
    {        
        private const int DEFAULT_LIFE_POINTS = 10;
        private const int STEP = 1;

        public readonly char DEFAULT_CHAR = 'G';

        public Gorilla(char displayAsChar, int lifePoints = DEFAULT_LIFE_POINTS)
            : base(displayAsChar, lifePoints)
        {
        }

        public Gorilla(int lifePoints = DEFAULT_LIFE_POINTS)
            : base(lifePoints)
        {
            this.DisplayAsChar = DEFAULT_CHAR;
        }

        public void MoveLeft()
        {
            this.PositionX -= STEP;
        }

        public void MoveRight()
        {
            this.PositionX += STEP;
        }
    }
}
