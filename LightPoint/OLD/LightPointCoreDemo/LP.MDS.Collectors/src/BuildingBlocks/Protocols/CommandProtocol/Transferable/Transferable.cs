using System;
using System.Collections.Generic;
using System.Text;

namespace CommandProtocol.Transferable
{
    public interface ITransferable 
    {
    }

    public class OutgoingMessage<R> where R : ITransferable
    {
        public string version { get; set; }
        public string requestor { get; set; }
        public string timestamp { get; set; }
        public string datasource { get; set; }
        public string correlationId { get; set; }
        public string userId { get; set; }
        public R values { get; set; }

    }

    public class ReferenceData : ITransferable
    {
        public List<RefData> refData{ get; set; }
       
    }

    public class RefData : ITransferable { 
        
        public SecurityDefinition security { get; set; }
        
        public Dictionary<string, RefDataDescriptor> fieldValues { get; set; }

        public Dictionary<String, String> referenceDataErrors { get; set; }

    }

    public class RefDataDescriptor : ITransferable
    {
        public String value { get; set;}
        public bool hasError { get; set; }
        public String timestamp { get; set; }

        public String originatingSource { get; set; }

        public String message { get; set; }

        private String collectorCode{ get; set; }
    }


    public class SecurityDefinition
    {
        public string securityIdentifier { get; set; }
        public string identifierType { get; set; }
        public string message { get; set; }
        public string lastUpdate { get; set; }
    }
}
