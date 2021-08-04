using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GbxRemoteNet.Utils {
    public static class FormattingExtensions {
        public static string CleanTMFormatting(this string str) {
            return Regex.Replace(str, @"\$(L\[.+\]|[0-9a-fA-F]{3}|[\w\$]{1})", "", RegexOptions.IgnoreCase);
        }
    }
}
