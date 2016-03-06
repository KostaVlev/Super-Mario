namespace SuperMario
{
    using System;

    /// <summary>
    /// Creates the game field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when map's width or height is less then the minimal width and height.
    /// <see cref="MIN_WIDTH"/> and <see cref="MIN_HEIGHT"/>
    /// </exception>
    public class Map
    {
        private const int MIN_WIDTH = 22;
        private const int MIN_HEIGHT = 50;

        private int width;
        private int height;

        public Map(int width = MIN_WIDTH, int height = MIN_HEIGHT)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            private set
            {
                if (value < MIN_WIDTH)
                {
                    throw new ArgumentOutOfRangeException("width", "Map width is to small");
                }

                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }

            private set
            {
                if (value < MIN_HEIGHT)
                {
                    throw new ArgumentOutOfRangeException("height", "Map height is to small");
                }

                this.height = value;
            }
        }

    }
}
