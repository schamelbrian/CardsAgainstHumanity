using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAHService
{
	public partial class Deck
	{
		private Random rng;
		private string[] allcards;
		public List<int> discard;
		public Queue<int> cardsleft;

		public string[] decklist
		{
			get { return allcards; }
		}
		public string this[int index]
		{
			get { return allcards[index]; }
		}

		public Deck() { ;}
		public Deck(string[] cardlist, Random gen)
		{
			rng = gen;
			allcards = cardlist;

			discard = new List<int>();
			cardsleft = new Queue<int>();

			for (int card = 0; card < allcards.Count(); card++)
			{
				discard.Add(card);
			}
			shuffle();
		}
		public Deck(string deckfile)
		{
			rng = new Random();
			discard = new List<int>();
			cardsleft = new Queue<int>();
			allcards = System.IO.File.ReadAllLines(deckfile);
			for (int card = 0; card < allcards.Count(); card++)
			{
				discard.Add(card);
			}
			shuffle();

		}
		//puts discard on bottom of deck in random order
		public void shuffle()
		{
			while (discard.Count != 0)
			{
				int index = rng.Next(discard.Count);
				cardsleft.Enqueue(discard[index]);
				discard.RemoveAt(index);
			}
		}
		public int drawFrom()
		{
			if (cardsleft.Count == 0)
				shuffle();
			return cardsleft.Dequeue();
		}

	}
}