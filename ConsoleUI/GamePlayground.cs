namespace ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using SuperMario;
    using SuperMario.Bonuses;
    using SuperMario.Bonuses.Coins;
    using SuperMario.Characters;
    using SuperMario.Characters.Enemies;
    using SuperMario.Characters.Players;
    
    /// <summary>
    /// Draws game's playground with all needed elements. With default parameters.
    /// </summary>
    class GamePlayground
    {
        private const int NUMBER_OF_ENEMIES = 4;
        private const int NUMBER_OF_GOLD_COINS = 2;
        private const int NUMBER_OF_SILVER_COINS = 6;
        private const int NUMBER_OF_FLOORS = 4;
        private const int JUMP_MAX_HEIGHT = 2;
        private const int PLAYER_SATRT_POSITION_X = 2;
        private const int PLAYER_SATRT_POSITION_Y = NUMBER_OF_FLOORS;

        public readonly char BRICK = '*';
        public readonly char EXIT = 'E';

        private Character player;
        private IList<Character> enemies;
        private IList<Bonus> bonuses;
        private Map map;

        public GamePlayground()
        {
            this.Player = new Mario(PLAYER_SATRT_POSITION_X, PLAYER_SATRT_POSITION_Y);
            this.Bonuses = GenerateBonuses();
            this.Enemies = GenerateEnemies();
            this.Map = new Map();
            this.Playground = new char[map.Width, map.Height];
            this.DrawPlayground();
        }

        public Character Player
        {
            get
            {
                return this.player;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("player", "Player can not be null");
                }

                this.player = value;
            }
        }

        public IList<Character> Enemies
        {
            get
            {
                return this.enemies;
            }

            private set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentNullException("enemies", "Enemies can not be null or empty");
                }

                this.enemies = value;
            }
        }

        public IList<Bonus> Bonuses
        {
            get
            {
                return this.bonuses;
            }

            private set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentNullException("bonuses", "Bonuses can not be null or empty");
                }

                this.bonuses = value;
            }
        }

        public Map Map
        {
            get
            {
                return this.map;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("map", "Map can not be null");
                }

                this.map = value;
            }
        }

        public char[,] Playground { get; set; }

        private void DrawPlayground()
        {
            //Draw playgroud borders and aisle;
            DrawGround(this.Playground);

            //Draw player
            this.Playground[Player.PositionY, Player.PositionX] = Player.DisplayAsChar;

            //Draw bonuses
            DrawBonuses(this.Playground);

            //Draw enemies
            DrawEnemies(this.Playground);

            //Draw all the compoments on the Console
            DisplayPlayground(this.Playground);
        }

        private IList<Bonus> GenerateBonuses()
        {
            this.bonuses = new List<Bonus>(NUMBER_OF_SILVER_COINS + NUMBER_OF_GOLD_COINS);

            for (int i = 0; i < NUMBER_OF_SILVER_COINS; i++)
            {
                this.Bonuses.Add(new SilverCoin());
            }

            for (int i = 0; i < NUMBER_OF_GOLD_COINS; i++)
            {
                this.Bonuses.Add(new GoldCoin());
            }

            return this.bonuses;
        }

        private IList<Character> GenerateEnemies()
        {
            this.enemies = new List<Character>(NUMBER_OF_ENEMIES);

            for (int i = 0; i < NUMBER_OF_ENEMIES; i++)
            {
                this.enemies.Add(new Gorilla());
            }

            return this.enemies;
        }

        private void DrawGround(char[,] playground)
        {
            int rows = playground.GetLength(0);
            int cols = playground.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    //Draw Playground borders.                    
                    playground[0, col] = BRICK;
                    playground[1, col] = BRICK;
                    playground[row, 0] = BRICK;
                    playground[row, 1] = BRICK;
                    playground[row, cols - 2] = BRICK;
                    playground[row, cols - 1] = BRICK;
                    playground[rows - 2, col] = BRICK;
                    playground[rows - 1, col] = BRICK;
                    playground[rows - 3, 2] = EXIT;

                    //Draw Playground aisle.                    
                    if ((row != 0 && row != rows - 1) && (row % (NUMBER_OF_FLOORS + 1)) == 0)
                    {
                        playground[row, col] = BRICK;

                        //Make gaps in the aisles.
                        if (row % 2 == 0)
                        {
                            playground[row, 2] = ' ';
                            playground[row, 3] = ' ';
                        }
                        else
                        {
                            playground[row, cols - 3] = ' ';
                            playground[row, cols - 4] = ' ';
                        }                        
                    }                    
                }
            }
        }

        private void DrawBonuses(char[,] playground)
        {
            int rows = playground.GetLength(0);
            int cols = playground.GetLength(1);
            int numberOfBonusesAtEachFloor = this.Bonuses.Count / NUMBER_OF_FLOORS;
            int columnOffset = 4;
            int bonusDrawn = 0;
            Random rnd = new Random();

            while (bonusDrawn < this.Bonuses.Count)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (row % (NUMBER_OF_FLOORS + 1) == 0 && row != 0)
                    {
                        for (int i = 0; i < numberOfBonusesAtEachFloor; i++)
                        {
                            //Draw bonus at max player jump height
                            int currentRow = row - JUMP_MAX_HEIGHT;
                            int currentColumn = rnd.Next(columnOffset, cols - columnOffset);
                            playground[currentRow, currentColumn] = this.Bonuses[bonusDrawn].DisplayAsChar;
                            this.Bonuses[bonusDrawn].PositionX = currentColumn;
                            this.Bonuses[bonusDrawn].PositionY = currentRow;
                            bonusDrawn++;
                        }
                    }
                }
            }
        }

        private void DrawEnemies(char[,] playground)
        {
            int rows = playground.GetLength(0);
            int cols = playground.GetLength(1);
            int numberOfEnemiesAtEachFloor = this.Enemies.Count / NUMBER_OF_ENEMIES;
            int columnOffsetRight = 4;
            int columnOffsetLeft = 6;
            int enemiesDrawn = 0;
            Random rnd = new Random();

            while (enemiesDrawn < NUMBER_OF_ENEMIES)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (row % (NUMBER_OF_FLOORS + 1) == 0 && row != 0)
                    {
                        for (int i = 0; i < numberOfEnemiesAtEachFloor; i++)
                        {
                            int currentColumn = rnd.Next(columnOffsetLeft, cols - columnOffsetRight);
                            playground[row - 1, currentColumn] = this.Enemies[enemiesDrawn].DisplayAsChar;
                            this.Enemies[enemiesDrawn].PositionX = currentColumn;
                            this.Enemies[enemiesDrawn].PositionY = row - 1;
                            enemiesDrawn++;
                        }
                    }
                }
            }

        }

        private void DisplayPlayground(char[,] playground)
        {
            int rows = playground.GetLength(0);
            int cols = playground.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(playground[row, col]);
                }

                Console.WriteLine();
            }

            Console.CursorVisible = false;
        }
    }
}
