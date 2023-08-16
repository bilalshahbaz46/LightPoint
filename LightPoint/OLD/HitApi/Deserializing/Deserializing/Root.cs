using System;
using System.Collections.Generic;
using System.Text;

namespace Deserializing
{
    public class ResultCount
    {
        public int bonds { get; set; }
        public int equities { get; set; }
    }

    public class Result
    {
        public string index { get; set; }
        public string symbol { get; set; }
        public string entity_id { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        public string fds_id { get; set; }
    }

    public class Typeahead
    {
        public ResultCount result_count { get; set; }
        public object error { get; set; }
        public int is_success { get; set; }
        public List<Result> results { get; set; }
    }

    public class Root
    {
        public Typeahead typeahead { get; set; }
    }
}
