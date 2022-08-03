namespace GbxRemoteNet.Structs;

public class ScriptInfo
{
    public string Name { get; set; }
    public string CompatibleMapTypes { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public ScriptParamDesc[] ParamDescs { get; set; }
    public ScriptCommandDescs[] CommandDescs { get; set; }
}