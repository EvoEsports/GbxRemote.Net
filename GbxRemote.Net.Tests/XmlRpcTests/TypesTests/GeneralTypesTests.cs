using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.TypesTests {
    public class GeneralTypesTests {
        [Theory]
        [InlineData("VGVzdCBTdHJpbmc=", "<base64>VGVzdCBTdHJpbmc=</base64>")]
        [InlineData("", "<base64></base64>")]
        public void XmlRpcBase64_GetXml_Returns_Correct_Element(string input, string expected) {
            XmlRpcBase64 base64 = new(Base64.FromBase64String(input));

            string xml = base64.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Theory]
        [InlineData("0", false)]
        [InlineData("1", true)]
        [InlineData("false", false)]
        [InlineData("true", true)]
        [InlineData("False", false)]
        [InlineData("True", true)]
        public void XmlRpcBoolean_Correctly_Parses_XElement(string input, bool expected) {
            XElement element = new("value", input);

            XmlRpcBoolean boolean = new(element);

            Assert.Equal(expected, boolean.Value);
        }

        [Theory]
        [InlineData(true, "<boolean>1</boolean>")]
        [InlineData(false, "<boolean>0</boolean>")]
        public void XmlRpcBoolean_GetXml_Returns_Correct_Element(bool value, string expected) {
            XmlRpcBoolean boolean = new(value);

            string xml = boolean.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Theory]
        [InlineData("2021-04-06T16:36:44.1557489+02:00", "<dateTime.iso8601>2021-04-06T16:36:44.1557489+02:00</dateTime.iso8601>")]
        public void XmlRpcDateTime_GetXml_Returns_Correct_Element(string dtstring, string expected) {
            XmlRpcDateTime datetime = new(DateTime.Parse(dtstring));

            string xml = datetime.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Fact]
        public void XmlRpcDateTime_Correctly_Parses_XElement() {
            XElement element = new("value", "2021-04-06T16:36:44.1557489+02:00");
            DateTime expected = DateTime.Parse("2021-04-06T16:36:44.1557489+02:00");

            XmlRpcDateTime datetime = new(element);

            Assert.Equal(expected, datetime.Value);
        }

        [Theory]
        [InlineData("0", (double)0)]
        [InlineData("1", (double)1)]
        [InlineData("234.652", 234.652)]
        [InlineData("234", (double)234)]
        [InlineData("-234", (double)-234)]
        [InlineData("0.1", .1)]
        public void XmlRpcDouble_Correctly_Parses_XElement(string input, double expected) {
            XElement element = new("value", input);

            XmlRpcDouble xmlrpcDouble = new(element);

            Assert.Equal(expected, xmlrpcDouble.Value);
        }

        [Theory]
        [InlineData(234.652, "<double>234.652</double>")]
        [InlineData((double)234, "<double>234</double>")]
        [InlineData((double)-234, "<double>-234</double>")]
        [InlineData(.1, "<double>0.1</double>")]
        [InlineData(0.0, "<double>0</double>")]
        public void XmlRpcDouble_GetXml_Returns_Correct_Element(double value, string expected) {
            XmlRpcDouble xmlrpcDouble = new(value);

            string xml = xmlrpcDouble.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("-1", -1)]
        [InlineData("14365146", 14365146)]
        [InlineData("-14365146", -14365146)]
        public void XmlRpcInteger_Correctly_Parses_XElement(string input, int expected) {
            XElement element = new("value", input);

            XmlRpcInteger integer = new(element);

            Assert.Equal(expected, integer.Value);
        }

        [Theory]
        [InlineData(0, "<int>0</int>")]
        [InlineData(1, "<int>1</int>")]
        [InlineData(-1, "<int>-1</int>")]
        [InlineData(14365146, "<int>14365146</int>")]
        [InlineData(-14365146, "<int>-14365146</int>")]
        public void XmlRpcInteger_GetXml_Returns_Correct_Element(int value, string expected) {
            XmlRpcInteger integer = new(value);

            string xml = integer.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("test", "test")]
        [InlineData(" t e s t ", " t e s t ")]
        public void XmlRpcString_Correctly_Parses_XElement(string input, string expected) {
            XElement element = new("value", input);

            XmlRpcString str = new(element);

            Assert.Equal(expected, str.Value);
        }

        [Theory]
        [InlineData("", "<string></string>")]
        [InlineData("test", "<string>test</string>")]
        [InlineData(" t e s t ", "<string> t e s t </string>")]
        public void XmlRpcString_GetXml_Returns_Correct_Element(string value, string expected) {
            XmlRpcString str = new(value);

            string xml = str.GetXml().ToString();

            Assert.Equal(expected, xml);
        }

        [Fact]
        public void XmlRpcFault_Correctly_Parses_XElement() {
            string faultXml = @"<struct>
    <member>
        <name>faultCode</name>
        <value><int>4</int></value>
    </member>
    <member>
        <name>faultString</name>
        <value><string>Too many parameters.</string></value>
    </member>
</struct>";
            XElement element = XElement.Parse(faultXml);

            XmlRpcFault fault = new(element);

            Assert.Equal(4, fault.FaultCode);
            Assert.Equal("Too many parameters.", fault.FaultString);
        }
    }
}
