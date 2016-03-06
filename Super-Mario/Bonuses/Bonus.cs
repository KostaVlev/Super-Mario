namespace SuperMario.Bonuses
{
    using SuperMario.Interfaces;

    /// <summary>
    /// Base class that is defining basic properties for all game bonuses.
    /// </summary>
    public abstract class Bonus : IBonus
    {
        private int positionX;
        private int positionY;
        private int bonusPoints;
        private char displayAsChar;
        private bool isVisible;

        protected Bonus(int bonusPoints, char displayAsChar, bool isVisible = true)
        {
            this.BonusPoints = bonusPoints;
            this.DisplayAsChar = displayAsChar;
            this.IsVisible = isVisible;
        }

        public int PositionX
        {
            get
            {
                return this.positionX;
            }

            set
            {
                this.positionX = value;
            }
        }

        public int PositionY
        {
            get
            {
                return this.positionY;
            }

            set
            {
                this.positionY = value;
            }
        }

        public int BonusPoints
        {
            get
            {
                return this.bonusPoints;
            }

            set
            {
                this.bonusPoints = value;
            }
        }

        public char DisplayAsChar
        {
            get
            {
                return this.displayAsChar;
            }

            set
            {
                this.displayAsChar = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                this.isVisible = value;
            }
        }
    }
}
