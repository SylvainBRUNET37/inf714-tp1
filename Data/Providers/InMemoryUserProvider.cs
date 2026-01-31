using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace INF714.Data.Providers
{
    public class InMemoryUserProvider : Interfaces.IUserProvider
    {
        private readonly Dictionary<Guid, User> _users = [];

        public InMemoryUserProvider()
        {

        }

        public async Task Create(User user)
        {
            if(_users.ContainsKey(user.Id))
            {
                throw new Exception("User already exists.");
            }
            _users.Add(user.Id, user);
        }

        public async Task<User> Get(Guid id)
        {
            User user = new();
            _users.TryGetValue(id, out user);
            return user;
        }

        public async Task Save(User user)
        {
            
        }
    }
}
