using Refresher.Dtos.Users;
using System.Collections.Generic;

namespace Refresher.RepoInterfaces
{
    public interface IUserService
    {
        public void AddUser(CreateUserDto request);
        public List<UserDto> GetUsers();
    }
}
