using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ModeScriptExample {
    class Program {
        // create client instance
        static GbxRemoteClient client = new("trackmania.test.server", 5001);

        static async Task MainAsync(string[] args) {
            // connect and login
            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // register event that gets all modescript callbacks
            client.OnModeScriptCallback += Client_OnModeScriptCallback;

            // enable callbacks
            await client.EnableCallbackTypeAsync();

            // get all modescript callbacks
            var ret = await client.GetModeScriptResponseAsync("XmlRpc.GetCallbacksList");
            Console.WriteLine("ModeScript Callbacks:");
            foreach (string callback in ret["callbacks"].Values<string>())
                Console.WriteLine($"- {callback}");

            // wait indefinitely
            await Task.Delay(-1);
        }

        private static async Task Client_OnModeScriptCallback(string method, JObject data) {
            if (method == "Trackmania.Event.GiveUp") {
                string playerLogin = data["login"].Value<string>();
                var player = await client.GetPlayerInfoAsync(playerLogin);
                Console.WriteLine($"{player.NickName } gave up");
            }
        }

        public class ExampleStruct {
            public int Field1;
            public double Field2;
            public string Field3;
            public bool Field4;
            public bool Field5;
            public Base64 Field6;
            public DateTime Field7;
            public int[] Field8;
            public ExampleSubStruct Field9;

            public class ExampleSubStruct {
                public int Field1;
                public int Field2;
                public int Field3;
                public ExampleSubSubStruct Field4;
                public string[] Field5;

                public class ExampleSubSubStruct {
                    public string Field1;
                    public string Field2;
                    public string Field3;
                }
            }
        }

        static void Main(string[] args) {
            // MainAsync(args).GetAwaiter().GetResult();

            XmlRpcStruct str = new XmlRpcStruct(new Struct() {
                { "Field1", new XmlRpcInteger(3425) },
                { "Field2", new XmlRpcDouble(325.235) },
                { "Field3", new XmlRpcString("Test String") },
                { "Field4", new XmlRpcBoolean(true) },
                { "Field5", new XmlRpcBoolean(false) },
                { "Field6", new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")) },
                { "Field7", new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00")) },
                { "Field8", new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcInteger(3)
                }) },
                { "Field9", new XmlRpcStruct(new Struct(){
                    { "Field1", new XmlRpcInteger(1) },
                    { "Field2", new XmlRpcInteger(2) },
                    { "Field3", new XmlRpcInteger(3) },
                    { "Field4", new XmlRpcStruct(new Struct(){
                        { "Field1", new XmlRpcString("Test String 1") },
                        { "Field2", new XmlRpcString("Test String 2") },
                        { "Field3", new XmlRpcString("Test String 3") }
                    }) },
                    { "Field5", new XmlRpcArray(new XmlRpcBaseType[]{
                        new XmlRpcString("Test Array String 1"),
                        new XmlRpcString("Test Array String 2"),
                        new XmlRpcString("Test Array String 3")
                    }) }
                }) },
            });

            ExampleStruct result = (ExampleStruct)XmlRpcTypes.ToNativeStruct<ExampleStruct>(str);
            Console.WriteLine();
        }
    }
}
