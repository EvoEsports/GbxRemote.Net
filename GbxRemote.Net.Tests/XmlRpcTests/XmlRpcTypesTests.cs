using GbxRemoteNet.XmlRpc;
using GbxRemoteNet.XmlRpc.ExtraTypes;
using GbxRemoteNet.XmlRpc.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests {
    public class XmlRpcTypesTests {
        [Fact]
        public void ElementToInstance_Throws_On_Invalid_XMLRPC_Element() {
            XElement element = new("InvalidElement");

            Assert.Throws<InvalidDataException>(() => XmlRpcTypes.ElementToInstance(element));
        }

        public static IEnumerable<object[]> ElementToInstanceData => new List<object[]> {
            new object[]{ new XElement("i4", "21"), new XmlRpcInteger(21) },
            new object[]{ new XElement("int", "3465"), new XmlRpcInteger(3465) },
            new object[]{ new XElement("string", "Test String"), new XmlRpcString("Test String") },
            new object[]{ new XElement("boolean", "1"), new XmlRpcBoolean(true) },
            new object[]{ new XElement("boolean", "0"), new XmlRpcBoolean(false) },
            new object[]{ new XElement("double", "34534.1234"), new XmlRpcDouble(34534.1234) },
            new object[]{ new XElement("dateTime.iso8601", "2021-04-06T14:36:44.1557489Z"), new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime()) },
            new object[]{ new XElement("base64", "VGVzdCBTdHJpbmc="), new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")) },
            new object[]{ new XElement("array", new XElement("data",
                    new XElement("value", new XElement("i4", 1)),
                    new XElement("value", new XElement("int", 2)),
                    new XElement("value", new XElement("string", "3")),
                    new XElement("value", new XElement("double", 4))
                )), new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcString("3"),
                    new XmlRpcDouble(4),
                })},
            new object[] { new XElement("struct",
                new XElement("member",
                        new XElement("name", "Key1"),
                        new XElement("value", new XElement("i4", 1))
                ),
                new XElement("member",
                        new XElement("name", "Key2"),
                        new XElement("value", new XElement("int", 2))
                ),
                new XElement("member",
                        new XElement("name", "Key3"),
                        new XElement("value", new XElement("string", "3"))
                ),
                new XElement("member",
                        new XElement("name", "Key4"),
                        new XElement("value", new XElement("double", 4))
                )
                ), new XmlRpcStruct(new Struct() {
                    { "Key1", new XmlRpcInteger(1) },
                    { "Key2", new XmlRpcInteger(2) },
                    { "Key3", new XmlRpcString("3") },
                    { "Key4", new XmlRpcDouble(4) }
                })}
        };
        [Theory]
        [MemberData(nameof(ElementToInstanceData))]
        public void ElementToInstance_Converts_Types_Correctly(XElement element, object expected) {
            XmlRpcBaseType result = XmlRpcTypes.ElementToInstance(element);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void ToNativeValue_Returns_Null_If_Null() {
            var result = XmlRpcTypes.ToNativeValue<object>(null);

            Assert.Null(result);
        }

        class ToNativeValue_NonExistentElement : XmlRpcBaseType {
            public ToNativeValue_NonExistentElement() : base(null) { }

            public override XElement GetXml() {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void ToNativeValue_Returns_Null_If_NonExistent_Element() {
            ToNativeValue_NonExistentElement element = new();
            var result = XmlRpcTypes.ToNativeValue<object>(element);

            Assert.Null(result);
        }

        public static IEnumerable<object[]> ToNativeValueData => new List<object[]> {
            new object[]{ new XmlRpcInteger(3462), 3462 },
            new object[]{ new XmlRpcDouble(345.2), 345.2 },
            new object[]{ new XmlRpcBoolean(false), false },
            new object[]{ new XmlRpcBoolean(true), true },
            new object[]{ new XmlRpcString("Test String"), "Test String" },
            new object[]{ new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")), Base64.FromBase64String("VGVzdCBTdHJpbmc=") },
            new object[]{ new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime()), DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime() },
            new object[]{ new XmlRpcArray(new XmlRpcBaseType[] {
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcString("3"),
                new XmlRpcDouble(4),
            }), new object[] { 1, 2, "3", (double)4 } },
            new object[]{ new XmlRpcStruct(new Struct() {
                { "Key1", new XmlRpcInteger(1) },
                { "Key2", new XmlRpcInteger(2) },
                { "Key3", new XmlRpcString("3") },
                { "Key4", new XmlRpcDouble(4) }
            }), new DynamicObject(){
                { "Key1", 1 },
                { "Key2", 2 },
                { "Key3", "3" },
                { "Key4", (double)4 }
            }}
        };
        [Theory]
        [MemberData(nameof(ToNativeValueData))]
        public void ToNativeValue_Returns_Correct_Type_For_Various_Basic_XmlRpcTypes(XmlRpcBaseType element, object expected) {
            object result = XmlRpcTypes.ToNativeValue<object>(element);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToNativeStruct_Returns_DynamicObject_If_Object_Type() {
            XmlRpcStruct str = new(new Struct());
            object result = XmlRpcTypes.ToNativeStruct<object>(str);

            Assert.IsType<DynamicObject>(result);
        }

        [Fact]
        public void ToNativeStruct_Returns_DynamicObject_If_DynamicObject_Type() {
            XmlRpcStruct str = new(new Struct());
            object result = XmlRpcTypes.ToNativeStruct<DynamicObject>(str);

            Assert.IsType<DynamicObject>(result);
        }

        public class ExampleStruct {
            public int Field1 { get; set; }
            public double Field2 { get; set; }
            public string Field3 { get; set; }
            public bool Field4 { get; set; }
            public bool Field5 { get; set; }
            public Base64 Field6 { get; set; }
            public DateTime Field7 { get; set; }
            public int[] Field8 { get; set; }
            public ExampleSubStruct Field9 { get; set; }

            public class ExampleSubStruct {
                public int Field1 { get; set; }
                public int Field2 { get; set; }
                public int Field3 { get; set; }
            }
        }
        [Fact]
        public void ToNativeStruct_Returns_Correct_Values_In_Custom_Struct() {
            XmlRpcStruct str = new XmlRpcStruct(new Struct() {
                { "Field1", new XmlRpcInteger(3425) },
                { "Field2", new XmlRpcDouble(325.235) },
                { "Field3", new XmlRpcString("Test String") },
                { "Field4", new XmlRpcBoolean(true) },
                { "Field5", new XmlRpcBoolean(false) },
                { "Field6", new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")) },
                { "Field7", new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime()) },
                { "Field8", new XmlRpcArray(new XmlRpcBaseType[]{ 
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcInteger(3)
                }) },
                { "Field9", new XmlRpcStruct(new Struct(){
                    { "Field1", new XmlRpcInteger(1) },
                    { "Field2", new XmlRpcInteger(2) },
                    { "Field3", new XmlRpcInteger(3) }
                }) },
            });

            ExampleStruct result = (ExampleStruct)XmlRpcTypes.ToNativeStruct<ExampleStruct>(str);

            Base64 field6Expected = Base64.FromBase64String("VGVzdCBTdHJpbmc=");
            DateTime field7Expected = DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime();

            Assert.NotNull(result);
            Assert.Equal(3425, result.Field1);
            Assert.Equal(325.235, result.Field2);
            Assert.Equal("Test String", result.Field3);
            Assert.True(result.Field4);
            Assert.False(result.Field5);
            Assert.Equal(field6Expected, result.Field6);
            Assert.Equal(field7Expected, result.Field7);
            Assert.Equal(new int[] { 1, 2, 3 }, result.Field8);
            Assert.NotNull(result.Field9);
            Assert.Equal(1, result.Field9.Field1);
            Assert.Equal(2, result.Field9.Field2);
            Assert.Equal(3, result.Field9.Field3);
        }

        [Fact]
        public void ToNativeArray_Returns_Integer_Array_With_Integer_XmlRpcArray_Array() {
            XmlRpcArray arr = new(new XmlRpcBaseType[]{
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcInteger(3)
            });

            int[] result = XmlRpcTypes.ToNativeArray<int>(arr);

            Assert.Equal(new int[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void ToNative2DArray_Returns_Correct_2D_Integer_Array() {
            XmlRpcArray arr = new(new XmlRpcBaseType[] {
                new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(1),
                    new XmlRpcInteger(2),
                    new XmlRpcInteger(3)
                }),
                new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(4),
                    new XmlRpcInteger(5),
                    new XmlRpcInteger(6)
                }),
                new XmlRpcArray(new XmlRpcBaseType[]{
                    new XmlRpcInteger(7),
                    new XmlRpcInteger(8),
                    new XmlRpcInteger(9)
                })
            });

            int[][] result = XmlRpcTypes.ToNative2DArray<int>(arr);

            Assert.Equal(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5, 6 },
                new int[]{ 7, 8, 9 }
            }, result);
        }

        class ToXmlRpcValue_ExampleStruct {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
            public int Field3 { get; set; }
        }
        public static IEnumerable<object[]> ToXmlRpcValueData => new List<object[]> {
            new object[]{ 34612, new XmlRpcInteger(34612) },
            new object[]{ true, new XmlRpcBoolean(true) },
            new object[]{ false, new XmlRpcBoolean(false) },
            new object[]{ 734.267, new XmlRpcDouble(734.267) },
            new object[]{ DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime(), new XmlRpcDateTime(DateTime.Parse("2021-04-06T16:36:44.1557489+02:00").ToUniversalTime()) },
            new object[]{ "Test String", new XmlRpcString("Test String") },
            new object[]{ Base64.FromBase64String("VGVzdCBTdHJpbmc="), new XmlRpcBase64(Base64.FromBase64String("VGVzdCBTdHJpbmc=")) },
            new object[]{ new int[] { 1, 2, 3 }, new XmlRpcArray(new XmlRpcBaseType[] { 
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcInteger(3)
            }) },
            new object[]{ new ToXmlRpcValue_ExampleStruct{
                Field1=1,
                Field2=2,
                Field3=3
            }, new XmlRpcStruct(new Struct(){
                { "Field1", new XmlRpcInteger(1) },
                { "Field2", new XmlRpcInteger(2) },
                { "Field3", new XmlRpcInteger(3) }
            }) }
        };
        [Theory]
        [MemberData(nameof(ToXmlRpcValueData))]
        public void ToXmlRpcValue_Returns_Correct_XmlRpc_Instance_From_Various_Native_Values(object value, object expected) {
            XmlRpcBaseType xmlRpcValue = XmlRpcTypes.ToXmlRpcValue(value);

            Assert.Equal(expected, xmlRpcValue);
        }

        [Fact]
        public void ToXmlRpcArray_Returns_XmlRpcArray_of_Integers_From_Int_Array() {
            int[] arr = new int[] { 1, 2, 3 };

            XmlRpcArray result = XmlRpcTypes.ToXmlRpcArray(arr);
            XmlRpcArray expected = new XmlRpcArray(new XmlRpcBaseType[] {
                new XmlRpcInteger(1),
                new XmlRpcInteger(2),
                new XmlRpcInteger(3)
            });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DynamicObject_To_XmlRpcStruct()
        {
            var obj = new DynamicObject();
            
            obj.Add("one", 1);
            obj.Add("two", "two");
            obj.Add("three", 3.3);

            var result = XmlRpcTypes.ToXmlRpcValue(obj);
            var expected = new XmlRpcStruct(new Struct
            {
                {"one", new XmlRpcInteger(1)},
                {"two", new XmlRpcString("two")},
                {"three", new XmlRpcDouble(3.3)}
            });
            
            Assert.Equal(expected, result);
        }
    }
}
