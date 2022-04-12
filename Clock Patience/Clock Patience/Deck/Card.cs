using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock_Patience.Deck
{
    public class Card
    {
        public enum Rank
        {
            A = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6,
            Seven = 7, Eight = 8, Nine = 9, T = 10, J = 11, Q = 12, K = 13
        }

        public enum Suit
        {
            H, D, C, S
        }

        public LinkedList<Card>[] holder { get; set; }

        public bool flipped { get; set; }

        public Rank rank { get; set; }
        public Suit suit { get; set; }



        public Card()
        {
            this.holder = new LinkedList<Card>[13];
            this.flipped = false;
        }
    }
}
