using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    class Hand
    {
        public Card[] UsedCards { get; private set; }
        public Card[] Kickers { get; private set; }
        public HandType Type { get; private set; }
        public string TypeString {
            get { return Hand.StringFromType(Type); }
        }

        public Hand(HandType type, Card[] cards, Card[] kickers) {
            UsedCards = cards;
            Kickers = kickers;
            Type = type;
        }

        public static string StringFromType(HandType type) {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
                Enum.GetName(typeof(HandType), type).Replace('_', ' ').ToLower());
        }
    }
}
