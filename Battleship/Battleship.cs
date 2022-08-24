using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public class BattleShip
    {
        private int _targetedPlayer;
        private int _height;
        private int _numberOfShots;

        public BattleShip(int height, int numberOfShots)
        {
            _targetedPlayer = 1;
            _height = height;
            _numberOfShots = numberOfShots;
        }

        public string Play()
        {
            HashSet<Coordinate>[] ships =
            {
                GetShips(),
                GetShips()
            };

            for (var shot = 0; shot < _numberOfShots; shot++)
            {
                string[] shotCoordinates = Program.ReadLineAndSplit();
                var x = int.Parse(shotCoordinates[0]);
                var y = int.Parse(shotCoordinates[1]);

                var shotCoordinate = new Coordinate(x, y);

                if (ships[_targetedPlayer].Contains(shotCoordinate))
                {
                    ships[_targetedPlayer].Remove(shotCoordinate);

                    if (ships[_targetedPlayer].Count == 0)
                    {
                        NextPlayer();
                    }
                }
                else
                {
                    NextPlayer();
                }
                if (_targetedPlayer == 1 && ships.Any(s => s.Count == 0))
                {
                    for (; shot < _numberOfShots - 1; shot++)
                    {
                        Console.ReadLine();
                    }
                    break;
                }
            }

            if (ships[0].Count > 0 && ships[1].Count == 0)
            {
                return "player one wins";
            }

            else if (ships[0].Count == 0 && ships[1].Count > 0)
            {
                return "player two wins";
            }

            return "draw";
        }

        public HashSet<Coordinate> GetShips()
        {
            var ships = new HashSet<Coordinate>();

            for (var y = _height - 1; y >= 0; y--)
            {
                var line = Console.ReadLine().ToCharArray();

                for (var x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#') ships.Add(new Coordinate(x, y));
                }
            }

            return ships;
        }

        public int NextPlayer() => _targetedPlayer = (_targetedPlayer + 1) % 2;
    }
}
