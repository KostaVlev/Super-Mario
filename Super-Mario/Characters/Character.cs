namespace SuperMario.Characters
{
    using System;

    /// <summary>
    /// Base class that is defining basic properties for all game characters,
    /// players, enemies, etc.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when negative life points are set
    /// </exception>
    public abstract class Character
    { 
        private int positionX;
        private int positionY;
        private int lifePoints;
        private char displayAsChar;
        private bool isLife;        

        protected Character(int staringPositionX, int startingPositionY, char displayAsChar, int lifePoints)
        {
            this.PositionX = staringPositionX;
            this.PositionY = startingPositionY;
            this.LifePoints = lifePoints;
            this.DisplayAsChar = displayAsChar;
            this.IsLife = true;
        }

        protected Character(char displayAsChar, int lifePoints)
        {
            this.DisplayAsChar = displayAsChar;
            this.LifePoints = lifePoints;
            this.IsLife = true;
        }

        protected Character(int lifePoints)
        {
            this.LifePoints = lifePoints;
            this.IsLife = true;
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

        public int LifePoints
        {
            get
            {
                return this.lifePoints;
            }

            set
            {
                this.lifePoints = value;
            }
        }

        public bool IsLife
        {
            get
            {
                return this.isLife;
            }

            set
            {
                this.isLife = value;
            }
        }
    }
}
