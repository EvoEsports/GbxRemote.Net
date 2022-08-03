namespace GbxRemoteNet.Structs;

public class SystemInfo
{
    public string PublishedIp { get; set; }
    public int Port { get; set; }
    public int P2PPort { get; set; }
    public string TitleId { get; set; }
    public string ServerLogin { get; set; }
    public int ServerPlayerId { get; set; }
    public int ConnectionDownloadRate { get; set; }
    public int ConnectionUploadRate { get; set; }
    public bool IsServer { get; set; }
    public bool IsDedicated { get; set; }
}