using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GroupService : IEntityService<Group>
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group> FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> InsertAsync(Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<Group[]> InsertAsync(params Group[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Group> UpdateAsync(int id, Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
