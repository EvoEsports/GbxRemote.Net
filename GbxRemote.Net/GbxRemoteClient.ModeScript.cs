using GbxRemoteNet.XmlRpc.Packets;
using GbxRemoteNet.XmlRpc.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GbxRemoteNet {
    public partial class GbxRemoteClient {
        /// <summary>
        /// If true, OnModeScriptCallback is triggered in addition when the callback
        /// is a response for a ModeScript method call.
        /// </summary>
        public bool InvokeEventOnModeScriptMethodResponse { get; set; }

        private JObject ParseModeScriptCallback(MethodCall call) {
            XmlRpcArray dataArr = (XmlRpcArray)call.Arguments[1];
            XmlRpcString dataStr = (XmlRpcString)dataArr.Values[0];
            return JObject.Parse(dataStr.Value);
        }

        ConcurrentDictionary<string, JObject> msResponses = new ConcurrentDictionary<string, JObject>();
        ConcurrentDictionary<string, ManualResetEvent> msSignals= new ConcurrentDictionary<string, ManualResetEvent>();

        public delegate Task ModeScriptCallbackAction(string method, JObject data);
        /// <summary>
        /// Triggered when any ModeScript callback is sent from the server.
        /// </summary>
        public event ModeScriptCallbackAction OnModeScriptCallback;

        /// <summary>
        /// Handle modescript callbacks and parse them.
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        private Task HandleModeScriptCallback(MethodCall call) {
            JObject data = ParseModeScriptCallback(call);

            if (data.ContainsKey("responseid")) {
                string responseId = data["responseid"].Value<string>();
                if (msSignals.ContainsKey(responseId)) {
                    // we have a modescript callback response
                    msResponses[responseId] = data;
                    msSignals[responseId].Set();

                    if (!InvokeEventOnModeScriptMethodResponse)
                        return Task.CompletedTask;
                }
            }

            // invoke the generic event
            OnModeScriptCallback?.Invoke(
                ((XmlRpcString)call.Arguments[0]).Value,
                data
            );

            return Task.CompletedTask;
        }

        public async Task<JObject> GetModeScriptResponseAsync(string method, params string[] args) {
            string responseId = Guid.NewGuid().ToString();
            List<string> passArgs = new(args);
            passArgs.Add(responseId);

            // send call
            msSignals[responseId] = new(false);
            await TriggerModeScriptEventArrayAsync(method, passArgs.ToArray());

            // wait for response
            msSignals[responseId].WaitOne();
            msResponses.Remove(responseId, out JObject response);
            msSignals.Remove(responseId, out _);

            return response;
        }
    }
}
