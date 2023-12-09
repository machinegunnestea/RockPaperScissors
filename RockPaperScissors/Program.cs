using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace RockPaperScissors
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string[] moves;
            //do
            //{
            //    WriteLine("Enter the moves:");
            //    moves = ReadLine().Split(' ');
            //    if (!IsValidMovesInput(moves))
            //        WriteLine("Invalid number of moves. You must provide an odd number of unique moves.");
            //} while (!IsValidMovesInput(moves));

            if (!IsValidMovesInput(args))
            {
                Console.WriteLine("Invalid arguments. You must provide an odd number of unique moves.");
                return;
            }

            Game game = new Game(args);
            DisplayHelpTable helpTable = new DisplayHelpTable(args);

            byte[] key = Crypto.GenerateKey();
            byte[] computerMove = game.GenerateComputerMove(key);

            byte[] hmacBytes = Crypto.GenerateHMAC(key, computerMove);
            string hmac = BitConverter.ToString(hmacBytes).Replace("-", "");

            WriteLine($"HMAC: {hmac}");

            game.DisplayMenu();
            int userMove = game.GetUserMove();

            WriteLine($"Your move: {args[userMove - 1]}");
            WriteLine($"Computer move: {args[computerMove[0] % args.Length]}");


            Result result = game.DetermineWinner(userMove - 1, computerMove[0] % args.Length);
            WriteLine(result == Result.Draw ? "It's a draw!" : result == Result.Win ? "You win!" : "You lose.");


            WriteLine($"HMAC key: {BitConverter.ToString(key).Replace("-", "")}");
        }

        static bool IsValidMovesInput(string[] moves)
        {
            return moves.Length >= 3 && moves.Length % 2 != 0 && moves.Distinct().Count() == moves.Length;
        }
    }
}
