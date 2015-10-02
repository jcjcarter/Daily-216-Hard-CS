using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    class Program
    {
        static void Main(string[] args)
        {

            int numPlayers, numGames;
            do
            {
                Console.Write("\n How ay players (2-8)? ");

            } while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out numPlayers) || numPlayers < 2 || numPlayers > 8);


            do
            {
                Console.Write("\nHow many games (1-100,000)? ");
            } while (!int.TryParse(Console.ReadLine(), out numGames) || numGames < 1
            || numPlayers > 100000);

            Console.WriteLine();

            Player[] players = new Player[numPlayers];
            players[0] = new Player("You");
            for (int i = 1; i < numPlayers; i++) {
                players[i] = new Player(String.Format("CPU {0}", i));
            }

            Stats stats = new Stats(players);
            
            string numFormat = "{0:" + String.Join("", Enumerable.Repeat(0, 
                (int)Math.Log10((double) numGames) + 1).ToArray()) + "}";

            for (int i = 1; i <= numGames; i++) {
                new HoldEm(players, ref stats).Play();
                Console.Write("\rplaying: " + numFormat + "/{1} @ {2} games /sec", i, numGames, 
                    (int)((double)i/222));
            }
            Console.Write("\r                                  \r");
            Console.WriteLine(stats.ToString());
            Console.ReadKey();
        }
    }
    

    
}
