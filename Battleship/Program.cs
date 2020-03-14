using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;
using BattleshipLibrary.Models;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel player1 = CreatePlayer("Player 1"); //can be thought of as active player
            PlayerInfoModel player2 = CreatePlayer("Player 2"); //can be thought of as opponent
            Console.WriteLine("BATTLESHIP");
            Console.WriteLine($"{player1.UserName} vs {player2.UserName}");
            Console.WriteLine();
            Console.WriteLine($"{player1.UserName} will go first");
            Console.WriteLine("Press ENTER when ready");
            Console.ReadLine();
            PlayerInfoModel winner = null;

            do
            {
                DisplayShotGrid(player1);
                RecordPlayerShot(player1, player2);
                bool gameStatus = GameLogic.CheckGameStatus(player2);

                if (gameStatus)
                {
                    //Player swap tuple
                    (player1, player2) = (player2, player1);
                }
                else
                {
                    winner = player1;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to BATTLESHIP");
            Console.WriteLine("Created by chrissc6");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer(string player)
        {
            Console.WriteLine($"Player information for {player}");
            PlayerInfoModel output = new PlayerInfoModel();
            ShowGridBlank();
            output.UserName = AskForUserName(player);
            GameLogic.InitializeGrid(output);
            PlaceShips(output);
            Console.Clear();
            return output;
        }

        private static string AskForUserName(string player)
        {
            //Console.WriteLine($"Welcome {player}");
            Console.Write("Enter your name: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void PlaceShips(PlayerInfoModel m)
        {
            do
            {
                Console.Write($"Where do you want to place ship {m.ShipLocations.Count + 1} out of 5? : ");
                string location = Console.ReadLine();
                bool isValidLocation = GameLogic.PlaceShip(m, location);
                if(!isValidLocation)
                {
                    Console.WriteLine("Invalid location, please try again.");

                }

            } while (m.ShipLocations.Count < 5);
        }

        private static void DisplayShotGrid(PlayerInfoModel player)
        {
            string currentRow = player.ShotGrid[0].SpotLetter;

            foreach (var g in player.ShotGrid)
            {
                if (g.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = g.SpotLetter;
                }

                if (g.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {g.SpotLetter}{g.SpotNumber} ");
                }
                else if (g.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X  ");
                }
                else if (g.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.Write(" ?  ");
                }
            }
            Console.WriteLine();
        }

        private static void RecordPlayerShot(PlayerInfoModel player1, PlayerInfoModel player2)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(player1);
                (row, column) = GameLogic.SplitShotInfo(shot);
                isValidShot = GameLogic.ValidateShot(player1, row, column);

                if (!isValidShot)
                {
                    Console.WriteLine("Invalid shot location, try again");
                }
            } while (!isValidShot);

            bool isHit = GameLogic.IdenfityShotResult(player2, row, column);

            GameLogic.MarkShotResult(player1, row, column, isHit);

            DisplayShotGridResults(row, column, isHit);
        }

        private static void DisplayShotGridResults(string row, int column, bool isHit)
        {
            if (isHit)
            {
                Console.WriteLine($"{row}{column} is a HIT!");
            }
            else
            {
                Console.WriteLine($"{row}{column} is a miss.");
            }
            Console.WriteLine("=====================================");
            Console.WriteLine();
        }

        private static string AskForShot(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.Write($"{player.UserName} enter shot selection: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations {winner.UserName} you WIN!");
            Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots.");
        }

        private static void ShowGridBlank()
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
                    Console.Write($"{ l}{ n} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
