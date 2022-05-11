using System;
using System.Collections.Generic;
using System.Linq;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Enemies;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core
{
    public class MapBuilder
    {
        private const int SIZE_OF_GRID = 40;

        private readonly IHandler<IEnemy> _enemyHandler;
        private readonly Random _random;

        internal MapBuilder(IHandler<IEnemy> enemyHandler)
        {
            _enemyHandler = enemyHandler;
            _random = new Random();
        }

        public Map Build(string[] mapLines)
        {
            var rows = mapLines.Length;
            var cols = mapLines[0].Length;
            var grid = new Grid(rows, cols, SIZE_OF_GRID);
            Point startingPoint = new(), endPoint = new();

            for (int x = 0; x < rows; x++)
                for (int y = 0; y < cols; y++)
                {
                    var element = grid[x, y];
                    switch (mapLines[x][y])
                    {
                        case ' ':
                            element.Type = GridElementType.Road;
                            break;
                        case 'x':
                            element.Type = GridElementType.Grass;
                            break;
                        case 'S':
                            startingPoint = new Point(x, y);
                            element.Type = GridElementType.Start;
                            break;
                        case 'E':
                            element.Type = GridElementType.End;
                            endPoint = new Point(x, y);
                            break;
                    }
                }

            var visitedPoints = new List<Point>();
            var visitingPoint = startingPoint;

            while (visitingPoint != endPoint)
            {
                visitedPoints.Add(visitingPoint);

                var (sX, sY) = ((int)visitingPoint.X, (int)visitingPoint.Y);
                if (mapLines[sX + 1][sY] == ' ' && !visitedPoints.Contains(visitingPoint + Vector.Right))
                {
                    visitingPoint += Vector.Right;
                }
                else if (mapLines[sX - 1][sY] == ' ' && !visitedPoints.Contains(visitingPoint + Vector.Left))
                {
                    visitingPoint += Vector.Left;
                }
                else if (mapLines[sX][sY + 1] == ' ' && !visitedPoints.Contains(visitingPoint + Vector.Up))
                {
                    visitingPoint += Vector.Up;
                }
                else if (mapLines[sX][sY - 1] == ' ' && !visitedPoints.Contains(visitingPoint + Vector.Down))
                {
                    visitingPoint += Vector.Down;
                }
                else
                {
                    break;
                }
            }
            visitedPoints.Add(endPoint);

            var enemyPathPoints = new List<Point>();
            enemyPathPoints.Add(startingPoint);
            for (int i = 1; i < visitedPoints.Count - 1; i++)
            {
                if (visitedPoints[i + 1] - visitedPoints[i]
                    != visitedPoints[i] - visitedPoints[i - 1])
                {
                    enemyPathPoints.Add(visitedPoints[i]);
                }
            }
            enemyPathPoints.Add(endPoint);

            var enemyPath = new Path(enemyPathPoints.Select(p => grid[(int)p.X, (int)p.Y].Center).ToArray());

            var enemies = new SortedList<int, SortedList<TimeSpan, IEnemy>>();
            for (int i = 0; i < 10; i++)
            {
                var enemyTimes = new SortedList<TimeSpan, IEnemy>();
                for (int j = 0; j < _random.Next(10, 30); j++)
                {
                    enemyTimes.Add(
                        TimeSpan.FromSeconds(3 + i * 30 + j + (0.3 * _random.NextDouble() - 1.5)),
                        GetRandomEnemy(enemyPath));
                }

                enemies.Add(i, enemyTimes);
            }

            return new Map(
                _enemyHandler,
                grid,
                enemyPath,
                enemies);
        }

        private IEnemy GetRandomEnemy(Path enemyPath)
        {
            var vote = _random.NextDouble();

            return vote switch
            {
                >= 0.9f => new SkullEnemy(enemyPath),
                >= 0.7f => new RabbitEnemy(enemyPath),
                >= 0.5f => new ManEnemy(enemyPath),
                < 0.5f => new EmojiEnemy(enemyPath),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
