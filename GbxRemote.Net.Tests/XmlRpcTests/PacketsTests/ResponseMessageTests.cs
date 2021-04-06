using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.PacketsTests {
    public class ResponseMessageTests : IClassFixture<MessageFixture> {
        MessageFixture fixture;

        public ResponseMessageTests(MessageFixture fixture) {
            this.fixture = fixture;
        }
    }
}
