namespace GbxRemoteNet.Structs {
    public class PlayerNetInfo {
        public string IPAddress { get; set; }
        public int StateUpdateLatency { get; set; }
        public int LatestNetworkActivity { get; set; }
        public double PacketLossRate { get; set; }
    }
}