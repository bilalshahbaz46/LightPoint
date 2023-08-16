using System;

namespace StaticDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            double c = 37;
            double f = 98.6;

            Console.WriteLine("Value of {0} celcius to fahrenheit at {1} is: ", c, Convertor.ToFahrenheit(c));
            Console.WriteLine("Value of {0} fahrenheit to celcius at {1} is: ", f, Convertor.ToCelcius(f));
        }
    }
}
