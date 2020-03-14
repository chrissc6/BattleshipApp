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

            return output;
        }

        public static bool CheckGameStatus(PlayerInfoModel player2)
        {
            throw new NotImplementedException();
        }

        public static int GetShotCount(PlayerInfoModel winner)
        {
            throw new NotImplementedException();
        }

        public static (string row, int column) SplitShotInfo(string shot)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateShot(PlayerInfoModel player1, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static bool IdenfityShotResult(PlayerInfoModel player2, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static void MarkShotResult(PlayerInfoModel player1, string row, int column, bool isHit)
        {
            throw new NotImplementedException();
        }
    }
}
