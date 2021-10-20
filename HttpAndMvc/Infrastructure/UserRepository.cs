using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpAndMvc.Models;

namespace HttpAndMvc.Infrastructure
{
    public class UserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    Email = "test@test.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                },
                new User
                {
                    Id = 2,
                    Email = "test2@test.com",
                    FirstName = "Petr",
                    LastName = "Petrov"
                }
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public User Add(User user)
        {
            var lastUserId = _users.LastOrDefault()?.Id ?? 0;
            user.Id = lastUserId + 1;
            _users.Add(user);
            return user;
        }
    }
}