using System;
using System.Collections.Generic;
using System.Text;

namespace StaticDemo
{
    public static class Convertor
    {
        public static double ToFahrenheit(double celcius)
        {
            return (celcius * 9 / 5) + 32;
        }

        public static double ToCelcius (double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }
    }
}
