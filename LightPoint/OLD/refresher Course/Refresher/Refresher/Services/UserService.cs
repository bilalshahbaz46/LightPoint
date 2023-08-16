using AutoMapper;
using Refresher.Dtos.Users;
using Refresher.Models;
using Refresher.RepoInterfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Refresher.Services
{
    public class UserService : IUserService
    {
        private Context _context;
        private IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddUser(CreateUserDto request)
        {
            _context.Users.Add(_mapper.Map<User>(request));
            _context.SaveChanges();
        }

        public List<UserDto> GetUsers()
        {
            return _mapper.Map<List<UserDto>>(_context.Users);
        }
    }
}
