using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace T2NSHOP.Common
{
    public class RemoveSpecialCharacter
    {
        public static string RemoveSpecialCharacters(string input)
        {
            // Sử dụng biểu thức chính quy để tìm và loại bỏ các ký tự đặc biệt
            string pattern = "[^a-zA-Z0-9]";
            string replacement = " ";
            string sanitizedInput = Regex.Replace(input, pattern, replacement);

            return sanitizedInput;
        }
    }
}