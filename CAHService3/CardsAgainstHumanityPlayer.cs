using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAHService
{
	public class CardsAgainstHumanityPlayer
	{
		public List<int> hand;
		public int points;
		public string name;
		private bool playedcard;

		static int IDmaker;
		int playerID;

		public void unplay()
		{
			playedcard = false;
		}
		public bool play()
		{
			if (playedcard)
				return true;
			playedcard = true;
			return false;
		}
		public CardsAgainstHumanityPlayer(string Name)
		{
			playerID = IDmaker;
			IDmaker++;
			hand = new List<int>();
			points = 1;
			name = Name;
			playedcard = false;

		}

	}
	
}