using Grpc.Core;
using GrpcUserPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refresher.GrpcServices.Users
{
    public class GrpcUserService : UserPointsProvider.UserPointsProviderBase
    {
        private Context _context;

        public GrpcUserService(Context context)
        {
            _context = context;
        }
        public override Task<UserPointsResponse> GetUserPoints(UserPointsRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserPointsResponse
            {
                UserPoints = _context.Users.FirstOrDefault(x => x.Id == request.UserId).Points
            });
        }
    }
}
