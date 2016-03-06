namespace ConsoleUI
{
    using System;
    using System.Threading;
    using SuperMario;
    using SuperMario.Characters;
    using SuperMario.Characters.Players;
    using SuperMario.Weapons;
    using SuperMario.Interfaces;
    
    class GameEngine
    {
        private ConsoleKeyInfo keyInfo;
        private GamePlayground playground;
        private Game game;      

        public GameEngine(GamePlayground playground, Game game)
        {
            this.Playground = playground;
            this.Game = game;
            this.Game.IsRunning = true;
            this.IsShootingRight = true;

            //First two and last two columns of map are saved for borders.
            this.RightBorder = playground.Map.Width - 3;
            this.LeftBorder = 2;       
        }

        public GamePlayground Playground
        {
            get
            {
                return this.playground;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("playground", "Playground can not be null");
                }

                this.playground = value;
            }
        }

        public Game Game
        {
            get
            {
                return this.game;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("game", "Game can not be null");
                }

                this.game = value;
            }
        }

        public bool IsShootingRight { get; private set; }

        public int RightBorder { get; private set; }

        public int LeftBorder { get; private set; }

        public void CheckIfKeyIsPressed()
        {
            this.keyInfo = Console.ReadKey(true);

            switch (this.keyInfo.Key)
            {
                case ConsoleKey.Escape: ExitGame();                   
                    break;
                case ConsoleKey.Spacebar: Shoot(this.Playground.Player);
                    break;
                case ConsoleKey.LeftArrow:
                    if (!CheckIfCharacterIsAtTheLeftBorder(this.Playground.Player))
                    {
                        MovePlayerLeft(this.Playground.Player);
                        MakePlayerFalling(this.Playground.Player);
                        this.IsShootingRight = false;
                    };

                    if (ChechIfPlayerIsAtTheExit(this.Playground.Player))
                    {
                        GameWin(this.Playground.Player);
                    }

                    if (CheckIfEnemyHitsPlyer(this.Playground.Player))
                    {
                        GameOver(this.Playground.Player);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    Jump(this.Playground.Player);
                    break;
                case ConsoleKey.RightArrow:
                    if (!CheckIfCharacterIsAtTheRightBorder(this.Playground.Player))
                    {
                        MovePlayerRight(this.Playground.Player);
                        MakePlayerFalling(this.Playground.Player);
                        this.IsShootingRight = true;
                    };

                    if (ChechIfPlayerIsAtTheExit(this.Playground.Player))
                    {
                        GameWin(this.Playground.Player);
                    }

                    if (CheckIfEnemyHitsPlyer(this.Playground.Player))
                    {
                        GameOver(this.Playground.Player);
                    }            
                    break;
                case ConsoleKey.F1:
                    Console.Clear();
                    this.Game.IsRunning = false;
                    break;                    
                default:
                    Console.SetCursorPosition(0, this.Playground.Map.Height + 2);
                    Console.WriteLine("Game controls:");
                    Console.WriteLine("Esc = EXIT, Spacebar = SHOOT, LeftArrow = GO LEFT, RightArrow = GO RIGHT, UpArrow = JUMP, F1 = New Game");
                    break;
            }
        }

        private void MovePlayerRight(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);

            ClearAtPosition(mario.PositionX, mario.PositionY);
            mario.MoveRight();
            WriteAtPosition(mario.PositionX, mario.PositionY, mario.DisplayAsChar);
        }

        private void MovePlayerLeft(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);

            ClearAtPosition(mario.PositionX, mario.PositionY);
            mario.MoveLeft();
            WriteAtPosition(mario.PositionX, mario.PositionY, mario.DisplayAsChar);
        }

        private void Jump(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);

            //Player is jumping straight up.
            ClearAtPosition(mario.PositionX, mario.PositionY);
            mario.StartJump();
            WriteAtPosition(mario.PositionX, mario.PositionY, mario.DisplayAsChar);
            TakeBonus(player);

            //Player is flaying for a moment.
            Thread.Sleep(200);

            //Player is landing on the ground.
            ClearAtPosition(mario.PositionX, mario.PositionY);
            mario.Falling();
            WriteAtPosition(mario.PositionX, mario.PositionY, mario.DisplayAsChar);
        }

        private void FallingDown(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);

            //Player is falling down.
            ClearAtPosition(mario.PositionX, mario.PositionY);
            mario.Falling();
            WriteAtPosition(mario.PositionX, mario.PositionY, mario.DisplayAsChar);

            //Make the falling slow.
            Thread.Sleep(100);
        }

        private void ClearAtPosition(int x, int y)
        {            
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        private void WriteAtPosition(int x, int y, char charToWrite)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(charToWrite);
        }

        private void ExitGame()
        {
            Console.Clear();
            Console.WriteLine("GOOD BYE!");
            Thread.Sleep(1500);
            this.Game.IsRunning = false;
            this.Game.IsFinished = true;
        }

        private void Shoot(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);
            Bullet bullet = mario.Shoot(new Bullet()) as Bullet;

            //Run the shoot in different thread so the player can move while the bullet is flying.
            if (this.IsShootingRight)
            {
                bullet.PositionX = player.PositionX + 1;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    while (bullet.IsShooted)
                    {

                        WriteAtPosition(bullet.PositionX, bullet.PositionY, bullet.DisplayAsChar);
                        Thread.Sleep(200);
                        ClearAtPosition(bullet.PositionX, bullet.PositionY);
                        WriteAtPosition(++bullet.PositionX, bullet.PositionY, bullet.DisplayAsChar);
                        TakeDownEnemy(bullet);

                        if (bullet.PositionX == this.RightBorder || IsEnemyTakenDown())
                        {
                            bullet.IsShooted = false;
                            ClearAtPosition(bullet.PositionX, bullet.PositionY);
                            Thread.CurrentThread.Abort();
                        }
                    }

                }).Start();
            }
            else
            {
                bullet.PositionX = player.PositionX - 1;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    while (bullet.IsShooted)
                    {
                        WriteAtPosition(bullet.PositionX, bullet.PositionY, bullet.DisplayAsChar);
                        Thread.Sleep(200);
                        ClearAtPosition(bullet.PositionX, bullet.PositionY);
                        WriteAtPosition(--bullet.PositionX, bullet.PositionY, bullet.DisplayAsChar);
                        TakeDownEnemy(bullet);

                        if (bullet.PositionX == this.LeftBorder || IsEnemyTakenDown())
                        {
                            bullet.IsShooted = false;
                            ClearAtPosition(bullet.PositionX, bullet.PositionY);
                            Thread.CurrentThread.Abort();
                        }
                    }

                }).Start();
            }
        }

        private void TakeDownEnemy(IAmmo ammo)
        {
            foreach (var enemy in this.Playground.Enemies)
            {
                if (ammo.PositionX == enemy.PositionX &&
                    ammo.PositionY == enemy.PositionY &&
                    enemy.IsLife)
                {
                    enemy.LifePoints -= ammo.Damage;

                    if (enemy.LifePoints <= 0)
                    {
                        enemy.IsLife = false;
                    }
                }
            }            
        }

        private bool IsEnemyTakenDown()
        {
            foreach (var enemy in this.Playground.Enemies)
            {
                if (!enemy.IsLife)
                {
                    this.Playground.Enemies.Remove(enemy);
                    return true;
                }
            }

            return false;
        }

        private Mario ReturnPlayerOfTypeMario(Character player)
        {
            var mario = player as Mario;

            if (mario == null)
            {
                throw new ArgumentNullException("mario", "Mario can not be null");
            }

            return mario;
        }

        private void MakePlayerFalling(Character player)
        {
            while (this.Playground.Playground[player.PositionY + 1, player.PositionX] != this.Playground.BRICK)
            {
                FallingDown(player);
            }
        }

        private bool CheckIfCharacterIsAtTheRightBorder(Character character)
        {
            if (this.Playground.Playground[character.PositionY, character.PositionX + 1] == this.Playground.BRICK)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfCharacterIsAtTheLeftBorder(Character character)
        {
            if (this.Playground.Playground[character.PositionY, character.PositionX - 1] == this.Playground.BRICK)
            {
                return true;
            }

            return false;
        }

        private bool ChechIfPlayerIsAtTheExit(Character player)
        {

            if (this.Playground.Playground[player.PositionY, player.PositionX - 1] == this.Playground.EXIT ||
                this.Playground.Playground[player.PositionY, player.PositionX + 1] == this.Playground.EXIT)
            {
                return true;
            }

            return false;
        }

        private void GameWin(Character player)
        {
            Mario mario = ReturnPlayerOfTypeMario(player);

            Console.Clear();
            Console.WriteLine("YOU WIN!");
            Console.WriteLine("Score: {0}", mario.Points);
            Console.WriteLine("Do want to play one game more? Esc = NO, N = NEW GAME");
        }

        private void GameOver(Character player)
        {
            Mario mario = ReturnPlayerOfTypeMario(player);

            Console.Clear();
            Console.WriteLine("YOU LOSE!");
            Console.WriteLine("Score: {0}", mario.Points);
            Console.WriteLine("Do want to play one game more? Esc = NO, F1 = NEW GAME");
        }

        private bool CheckIfEnemyHitsPlyer(Character player)
        {
            foreach (var enemy in this.Playground.Enemies)
            {
                if (enemy.PositionX == this.Playground.Player.PositionX &&
                    enemy.PositionY == this.Playground.Player.PositionY)
                {
                    return true;
                }
            }

            return false;      
        }

        private void TakeBonus(Character player)
        {
            var mario = ReturnPlayerOfTypeMario(player);

            foreach (var bonus in this.Playground.Bonuses)
            {
                if (bonus.PositionX == this.Playground.Player.PositionX &&
                    bonus.PositionY == this.Playground.Player.PositionY)
                {
                    mario.Points += bonus.BonusPoints;
                }
            }
        }


    }
}
