using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Data;

namespace ConvertExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Work\LightPoint\ConvertExcel\Files\FactSetBBGMapping.xlsx";

            var Data = new ExcelMapper(filePath).Fetch<MapEntry>();

            List<MapEntry> list = new List<MapEntry>();

            foreach (var item in Data)
            {
                list.Add(new MapEntry()
                {
                    Data_Type = item.Data_Type,
                    BBGMnemonicFields = item.BBGMnemonicFields,
                    FactSet = item.FactSet
                });
            }

            foreach (var row in list)
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"Datatype = {row.Data_Type}");
                Console.WriteLine($"BBGMnemonicField = {row.BBGMnemonicFields}");
                Console.WriteLine($"FactSet = {row.FactSet}");
                Console.WriteLine("--------------------------------------------------------");
            }
        }
    }

    public class MapEntry
    {
        public string Data_Type { get; set; }
        public string BBGMnemonicFields { get; set; }
        public string FactSet { get; set; }
    }
}
