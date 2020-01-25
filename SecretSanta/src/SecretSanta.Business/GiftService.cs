using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GiftService : IEntityService<Gift>
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gift>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Gift> FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> InsertAsync(Gift entity)
        {
            throw new NotImplementedException();
        }

        public Task<Gift[]> InsertAsync(params Gift[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> UpdateAsync(int id, Gift entity)
        {
            throw new NotImplementedException();
        }
    }
}
