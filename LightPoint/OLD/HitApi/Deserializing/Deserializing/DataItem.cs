using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deserializing
{
    public class DataItem
    {
        public DataItem(string json, string ticker)
        {
            JObject jObject = JObject.Parse(json);
            JToken jDataItem = jObject["DataItem"];
            ticket = ticker;
            fields.Add((string)jDataItem["@name"], (string)jDataItem["#text"]);
        }
        public String ticket { get; set; }
        public Dictionary<String, String> fields { get; set; }
    }
}
