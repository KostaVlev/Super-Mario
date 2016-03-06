namespace SuperMario.Characters.Players
{
    using System;
    using SuperMario.Interfaces;

    /// <summary>
    /// Implements main player's character.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Thrown when null parameter "ammo" is passed in the Shoot method.
    /// </exception>
    public class Mario : Character, IAttack, IJump, IMovable
    {
        private const char DEFAULT_CHAR = 'M';
        private const int DEFAULT_LIFE_POINTS = 10;
        private const int JUMP_HEIGHT = 1;
        private const int STEP = 1;

        private int points;

        public Mario(int staringPositionX, int startingPositionY, char displayAsChar = DEFAULT_CHAR, int lifePoints = DEFAULT_LIFE_POINTS)
            : base(staringPositionX, startingPositionY, displayAsChar, lifePoints)
        {
        }

        public int Points
        {
            get
            {
                return this.points;
            }

            set
            {
                this.points = value;
            }
        }

        public IAmmo Shoot(IAmmo ammo)
        {
            if (ammo == null)
            {
                throw new ArgumentNullException("ammo", "Ammo can not be null");
            }

            ammo.PositionX = this.PositionX;
            ammo.PositionY = this.PositionY;
            ammo.IsShooted = true;

            return ammo;
        }

        public void StartJump()
        {
            this.PositionY -= JUMP_HEIGHT;
        }

        public void Falling()
        {
            this.PositionY += STEP;
        }

        public void MoveLeft()
        {
            this.PositionX -= STEP;
        }

        public void MoveRight()
        {
            this.PositionX += STEP;
        }

        public void AddPoints(IBonus bonus)
        {
            this.points += bonus.BonusPoints;
        }
    }
}
