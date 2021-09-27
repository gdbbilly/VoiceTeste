using System;
using System.Collections.Generic;
using System.Linq;

namespace Voice.Picking.Test.Pages.Extensions
{
    public static class Extensions
    {
        public static string NumberInString(this string text)
        {
            string ret = string.Empty;
            var chars = text.ToCharArray().Select(x => char.IsNumber(x) ? x : ' ').ToList();
            foreach (var item in chars)
            {
                if (item != ' ')
                {
                    ret += item.ToString();
                }
            }
            return ret;
        }

        public static int FindFirstNumeric(this List<string> list)
        {
            string number = list.FirstOrDefault(x => OnlyNumbers(x));

            if (!string.IsNullOrEmpty(number) && number.Contains(":00"))
            {
                number = number.Replace(":00", "");
            }
            return Convert.ToInt32(number);
        }

        public static string FindFirstNumericString(this List<string> list)
        {
            string number = list.FirstOrDefault(x => OnlyNumbers(x));
            if (string.IsNullOrEmpty(number))
            {
                number = list.FirstOrDefault(x => OnlyTextToNumbers(x));
                if (string.IsNullOrEmpty(number))
                    return string.Empty;
            }

            return number.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","");
        }

        public static string FindFirstNumericFormatString(this List<string> list)
        {
            string number = list.FirstOrDefault(x => OnlyNumbers(x));
            if (number == null) { return string.Empty; }
            string ret = string.Empty;
            foreach (var item in number.ToCharArray())
            {
                ret += $" {item},";
            }

            return ret;
        }

        public static string FindFirstNumericFormatString(this string text)
        {
            if (text == null) { return string.Empty; }
            string ret = string.Empty;
            foreach (var item in text.ToCharArray())
            {
                ret += $" {item},";
            }

            return ret;
        }

        private static bool OnlyNumbers(string text)
        {
            if (text.ToCharArray().Where(c => char.IsLetter(c)).Count() > 0)
            {
                return false;
            }
            else
            {
                if (text.Contains(":00"))
                {
                    text = text.Replace(":00", "");

                    return text.ToCharArray().Where(c => char.IsNumber(c)).Count() > 0;
                }
                
                return text.ToCharArray().Where(c => char.IsNumber(c)).Count() > 0;
            }
        }

        private static bool OnlyTextToNumbers(string text)
        {
            text = ConvertTextNumberToNumber(text);

            if (text.ToCharArray().Where(c => char.IsLetter(c)).Count() > 0)
                return false;
            else
            {
                return text.ToCharArray().Where(c => char.IsNumber(c)).Count() > 0;
            }
        }

        private static string ConvertTextNumberToNumber(string text)
        {
            text = text.Replace("um ", "1");
            text = text.Replace("dois ", "2");
            text = text.Replace("três ", "3");
            text = text.Replace("quatro ", "4");
            text = text.Replace("cinco ", "5");
            text = text.Replace("seis ", "6");
            text = text.Replace("sete ", "7");
            text = text.Replace("oito ", "8");
            text = text.Replace("nove ", "9");
            text = text.Replace("zero ", "0");

            return text;
        }
    }
}
