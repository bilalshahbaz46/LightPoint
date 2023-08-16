using ExtendedMethods.DataTypes;
using ExtendedMethods.Extenders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtendedMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "bilal Shahbaz";
            Console.WriteLine(name.FirstCharUpper());

            int n = 3;
            Console.WriteLine(n.AddTwo());

            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9,10};
            //IEnumerable<int> nums = numbers.Where(x => x % 2 == 0);
            IEnumerable<int> nums = Enumerable.Where(numbers, x => x % 2 == 0);

            foreach (var item in nums)
            {
                Console.WriteLine(item);
            }



            //myDataType
            myDataType data = new myDataType() { DataString = "Billu Barbar", DataInt = 100, DataDouble = 100.1 };

            Console.WriteLine("\n");
            Console.WriteLine("Before Editing");
            Console.WriteLine($"DataString = {data.DataString} \nDataInt = {data.DataInt} \n DataDouble = {data.DataDouble}");

            Console.WriteLine("\n");
            Console.WriteLine("After String Editing");
            var result = data.ChangeTheString();
            Console.WriteLine($"DataString = {result.DataString} \nDataInt = {result.DataInt} \n DataDouble = {result.DataDouble}");

            Console.WriteLine("\n");
            Console.WriteLine("After Int Editing");
            result = data.ChangeTheInt();
            Console.WriteLine($"DataString = {result.DataString} \nDataInt = {result.DataInt} \n DataDouble = {result.DataDouble}");

            Console.WriteLine("\n");
            Console.WriteLine("After Double Editing");
            result = data.ChangeTheDouble();
            Console.WriteLine($"DataString = {result.DataString} \nDataInt = {result.DataInt} \n DataDouble = {result.DataDouble}");


        }
    }
}
