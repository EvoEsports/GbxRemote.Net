namespace GbxRemoteNet.Structs;

public class TmLobbyInfo
{
    public bool IsLobby { get; set; }
    public int LobbyPlayers { get; set; }
    public int LobbyMaxPlayers { get; set; }
    public double LobbyPlayersLevel { get; set; }
}