using GbxRemoteNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GbxRemote.Net.Tests.XmlRpcTests.Utils {
    public class FormattingTests {
        [Theory]
        [InlineData("$oTest", "Test")]
        [InlineData("$OTest", "Test")]
        [InlineData("$iTest", "Test")]
        [InlineData("$ITest", "Test")]
        [InlineData("$wTest", "Test")]
        [InlineData("$WTest", "Test")]
        [InlineData("$nTest", "Test")]
        [InlineData("$NTest", "Test")]
        [InlineData("$tTest", "Test")]
        [InlineData("$TTest", "Test")]
        [InlineData("$sTest", "Test")]
        [InlineData("$STest", "Test")]
        [InlineData("$lTest", "Test")]
        [InlineData("$LTest", "Test")]
        [InlineData("$l[https://google.com]Test", "Test")]
        [InlineData("$L[https://google.com]Test", "Test")]
        [InlineData("$gTest", "Test")]
        [InlineData("$GTest", "Test")]
        [InlineData("$zTest", "Test")]
        [InlineData("$ZTest", "Test")]
        [InlineData("$$Test", "Test")]
        [InlineData("$<Test", "Test")]
        [InlineData("$>Test", "Test")]
        public void CleanTMFormatting_Cleans_Control_Chars(string input, string expected) {
            string cleaned = input.CleanTMFormatting();

            Assert.Equal(expected, cleaned);
        }

        [Theory]
        [InlineData("$000Test", "Test")]
        [InlineData("$111Test", "Test")]
        [InlineData("$222Test", "Test")]
        [InlineData("$333Test", "Test")]
        [InlineData("$444Test", "Test")]
        [InlineData("$555Test", "Test")]
        [InlineData("$666Test", "Test")]
        [InlineData("$777Test", "Test")]
        [InlineData("$888Test", "Test")]
        [InlineData("$999Test", "Test")]
        [InlineData("$AAATest", "Test")]
        [InlineData("$aaaTest", "Test")]
        [InlineData("$BBBTest", "Test")]
        [InlineData("$bbbTest", "Test")]
        [InlineData("$CCCTest", "Test")]
        [InlineData("$cccTest", "Test")]
        [InlineData("$DDDTest", "Test")]
        [InlineData("$dddTest", "Test")]
        [InlineData("$EEETest", "Test")]
        [InlineData("$eeeTest", "Test")]
        [InlineData("$FFFTest", "Test")]
        [InlineData("$fffTest", "Test")]
        public void CleanTMFormatting_Removes_Colors(string input, string expected) {
            string cleaned = input.CleanTMFormatting();

            Assert.Equal(expected, cleaned);
        }

        [Theory]
        [InlineData(0x0, 0x0, 0x0, "$000")]
        [InlineData(0x11, 0x11, 0x11, "$111")]
        [InlineData(0x22, 0x22, 0x22, "$222")]
        [InlineData(0x33, 0x33, 0x33, "$333")]
        [InlineData(0x44, 0x44, 0x44, "$444")]
        [InlineData(0x55, 0x55, 0x55, "$555")]
        [InlineData(0x66, 0x66, 0x66, "$666")]
        [InlineData(0x77, 0x77, 0x77, "$777")]
        [InlineData(0x88, 0x88, 0x88, "$888")]
        [InlineData(0x99, 0x99, 0x99, "$999")]
        [InlineData(0xaa, 0xaa, 0xaa, "$AAA")]
        [InlineData(0xbb, 0xbb, 0xbb, "$BBB")]
        [InlineData(0xcc, 0xcc, 0xcc, "$CCC")]
        [InlineData(0xdd, 0xdd, 0xdd, "$DDD")]
        [InlineData(0xee, 0xee, 0xee, "$EEE")]
        [InlineData(0xff, 0xff, 0xff, "$FFF")]
        public void Test_TmColor_Returns_Correct_Format(byte ir, byte ig, byte ib, string expected) {
            string result = Formatting.TmColor(ir, ig, ib);

            Assert.Equal(expected, result);
        }
    }
}
