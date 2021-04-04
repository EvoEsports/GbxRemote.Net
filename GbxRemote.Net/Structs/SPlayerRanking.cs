using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbxRemoteNet.Structs {
    public class SPlayerRanking {
        public string Login;
        public string NickName;
        public int BestTime;
        public int[] BestCheckpoints;
        public int Score;
        public int NbrLapsFinished;
        public double LadderScore;
    }
}
