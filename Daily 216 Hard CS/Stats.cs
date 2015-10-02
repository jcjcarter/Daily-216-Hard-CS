using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
   public class Stats
    {
        public int TotalGames { get; private set; }
        public int BadFolds { get; private set; }
        public Dictionary<Player, int> WinCount { get; private set; }
        public List<HandType> WinHands { get; private set; }

        public Stats(Player[] players)
        {
            BadFolds = 0;
            TotalGames = 0;
            WinCount = new Dictionary<Player, int>();
            WinHands = new List<HandType>();

            foreach (Player p in players)
            {
                WinCount.Add(p, 0);
            }

        }

        public void AddResult(Player[] winners, HandType handType, bool badFold)
        {
            foreach (Player p in winners)
            {
                WinCount[p]++;
            }

            WinHands.Add(handType);

            if (badFold)
            {
                BadFolds++;
                TotalGames++;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Total games:\t{0}\n", TotalGames);

            sb.AppendFormat("Bad folds:\t{0} ({1:0.00%})\n\n", BadFolds, (double)BadFolds /
                (double)TotalGames);

            sb.AppendLine(" Player     WIN-LOSS        WIN %");

            sb.AppendLine("----------------+----------------------+---------------");

            foreach (var kv in WinCount)
            {
                sb.AppendFormat(" {0,-14}| {1,-17}| {2.00.00%}\n", kv.Key.Name,
                    String.Format("{0}-{1}",
                    kv.Value,
                    TotalGames - kv.Value), (double)kv.Value / (double)TotalGames);

            }

            sb.AppendLine(" ");
            sb.AppendLine("            HAND           WIN %");
            sb.AppendLine("------------------------+---------------------");
            var winTypes = WinHands.GroupBy(t => t).OrderBy(grp => (int)grp.Key).Select(
                grp => new { type = grp.Key, count = grp.Count() });

            foreach (var obj in winTypes)
            {
                sb.AppendFormat(" {0, -18}|{1.00.00%}\n", Hand.StringFromType(obj.type),
                    (double)obj.count / (double)TotalGames);
            }


            return sb.ToString();
        }
    }
}
