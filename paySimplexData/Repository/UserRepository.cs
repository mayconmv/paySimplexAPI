using paySimplexData.Context;
using paySimplexData.Contracts;
using paySimplexData.Entities;
using paySimplexData.Repository.Base;

namespace paySimplexData.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(paySimplexContext dbContext) : base(dbContext) { }
    }
}
