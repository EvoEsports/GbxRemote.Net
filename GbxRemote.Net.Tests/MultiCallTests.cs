using GbxRemoteNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests {
    public class MultiCallTests {
        public Task<bool> TestMethod() => Task.FromResult(true);

        [Fact]
        public void Correctly_Add_Method_From_Native_Method() {
            MultiCall mc = new();
            mc.Add(TestMethod);

            string methodName = mc.MethodCalls[0].MethodName;

            Assert.Equal("TestMethod", methodName);
        }

        [Fact]
        public void Correctly_Add_Method_From_NameOf() {
            MultiCall mc = new();
            mc.Add(nameof(TestMethod));

            string methodName = mc.MethodCalls[0].MethodName;

            Assert.Equal("TestMethod", methodName);
        }

        [Fact]
        public void Correctly_Add_Method_From_String() {
            MultiCall mc = new();
            mc.Add("TestMethod");

            string methodName = mc.MethodCalls[0].MethodName;

            Assert.Equal("TestMethod", methodName);
        }

        [Fact]
        public void Correctly_Adds_Arguments_For_Method() {
            MultiCall mc = new();
            mc.Add("TestMethod", 1, "2", 3.14);

            int arg1 = (int)mc.MethodCalls[0].Arguments[0];
            string arg2 = (string)mc.MethodCalls[0].Arguments[1];
            double arg3 = (double)mc.MethodCalls[0].Arguments[2];

            Assert.Equal(1, arg1);
            Assert.Equal("2", arg2);
            Assert.Equal(3.14, arg3);
        }
    }
}
