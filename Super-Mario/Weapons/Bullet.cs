namespace SuperMario.Weapons
{
    using System;
    using SuperMario.Interfaces;

    /// <summary>
    /// Basic ammonition that player can shoot to kill enemies with
    /// default demage value of <see cref="DEFAULT_DAMAGE"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when negative number damage is set.
    /// </exception>
    public class Bullet : IAmmo
    {
        public const char DEFAULT_CHAR = '-';
        private const int DEFAULT_DAMAGE = 10;

        private int positionX;
        private int positionY;
        private int damage;
        private char displayAsChar;
        private bool isShooted;

        public Bullet(char displayAsChar = DEFAULT_CHAR, int damage = DEFAULT_DAMAGE)
        {
            this.Damage = damage;
            this.DisplayAsChar = displayAsChar;
            this.isShooted = false;
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

        public int Damage
        {
            get
            {
                return this.damage;
            }

            set
            {
                if (damage < 0)
                {
                    throw new ArgumentOutOfRangeException("damage", "Damage can not be negative number");
                }

                this.damage = value;
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

        public bool IsShooted
        {
            get
            {
                return this.isShooted;
            }

            set
            {
                this.isShooted = value;
            }
        }
    }
}
