using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelFileRead
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public List<T> ImportExcel<T>(string excelFilePath, string sheetName)
        {
            List<T> list = new List<T>();
            Type type

            using (IXLWorkbook workbook = new XLWorkbook(excelFilePath))
            {
                var worksheet = workbook.Worksheets.Where(x => x.Name == sheetName).First();
                var properties = 
            }
        }
    }

    public class DataRow
    {
        public string Data_Type { get; set; }
        public string BBG_Mnemonic_Fields { get; set; }
        public string FactSet { get; set; }
        public string Mapping_Confidence { get; set; }
        public string BasicPackage { get; set; }
        public string MarketDataApi { get; set; }
    }
}
