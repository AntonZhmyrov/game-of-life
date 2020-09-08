using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Game
    {
        private const int LowBorderValue = 0;
        
        private const int NumberOfRows = 25;
        private const int NumberOfColumns = 25;
        
        private bool[,] _gameField;

        public int Rows { get; } = NumberOfRows;

        public int Columns { get; } = NumberOfColumns;

        public bool[,] Initialize(string configuration)
        {
            _gameField = new bool[Rows, Columns];

            var configurationIndexes = ParseConfiguration(configuration);

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var index = configurationIndexes.FirstOrDefault(entity => entity.Row == i && entity.Column == j);
                    _gameField[i, j] = index != null;
                }
            }
            
            return _gameField;
        }
        
        public bool[,] NextGeneration()
        {
            var nextGeneration = new bool[Rows, Columns];
            
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var isAlive = _gameField[i, j];
                    var aliveNeighbors = CalculateAliveNeighbors(new Index { Row = i, Column = j });

                    if (isAlive)
                    {
                        if (aliveNeighbors < 2)
                        {
                            isAlive = false;
                        }
                        else if (aliveNeighbors > 3)
                        {
                            isAlive = false;
                        }
                    }
                    else
                    {
                        if (aliveNeighbors == 3)
                        {
                            isAlive = true;
                        }
                    }

                    nextGeneration[i, j] = isAlive;
                }
            }

            _gameField = nextGeneration;
            
            return nextGeneration;
        }

        private static Index[] ParseConfiguration(string configuration)
        {
            var configurationIndexes = new List<Index>();
            var cellConfigurations = configuration.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var cellConfiguration in cellConfigurations)
            {
                var cellConfig = cellConfiguration.Trim('(', ')', ' ');
                var indexes = cellConfig.Split(":", StringSplitOptions.RemoveEmptyEntries);

                var row = Convert.ToInt32(indexes[0]);
                var column = Convert.ToInt32(indexes[1]);
                
                configurationIndexes.Add(new Index { Row = row, Column = column});
            }

            return configurationIndexes.ToArray();
        }

        private int CalculateAliveNeighbors(Index index)
        {
            var aliveNeighbors = 0;

            if (index.Row - 1 >= LowBorderValue &&
                index.Column - 1 >= LowBorderValue &&
                _gameField[index.Row - 1, index.Column - 1])
            {
                aliveNeighbors++;
            }

            if (index.Row - 1 >= LowBorderValue && _gameField[index.Row - 1, index.Column])
            {
                aliveNeighbors++;
            }

            if (index.Row - 1 >= LowBorderValue &&
                index.Column + 1 < Columns &&
                _gameField[index.Row - 1, index.Column + 1])
            {
                aliveNeighbors++;
            }

            if (index.Column - 1 >= LowBorderValue && _gameField[index.Row, index.Column - 1])
            {
                aliveNeighbors++;
            }

            if (index.Column + 1 < Columns && _gameField[index.Row, index.Column + 1])
            {
                aliveNeighbors++;
            }

            if (index.Row + 1 < Rows &&
                index.Column - 1 >= LowBorderValue &&
                _gameField[index.Row + 1, index.Column - 1])
            {
                aliveNeighbors++;
            }

            if (index.Row + 1 < Rows && _gameField[index.Row + 1, index.Column])
            {
                aliveNeighbors++;
            }

            if (index.Row + 1 < Rows && 
                index.Column + 1 < Columns && 
                _gameField[index.Row + 1, index.Column + 1])
            {
                aliveNeighbors++;
            }

            return aliveNeighbors;
        }
    }
}