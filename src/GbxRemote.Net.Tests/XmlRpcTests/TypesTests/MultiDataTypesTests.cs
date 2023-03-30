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
    public class MultiDataTypesTests {
        #region Arrays
        [Fact]
        public void XmlRpcArray_Correctly_Parses_XElement_Array_Of_Different_Types() {
            string arrayXml = @"<value>
    <data>
        <value><i4>1</i4></value>
        <value><int>2</int></value>
        <value><string>Test String</string></value>
        <value><boolean>0</boolean></value>
        <value><i4>-1</i4></value>
        <value><double>64.23</double></value>
        <value><dateTime.iso8601>2021-04-06T16:36:44.1557489+02:00</dateTime.iso8601></value>
        <value><base64>VGVzdCBCYXNlNjQgU3RyaW5n</base64></value>
    </data>
</value>";
            XElement arrayElement = XElement.Parse(arrayXml);

            XmlRpcArray array = new(arrayElement);
            XmlRpcInteger entry1 = (XmlRpcInteger)array.Values[0];
            XmlRpcInteger entry2 = (XmlRpcInteger)array.Values[1];
            XmlRpcString entry3 = (XmlRpcString)array.Values[2];
            XmlRpcBoolean entry4 = (XmlRpcBoolean)array.Values[3];
            XmlRpcInteger entry5 = (XmlRpcInteger)array.Values[4];
            XmlRpcDouble entry6 = (XmlRpcDouble)array.Values[5];
            XmlRpcDateTime entry7 = (XmlRpcDateTime)array.Values[6];
            GbxRemoteNet.XmlRpc.Types.XmlRpcBase64 entry8 = (GbxRemoteNet.XmlRpc.Types.XmlRpcBase64)array.Values[7];

            DateTime expectedDateTime = DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime();
            GbxBase64 expectedBase64 = GbxBase64.FromBase64String("VGVzdCBCYXNlNjQgU3RyaW5n");

            Assert.Equal(1, entry1.Value);
            Assert.Equal(2, entry2.Value);
            Assert.Equal("Test String", entry3.Value);
            Assert.False(entry4.Value);
            Assert.Equal(-1, entry5.Value);
            Assert.Equal(64.23, entry6.Value);
            Assert.Equal(expectedDateTime, entry7.Value);
            Assert.Equal(expectedBase64.Data, entry8.Value.Data);
        }

        [Fact]
        public void XmlRpcArray_Corrected_Parses_2D_Array() {
            string arrayXml = @"<value>
    <data>
        <value>
            <array>
                <data>
                    <value><i4>1</i4></value>
                    <value><i4>2</i4></value>
                    <value><i4>3</i4></value>
                </data>
            </array>
        </value>
        <value>
            <array>
                <data>
                    <value><i4>4</i4></value>
                    <value><i4>5</i4></value>
                    <value><i4>6</i4></value>
                </data>
            </array>
        </value>
        <value>
            <array>
                <data>
                    <value><i4>7</i4></value>
                    <value><i4>8</i4></value>
                    <value><i4>9</i4></value>
                </data>
            </array>
        </value>
    </data>
</value>";

            XElement arrayElement = XElement.Parse(arrayXml);
            XmlRpcArray array = new(arrayElement);

            int entry1_1 = ((XmlRpcInteger)((XmlRpcArray)array.Values[0]).Values[0]).Value;
            int entry1_2 = ((XmlRpcInteger)((XmlRpcArray)array.Values[0]).Values[1]).Value;
            int entry1_3 = ((XmlRpcInteger)((XmlRpcArray)array.Values[0]).Values[2]).Value;

            int entry2_1 = ((XmlRpcInteger)((XmlRpcArray)array.Values[1]).Values[0]).Value;
            int entry2_2 = ((XmlRpcInteger)((XmlRpcArray)array.Values[1]).Values[1]).Value;
            int entry2_3 = ((XmlRpcInteger)((XmlRpcArray)array.Values[1]).Values[2]).Value;

            int entry3_1 = ((XmlRpcInteger)((XmlRpcArray)array.Values[2]).Values[0]).Value;
            int entry3_2 = ((XmlRpcInteger)((XmlRpcArray)array.Values[2]).Values[1]).Value;
            int entry3_3 = ((XmlRpcInteger)((XmlRpcArray)array.Values[2]).Values[2]).Value;

            Assert.Equal(1, entry1_1);
            Assert.Equal(2, entry1_2);
            Assert.Equal(3, entry1_3);

            Assert.Equal(4, entry2_1);
            Assert.Equal(5, entry2_2);
            Assert.Equal(6, entry2_3);

            Assert.Equal(7, entry3_1);
            Assert.Equal(8, entry3_2);
            Assert.Equal(9, entry3_3);
        }
        #endregion

        #region Structs
        [Fact]
        public void XmlRpcStruct_Parses_Correct_Key_Names() {
            string structxml = @"<struct>
    <member>
        <name>Key1</name>
        <value><string></string></value>
    </member>
    <member>
        <name>Key2</name>
        <value><string></string></value>
    </member>
    <member>
        <name>Key3</name>
        <value><string></string></value>
    </member>
</struct>";
            XElement structElement = XElement.Parse(structxml);
            XmlRpcStruct strct = new(structElement);

            Assert.Contains("Key1", strct.Fields.Keys);
            Assert.Contains("Key2", strct.Fields.Keys);
            Assert.Contains("Key3", strct.Fields.Keys);
        }

        [Fact]
        public void XmlRpcStruct_Parses_Various_XML_Struct_Values_Correctly() {
            string structxml = @"<struct>
    <member>
        <name>Key1</name>
        <value><i4>1</i4></value>
    </member>
    <member>
        <name>Key2</name>
        <value><int>-2</int></value>
    </member>
    <member>
        <name>Key3</name>
        <value><string>Test String</string></value>
    </member>
    <member>
        <name>Key4</name>
        <value><boolean>0</boolean></value>
    </member>
    <member>
        <name>Key5</name>
        <value><double>64.23</double></value>
    </member>
    <member>
        <name>Key6</name>
        <value><base64>VGVzdCBCYXNlNjQgU3RyaW5n</base64></value>
    </member>
    <member>
        <name>Key7</name>
        <value><dateTime.iso8601>2021-04-06T16:36:44.1557489+02:00</dateTime.iso8601></value>
    </member>
    <member>
        <name>Key8</name>
        <value>        
            <array>
                <data>
                    <value><i4>1</i4></value>
                    <value><i4>2</i4></value>
                    <value><i4>3</i4></value>
                </data>
            </array>
        </value>
    </member>
</struct>";
            XElement structElement = XElement.Parse(structxml);
            XmlRpcStruct strct = new(structElement);

            int value1 = ((XmlRpcInteger)strct.Fields["Key1"]).Value;
            int value2 = ((XmlRpcInteger)strct.Fields["Key2"]).Value;
            string value3 = ((XmlRpcString)strct.Fields["Key3"]).Value;
            bool value4 = ((XmlRpcBoolean)strct.Fields["Key4"]).Value;
            double value5 = ((XmlRpcDouble)strct.Fields["Key5"]).Value;
            GbxBase64 value6 = ((GbxRemoteNet.XmlRpc.Types.XmlRpcBase64)strct.Fields["Key6"]).Value;
            DateTime value7 = ((XmlRpcDateTime)strct.Fields["Key7"]).Value;
            int value8 = ((XmlRpcInteger)((XmlRpcArray)strct.Fields["Key8"]).Values[0]).Value;
            int value9 = ((XmlRpcInteger)((XmlRpcArray)strct.Fields["Key8"]).Values[1]).Value;
            int value10 = ((XmlRpcInteger)((XmlRpcArray)strct.Fields["Key8"]).Values[2]).Value;

            byte[] expectedValue6 = GbxBase64.FromBase64String("VGVzdCBCYXNlNjQgU3RyaW5n").Data;
            DateTime expectedValue7 = DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime();

            Assert.Equal(1, value1);
            Assert.Equal(-2, value2);
            Assert.Equal("Test String", value3);
            Assert.False(value4);
            Assert.Equal(64.23, value5);
            Assert.Equal(expectedValue6, value6.Data);
            Assert.Equal(expectedValue7, value7);
            Assert.Equal(1, value8);
            Assert.Equal(2, value9);
            Assert.Equal(3, value10);
        }

        [Fact]
        public void XmlRpcStruct_Correctly_Parses_Struct_As_Field() {
            string structxml = @"<struct>
    <member>
        <name>TestKey</name>
        <value>
            <struct>
                <member>
                    <name>Key1</name>
                    <value><string>Test Value 1</string></value>
                </member>
                <member>
                    <name>Key2</name>
                    <value><string>Test Value 2</string></value>
                </member>
                <member>
                    <name>Key3</name>
                    <value><string>Test Value 3</string></value>
                </member>
            </struct>
        </value>
    </member>
</struct>";
            XElement structElement = XElement.Parse(structxml);
            XmlRpcStruct strct = new(structElement);

            XmlRpcStruct subStruct = (XmlRpcStruct)strct.Fields["TestKey"];

            string value1 = ((XmlRpcString)subStruct.Fields["Key1"]).Value;
            string value2 = ((XmlRpcString)subStruct.Fields["Key2"]).Value;
            string value3 = ((XmlRpcString)subStruct.Fields["Key3"]).Value;

            Assert.Equal("Test Value 1", value1);
            Assert.Equal("Test Value 2", value2);
            Assert.Equal("Test Value 3", value3);
        }
        #endregion
    }
}
