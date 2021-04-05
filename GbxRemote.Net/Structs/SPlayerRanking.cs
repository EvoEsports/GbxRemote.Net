using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class SPlayerRanking {
        /// <summary>
        /// TM2020
        /// </summary>
        public string Login;
        /// <summary>
        /// TM2020
        /// </summary>
        public string NickName;
        /// <summary>
        /// TM2020
        /// </summary>
        public string PlayerId;
        /// <summary>
        /// TM2020
        /// </summary>
        public int Rank;
        /// <summary>
        /// Legacy
        /// </summary>
        public int BestTime;
        /// <summary>
        /// Legacy
        /// </summary>
        public int[] BestCheckpoints;
        /// <summary>
        /// Legacy
        /// </summary>
        public int Score;
        /// <summary>
        /// Legacy
        /// </summary>
        public int NbrLapsFinished;
        /// <summary>
        /// Legacy
        /// </summary>
        public double LadderScore;
    }
}
