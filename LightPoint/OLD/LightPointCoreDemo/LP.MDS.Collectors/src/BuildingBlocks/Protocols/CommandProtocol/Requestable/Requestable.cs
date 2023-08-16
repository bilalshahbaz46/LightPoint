using System;
using System.Collections.Generic;
using System.Text;

namespace CommandProtocol.Requestable
{
    public interface IRequestable
    {

    }

    public class SecurityDefinition
    {
        public string securityIdentifier { get; set; }
        public string identifierType { get; set; }
        public string message { get; set; }
        public string lastUpdate { get; set; }
    }

   

    public class ReferenceDataRequest : IRequestable
    {
        public List<SecurityDefinition> securities { get; set; }
        public List<string> fields { get; set; }
    }

    public class SubscriptionRequest : IRequestable
    {
        public List<SecurityDefinition> securities { get; set; }
        public int rate { get; set; }
        public List<string> fields { get; set; }
        public string options { get; set; }
    }

    public class IncomingRequest<R>
        where R : IRequestable
    {
        public string version { get; set; }
        public string requestor { get; set; }
        public string timestamp { get; set; }
        public string datasource { get; set; }
        public string correlationId { get; set; }
        public string userId { get; set; }
        public R values { get; set; }
    }
}
