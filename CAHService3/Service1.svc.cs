using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
namespace CAHService
{
	public class CAHService1 : ICAHService
	{

		static CardsAgainstHumanityGame theGame;
		public CAHService1()
		{

			string[] whtcards = new string[]{	"An icepick lobotomy",
												"Friends with benefits",
												"Teaching a robot to love.",
												"Women's suffrage.",
												"The heart of a child.",
												"Smallpox blankets.",
												"John Wilkes Booth.",
												"Being rich.",
												"Michael Jackson."};

			string[] blkcards = new string[]{	"______.  That's how I want to die.",
												"In the new Disney Channel original movie, Hannah Montana struggles with _______.",
												"______ and _____ are the new hot couple."};
			theGame = new CardsAgainstHumanityGame(whtcards, blkcards);
		}
													
		/*public CAHService1(CardsAgainstHumanityGame param)
		{
			theGame = param;
		}*/
		public gameInfo czarchooses(int choice, int playerID)
		{
			return new gameInfo();
		}

		public int testservice() { return 1; }

		public int playcard(int cardID, int playerID)
		{
			theGame.playcard(cardID, playerID);

			return theGame.playedthisturn.Count();
		}
		public newturninfo getnewturninfo(int playerID)
		{
			return theGame.getnewturninfo(playerID);
		}
		public gameInfo firstconnect(string playername)
		{

			gameInfo ret = new gameInfo();

			ret.wht = theGame.whtdeck.decklist;
			ret.blk = theGame.blkdeck.decklist;

			ret.blkcard = theGame.black;

			ret.cardsdrawn = theGame.getHand(theGame.addPlayer(playername)).ToArray();

			ret.playernames = new string[theGame.players.Count];
			ret.playerpts = new int[theGame.players.Count];

			for (int i = 0; i < theGame.players.Count(); i++)
			{
				ret.playernames[i] = theGame.players[i].name;
				ret.playerpts[i] = theGame.players[i].points;
			}


			ret.czarname = ret.playernames[theGame.czarid];
			ret.gstate = theGame.gstate;
			return ret;
		}
	}

	
	
}