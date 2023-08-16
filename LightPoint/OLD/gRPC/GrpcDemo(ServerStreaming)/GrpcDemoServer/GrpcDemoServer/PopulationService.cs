using Grpc.Core;
using GrpcPopulation;
using System.Threading.Tasks;

namespace GrpcDemoServer
{
    public class PopulationService : PopulationProvider.PopulationProviderBase
    {
        public override Task<PopulationResponse> GetPopulation(CountryRequest request, ServerCallContext serverCallContext)
        {
            switch (request.State)
            {
                case "us":
                    return Task.FromResult(new PopulationResponse { Count = "1000"});
                case "pk":
                    return Task.FromResult(new PopulationResponse { Count = "2000" });
                case "om":
                    return Task.FromResult(new PopulationResponse { Count = "3000" });
                default:
                    return Task.FromResult(new PopulationResponse { Count = "The Country code was not Found!" });

            }
        }

        public override Task<NameResponse> GetFullName(CountryRequest request, ServerCallContext serverCallContext)
        {
            switch (request.State)
            {
                case "us":
                    return Task.FromResult(new NameResponse { Name = "United States of America" });
                case "pk":
                    return Task.FromResult(new NameResponse { Name = "Pakistan" });
                case "om":
                    return Task.FromResult(new NameResponse { Name = "Oman" });
                default:
                    return Task.FromResult(new NameResponse { Name = "The Country code was not Found!" });

            }
        }
    }
}
