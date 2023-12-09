using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Game
    {
        private string[] moves;

        public Game(string[] moves)
        {
            this.moves = moves;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        public int GetUserMove()
        {
            int userMove;
            do
            {
                Console.Write("Enter your move: ");
                string input = Console.ReadLine();

                if (input == "?")
                {
                    DisplayHelpTable helpTable = new DisplayHelpTable(moves);
                    helpTable.ShowTable();
                    DisplayMenu();
                    continue;
                }
                else if (Convert.ToInt32(input) == 0)
                {
                    DisplayMenu();
                }

            } while (!int.TryParse(Console.ReadLine(), out userMove) || userMove < 0 || userMove > moves.Length);

            return userMove;
        }

        public byte[] GenerateComputerMove(byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] randomBytes = new byte[1];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(randomBytes);
                }

                return hmac.ComputeHash(randomBytes);
            }
        }

        public Result DetermineWinner(int userMove, int computerMove)
        {
            //int halfLength = (moves.Length - 1) / 2;
            ////if ((userMove + 1) % moves.Length == computerMove - 1 || (userMove + halfLength) % moves.Length == (computerMove - 1 + halfLength) % moves.Length)
            //if (userMove == computerMove + 1 || userMove == (computerMove + moves.Length / 2 + 1) % moves.Length)
            ////if (computerMove == userMove + 1 || computerMove == (userMove + moves.Length / 2 + 1) % moves.Length)
            //{
            //    return Result.Win;
            //}
            //else if (userMove == computerMove)
            //{
            //    return Result.Draw;
            //}
            //else
            //{
            //    return Result.Lose;
            //}
            int diff = (userMove - computerMove + moves.Length) % moves.Length;
            int halfRounds = moves.Length / 2;

            if (diff == 0)
                return Result.Draw; // Ничья
            else if (diff <= halfRounds)
                return Result.Win; // Победа игрока
            else
                return Result.Lose; // Победа компьютера
        }
    }


}
