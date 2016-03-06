namespace SuperMario
{
    /// <summary>
    /// Definig the game state.
    /// </summary>
    public class Game
    {
        private bool isRunning;
        private bool isFinished;
        private bool isGameOver;

        public Game()
        {
            this.IsRunning = true;
            this.IsFinished = false;
            this.IsGameOver = false;
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                this.isRunning = value;
            }
        }

        public bool IsFinished
        {
            get
            {
                return this.isFinished;
            }

            set
            {
                this.isFinished = value;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return this.isGameOver;
            }

            set
            {
                this.isGameOver = value;
            }
        }
    }
}
