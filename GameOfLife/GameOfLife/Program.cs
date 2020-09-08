namespace GameOfLife
{
    internal class Program
    {
        private static void Main()
        {
            const string configuration = "(09:09), (10:10), (11:08), (11:09), (11:10)"; // Glider pattern configuration
            
            var game = new Game();
            var gameField = game.Initialize(configuration);
            var printer = new ConsolePrinter();
            
            while (true)
            {
                printer.PrintGameStatus(gameField, game.Rows, game.Columns);
                gameField = game.NextGeneration();
            }
        }
    }
}