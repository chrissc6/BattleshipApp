using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel m)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };
            List<int> nums = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            foreach (string l in letters)
            {
                foreach (int n in nums)
                {
                    AddGridSpot(m, l, n);
                }
            }
        }

        private static void AddGridSpot(PlayerInfoModel m, string l, int n)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = l,
                SpotNumber = n,
                Status = GridSpotStatus.Empty
            };
            m.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerInfoModel m, string loc)
        {
            bool output = false;

            (string row, int column) = SplitShotInfo(loc);

            bool isValidLocation = ValidateGridLocation(m, row, column);
            bool isSpotOpen = ValidateShipLocation(m, row, column);

            if (isValidLocation && isSpotOpen)
            {
                m.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });

                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel m, string row, int column)
        {
            bool isValid = true;

            foreach (var s in m.ShipLocations)
            {
                if (s.SpotLetter.ToUpper() == row.ToUpper() && s.SpotNumber == column)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private static bool ValidateGridLocation(PlayerInfoModel m, string row, int column)
        {
            bool isValid = false;

            foreach (var s in m.ShotGrid)
            {
                if (s.SpotLetter.ToUpper() == row.ToUpper() && s.SpotNumber == column)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public static bool CheckGameStatus(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }

            return isActive;
        }

        public static int GetShotCount(PlayerInfoModel winner)
        {
            int count = 0;

            foreach (var shot in winner.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    count += 1;
                }
            }

            return count;
        }

        public static (string row, int column) SplitShotInfo(string shot)
        {
            char[] shotInfo = shot.ToArray();


            string row;
            int column;

            if (shot.Length != 2)
            {
                //will give back invalid location
                row = "Z";
                column = 9;
            }
            else
            {

                column = NumCheck(shotInfo[1].ToString());
                row = shotInfo[0].ToString();
            }

            return (row, column);
        }

        private static int NumCheck(string v)
        {
            bool x = true;
            int y;
            x = int.TryParse(v, out y);

            if (x)
            {
                return y;
            }
            else
            {
                return 9;
            }

        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool isValid = false;

            foreach (var g in player.ShotGrid)
            {
                if (g.SpotLetter.ToUpper() == row.ToUpper() && g.SpotNumber == column)
                {
                    if(g.Status == GridSpotStatus.Empty)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        public static bool IdenfityShotResult(PlayerInfoModel player, string row, int column)
        {
            bool isValid = false;

            foreach (var s in player.ShipLocations)
            {
                if (s.SpotLetter.ToUpper() == row.ToUpper() && s.SpotNumber == column)
                {
                    isValid = true;
                    s.Status = GridSpotStatus.Sunk;
                }
            }

            return isValid;
        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isHit)
        {
            foreach (var s in player.ShotGrid)
            {
                if (s.SpotLetter.ToUpper() == row.ToUpper() && s.SpotNumber == column)
                {
                    if (isHit)
                    {
                        s.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        s.Status = GridSpotStatus.Miss;
                    }
                }
            }
        }
    }
}
