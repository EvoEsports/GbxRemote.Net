using GbxRemoteNet;
using GbxRemoteNet.Structs;
using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicExample {
    class Program {
        static async Task MainAsync(string[] args) {
            GbxRemoteClient client = new GbxRemoteClient("127.0.0.1", 5001);

            if (!await client.LoginAsync("SuperAdmin", "SuperAdmin")) {
                Console.WriteLine("Failed to login.");
                return;
            }

            Console.WriteLine("Connected and authenticated!");

            // var ret = await client.CallAsync("GetChatLines");
            var ret = await client.GetChatLinesAsync();

            /* string[][] methods = await client.SystemMethodSignatureAsync("CallVoteEx");
            foreach (var method in methods[0]) {
                Console.WriteLine(method);
            } */

            await client.DisconnectAsync();
        }

        static void Main(string[] args) {
            MainAsync(args).GetAwaiter().GetResult();
            /* remote.Call("someMethod", "arg1", "arg2", 3, new { 
                Four = 4,
                Five = "five",
                Six = new {
                    seven = 7
                }
            }, new object[] { 1,2,3, "test"}); */

            /* remote.Call("someMethod", 
                new XmlRpcString("arg1"),
                new XmlRpcString("arg2"),
                new XmlRpcInteger(3),
                new XmlRpcStruct(new Struct() {
                    { "Four", new XmlRpcInteger(4) },
                    { "Five", new XmlRpcString("five") },
                    { 
                        "Six", new XmlRpcStruct(new Struct(){
                            { "seven", new XmlRpcInteger(7) }
                        })
                    },
                }),
                new XmlRpcArray(new XmlRpcBaseType[] {
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcInteger(3),
                    new XmlRpcString("test")
                })
            ); */

            /* XmlRpcArray arr = new(new XmlRpcBaseType[] {
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcInteger(3),
                new XmlRpcString("test"),
            });

            XmlRpcStruct stru = new(new Struct() {
                { "one", new XmlRpcInteger(1) },
                { "two", new XmlRpcInteger(2) },
                { "three", new XmlRpcInteger(3) },
                { "four", new XmlRpcString("4 four yes") }
            });

            XElement el = stru.GetXml();
            XmlRpcStruct value = (XmlRpcStruct)XmlRpcTypes.ElementToInstance(el);
             
            foreach (var kv in value.Fields) {
                if (kv.Value.GetType() == typeof(XmlRpcInteger)) {
                    Console.WriteLine($"{kv.Key}:{((XmlRpcInteger)kv.Value).Value}");
                } else if (kv.Value.GetType() == typeof(XmlRpcString)) {
                    Console.WriteLine($"{kv.Key}:{((XmlRpcString)kv.Value).Value}");
                }
            } */

            

            /* NadeoXmlRpcClient xmlrpc = new NadeoXmlRpcClient("127.0.0.1", 5001);
            xmlrpc.ConnectAsync().GetAwaiter().GetResult();

            Message msg = xmlrpc.CallAsync("Authenticate", new XmlRpcString("SuperAdmin"), new XmlRpcString("SuperAdmin")).GetAwaiter().GetResult();

            var data = msg.GetResponseData();

            Console.WriteLine(((XmlRpcBoolean)data).Value);

            // xmlrpc.Disconnect().GetAwaiter().GetResult();
            Task.Delay(-1).GetAwaiter().GetResult(); */

            /* XmlRpcCall call = new XmlRpcCall("someMethod", "hello", "there", 1234);
            Console.WriteLine(call.GenerateXML()); */

            /* XDocument xml = new XDocument(new XDeclaration("1.0", null, null), 
                new XElement(XmlRpcTypes.Elements.MethodCall,
                    new XElement(XmlRpcTypes.Elements.MethodName, "Authenticate"),
                    new XElement(XmlRpcTypes.Elements.Params,
                        new XElement(XmlRpcTypes.Elements.Param,
                            XmlRpcTypes.ToValue("SuperAdmin")
                        ),
                        new XElement(XmlRpcTypes.Elements.Param,
                            XmlRpcTypes.ToValue(12345)
                        )
                    )
                )
            );

            var wr = new StringWriter();
            xml.Save(wr);

            Console.WriteLine(wr); */

            /* XmlRpcArray arr = new XmlRpcArray(new XmlRpcBaseType[] {
                new XmlRpcString("one"),
                new XmlRpcString("two"),
                new XmlRpcInteger(3),
                new XmlRpcInteger(4),
                new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcString("five"),
                    new XmlRpcString("six"),
                    new XmlRpcInteger(7),
                    new XmlRpcInteger(8),
                })
            });

            XmlRpcStruct stru = new XmlRpcStruct(new Dictionary<string, XmlRpcBaseType>() {
                { "something", new XmlRpcString("<halo") }
            });

            XDocument xml = new XDocument(new XDeclaration("1.0", null, null));
            xml.Add(stru.GetXml());
            var wr = new StringWriter();
            xml.Save(wr);
            Console.WriteLine(wr); */
        }
    }
}
