using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedMethods.DataTypes
{
    public class myDataType
    {
        public string DataString { get; set; }
        public int DataInt { get; set; }
        public double DataDouble { get; set; }

        public myDataType ChangeTheString(this myDataType request)
        {
            return new myDataType() { DataString = "Nothing", DataInt = request.DataInt, DataDouble = request.DataDouble };
        }
    }
}
