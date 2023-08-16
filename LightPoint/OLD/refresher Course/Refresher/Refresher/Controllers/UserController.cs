using Microsoft.AspNetCore.Mvc;
using Refresher.Dtos.Users;
using Refresher.RepoInterfaces;
using Refresher.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refresher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public void AddUser(CreateUserDto request)
        {
            _service.AddUser(request);
        }

        [HttpGet("[action]")]

        public List<UserDto> GetUsers()
        {
            return _service.GetUsers();
        }

    }
}
