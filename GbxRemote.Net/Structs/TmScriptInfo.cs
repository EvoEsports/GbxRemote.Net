namespace GbxRemoteNet.Structs;

public class TmScriptInfo
{
    public string Name { get; set; }
    public string CompatibleMapTypes { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public TmScriptParamDesc[] ParamDescs { get; set; }
    public TmScriptCommandDescs[] CommandDescs { get; set; }
}