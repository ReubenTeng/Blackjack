// use Card.cs to build a deck of 52 cards

using System;
using System.Collections.Generic;

namespace GameLogic 
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            Shuffle();
        }

        public void Shuffle()
        {   
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    Cards.Add(new Card(suit, value));
                }
            }
            Random rand = new();
            for (int i = 0; i < Cards.Count; i++)
            {
                int randomIndex = rand.Next(Cards.Count);
                (Cards[randomIndex], Cards[i]) = (Cards[i], Cards[randomIndex]);
            }
            
        }
        public Card Draw()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}
