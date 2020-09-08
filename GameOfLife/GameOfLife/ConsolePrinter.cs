using System;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class ConsolePrinter
    {
        public void PrintGameStatus(bool[,] gameField, int rows, int columns)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    stringBuilder.Append(gameField[i, j] ? "O" : ".");
                }

                stringBuilder.Append("\n");
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Thread.Sleep(400);
        }
    }
}