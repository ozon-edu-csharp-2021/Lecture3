using System.Collections.Generic;
using System.Linq;
using HttpAndMvc.Filters;
using HttpAndMvc.Infrastructure;
using HttpAndMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Simplest.Middlewares;

namespace HttpAndMvc.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public List<UserDto> GetUsersAsync()
        {
            var users = _userRepository.GetAll();

            var usersDtos = users.Select(x => new UserDto
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
            }).ToList();

            return usersDtos;
        }

        [AddHeader("feature", "get-user-by-id")]
        [MiddlewareFilter(typeof(LoggingMiddleware))]
        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
                return NotFound();

            var usersDto =  new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };
            
            return usersDto;
        }     
        
        [HttpPost]
        public ActionResult<UserDto> AddUser([FromBody] AddUserDto addUserDto)
        {
            var user = _userRepository.Add(new User
            {
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName
            });

            var userDto =  new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };
            
            return userDto;
        }
    }
}