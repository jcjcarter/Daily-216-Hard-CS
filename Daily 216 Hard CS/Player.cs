using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_216_Hard_CS
{
    public class Player
    {
        public string Name { get; private set; }
        public Card[] Cards { get; private set;  }
        public bool Folded { get; set; }

        public Player(string name) {
            this.Name = name;
            Cards = new Card[2];
            Folded = false;
        }

        public override string ToString()
        {
            return string.Format("{0} Hand:\t{1} {2}", Name == "You" ? "Your" : 
                Name, Cards[0], Cards[1]);
        }
    }
}
