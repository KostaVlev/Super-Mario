namespace ConsoleUI
{
    using SuperMario;

    /// <summary>
    /// Main entry point for the game.
    /// </summary>
    class ConsoleUI
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            while (!game.IsFinished)
            {
                GamePlayground playGround = new GamePlayground();
                GameEngine engine = new GameEngine(playGround, game);

                while (game.IsRunning)
                {
                    engine.CheckIfKeyIsPressed();
                }
            }     
        }
    }
}
