using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class UserService : IEntityService<User>
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> InsertAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> InsertAsync(params User[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
