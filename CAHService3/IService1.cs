using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CAHService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICAHService" in both code and config file together.
    [ServiceContract]
    public interface ICAHService
    {

        [OperationContract]
        gameInfo firstconnect(string playername);

        [OperationContract]
        int playcard(int cardinhand, int playerID);

        [OperationContract]
        gameInfo czarchooses(int choice, int playerID);

		[OperationContract]
		newturninfo getnewturninfo(int playerID);

		[OperationContract]
		int testservice();
    }

    [DataContract]
    public class newturninfo //return to clinets @ eot
    {
        [DataMember]
        public int[] hand;

        [DataMember]
        public int[] cardsplayed;

        [DataMember]
        public int[] playerIDs;
    }

    [DataContract]
    public class gameInfo //gameinfo to return to one client
    {
        [DataMember]
        public int[] hand { get; set; }
        [DataMember]
        public gamestate gstate { get; set; }

        [DataMember]
        public int blkcard;
        [DataMember]
        public int[] cardsplayed { get; set; }

        [DataMember]
        public int[] cardsdrawn { get; set; }

        [DataMember]
        public string czarname { get; set; }
        [DataMember]
        public string[] playernames { get; set; }
        [DataMember]
        public int[] playerpts { get; set; }

        [DataMember]
        public string[] blk { get; set; }
        [DataMember]
        public string[] wht { get; set; }

        [DataMember]
        public int playerID { get; set; }
    }
    [DataContract]
    public enum gamestate { NONE, PLAY, CZAR, WAIT }
}
