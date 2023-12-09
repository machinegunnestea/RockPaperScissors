using ConsoleTables;
using System;
using System.Linq;

namespace RockPaperScissors
{
    internal class DisplayHelpTable
    {
        private readonly string[] moves;

        public DisplayHelpTable(string[] moves)
        {
            this.moves = moves;
        }

        public void ShowTable()
        {
            var headers = new string[] { " < PC/User > " }.Concat(moves).ToArray();
            var table = new ConsoleTable(headers);

            for (int i = 0; i < moves.Length; i++)
            {
                table.AddRow(BuildRow(i));
            }

            Console.WriteLine(table.ToString());
        }

        private string[] BuildRow(int rowIndex)
        {
            var row = new string[moves.Length + 1];
            row[0] = moves[rowIndex];
            Game game = new Game(moves);

            for (int i = 0; i < moves.Length; i++)
            {

                Result result = game.DetermineWinner(i+ 1, rowIndex + 1);
                row[i +1] = result.ToString();
            }

            return row;
        }
    }
}
