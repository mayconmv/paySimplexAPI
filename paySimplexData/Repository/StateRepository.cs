using paySimplexData.Context;
using paySimplexData.Contracts;
using paySimplexData.Entities;
using paySimplexData.Repository.Base;

namespace paySimplexData.Repository
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(paySimplexContext dbContext) : base(dbContext) { }
    }
}
