using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedMethods.Extenders
{
    public static class Extender
    {
        public static string FirstCharUpper(this string nameString)
        {
            char[] charString = nameString.ToCharArray();
            charString[0] = char.IsUpper(charString[0]) ? char.ToLower(charString[0]) : char.ToUpper(charString[0]);
            return new string(charString);
        }

        public static int AddTwo(this int num)
        {
            return num + 2;
        }
    }
}
