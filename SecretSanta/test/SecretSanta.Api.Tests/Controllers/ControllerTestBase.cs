using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Api.Tests.Controllers
{
    /* reference from Blod Engine
    public class AuthorService : IAuthorService
    {
        private Dictionary<int, Author> Items { get; } = new Dictionary<int, Author>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> FetchByIdAsync(int id)
        {
            if(Items.TryGetValue(id, out Author? author))
            {
                Task<Author?> t1 = Task.FromResult<Author?>(author);
                return t1;
            }
            Task<Author?> t2 =  Task.FromResult<Author?>(null);
            return t2;
        }

        public Task<Author> InsertAsync(Author entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestAuthor(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Author[]> InsertAsync(params Author[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Author?> UpdateAsync(int id, Author entity)
        {
            throw new NotImplementedException();
        }
    }

    public class  TestAuthor : Author
    {
        public TestAuthor(Author author, int id)
            : base(author.FirstName, author.LastName, author.Email)
        {
            Id = id;
        }
    }
}
     */
    [TestClass]
    public abstract class ControllerTestBase<TService>
    {
        protected abstract TService CreateService();
    }
}
