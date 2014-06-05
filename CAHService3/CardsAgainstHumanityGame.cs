using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace CAHService
{
	public partial class CardsAgainstHumanityGame
	{
		//private Form serverform;
		private string homeDir;

		public Deck whtdeck;
		public Deck blkdeck;
		public List<int> playedlastturn;

		private Random rng;
		private int handsize;
		public List<CardsAgainstHumanityPlayer> players;
		public List<int> playedthisturn;//cards played this turn
		private const int HOSTPLAYER = 0;

		//ServiceHost selfHost;

		public int MAXPLAYERS = 60;

		//private Thread[] threads;

		private int blkcard; public int black { get { return blkcard; } }
		private int czarplayerid; public int czarid { get { return czarplayerid; } }

		public gamestate gstate;

		public List<int> getdiscard()
		{
			return whtdeck.discard;
		}
		public Queue<int> getdeckleft()
		{
			return whtdeck.cardsleft;
		}
		public int playersfinished()
		{//eventually change to accomodate more cards
			return playedthisturn.Count();
		}

		public int Handsize
		{
			get { return handsize; }
			set { handsize = Handsize; }
		}

		public CardsAgainstHumanityGame(string[] whtcards, string[] blkcards)
		{
			rng = new Random();

			getRules();
			makeCardDecks(blkcards, whtcards);
			
			players = new List<CardsAgainstHumanityPlayer>();

			playedthisturn = new List<int>();
		}
		public CardsAgainstHumanityGame(/*Form theform, */string whtcardsfile = "wht.txt",
										string blkcardsfile = "blk.txt", string homedir = @"D:\CAH\WcfService2\WcfService2\bin\")
		{
			homeDir = homedir;


			getRules();
			loadCardDecks(homeDir + blkcardsfile, homeDir + whtcardsfile);
			rng = new Random();
			players = new List<CardsAgainstHumanityPlayer>();

			playedthisturn = new List<int>();

			//openService();

		}

		/*private void openService()
		{
			// Step 1 of the address configuration procedure: Create a URI to serve as the base address.
			//pass this to svcutil
			Uri baseAddress = new Uri("http://localhost:8000/ServiceModelSamples/Service");

			// Step 2 of the hosting procedure: Create ServiceHost
			selfHost = new ServiceHost(typeof(CAHService1), baseAddress);



			try
			{
				// Step 3 of the hosting procedure: Add a service endpoint.
				selfHost.AddServiceEndpoint(
					typeof(ICAHService),
					new WSHttpBinding(),
					"ICAHService");


				// Step 4 of the hosting procedure: Enable metadata exchange.
				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				selfHost.Description.Behaviors.Find<ServiceMetadataBehavior>().HttpGetEnabled = true;
				//selfHost.Description.Behaviors.Add(smb);


				// Step 5 of the hosting procedure: Start (and then stop) the service.
				selfHost.Open();


				//svcutil.exe /language:cs /out:generatedProxy.cs /config:app.config http://localhost:8000/ServiceModelSamples/service

				// Close the ServiceHostBase to shutdown the service.
				selfHost.Close();

			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("An exception occurred: {0}", ce.Message);
				Console.ReadKey();
				selfHost.Abort();
			}
		}*/
		public int addPlayer(string name)
		{

			players.Add(new CardsAgainstHumanityPlayer(name));
			fillHands();

			return players.Count - 1;

		}
		public List<int> getHand(int player)
		{
			return players[player].hand;
		}
		public void playcard(int card, int playerID)
		{
			if (players[playerID].play())
			{
				return;
			}


			players[playerID].hand.Remove(card);
			Console.WriteLine("Playing '" + whtdeck[card] + "' from " + players[playerID].name + ". currently left in their hand:");
			foreach (int cardID in players[playerID].hand)
			{
				Console.WriteLine(whtdeck[cardID]);
			}

			Console.WriteLine();

			playedthisturn.Add(card);

			if (playersfinished() == players.Count)
			{
				newTurn();
			}

		}

		private void waitforczar()
		{
			while (gstate == gamestate.CZAR)
				Thread.Sleep(0);
		}
		private void newTurn()
		{
			gstate = gamestate.WAIT;
			czarplayerid++;
			if (czarplayerid == players.Count())
				czarplayerid = 0;

			fillHands();
			foreach (int card in playedthisturn)
				whtdeck.discard.Add(card);

			playedthisturn.Clear();
			foreach (CardsAgainstHumanityPlayer player in players)
			{
				player.unplay();
			}

			blkcard = blkdeck.drawFrom();
		}
		private void fillHands()
		{
			for (int i = 0; i < players.Count; i++)
				while (players[i].hand.Count < handsize)
					draw(i);
		}
		private void draw(int playerID, int num = 1)
		{

			for (int i = 0; i < num; i++) players[playerID].hand.Add(whtdeck.drawFrom());
		}
		private void getRules()
		{
			handsize = 3;
		}
		private void getRules(string filepath) { loadRulesFile(filepath); }
		private void loadRulesFile(string filepath)
		{

		}//implement
		private void loadCardDecks(string blkcardsfile, string whtcardsfile)
		{
			whtdeck = new Deck(whtcardsfile);
			blkdeck = new Deck(blkcardsfile);
		}
		private void makeCardDecks(string[] blkcards, string[] whtcards)
		{
			whtdeck = new Deck(whtcards, rng);
			blkdeck = new Deck(blkcards, rng);
		}
		public newturninfo getnewturninfo(int playerID)
		{
			newturninfo ret = new newturninfo();
			ret.hand = players[playerID].hand.ToArray();
			ret.cardsplayed = playedthisturn.ToArray();
			return ret;
		}
	}
}