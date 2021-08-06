using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GbxRemoteNet.Utils {
    public static class Formatting {
        const string HexCharset = "0123456789ABCDEF";
        static Regex formatCleaner = new Regex(@"\$((L|H)\[.+\]|[\da-f]{3}|[\w\$\<\>]{1})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string CleanTMFormatting(this string str) {
            return formatCleaner.Replace(str, "");
        }

        public static string TmColor(byte r, byte g, byte b) {
            int rc = (int)Math.Round((float)r/0xff*0xf);
            int gc = (int)Math.Round((float)g/0xff*0xf);
            int bc = (int)Math.Round((float)b/0xff*0xf);

            return "$" + HexCharset[rc] + HexCharset[gc] + HexCharset[bc];
        }
    }
}
