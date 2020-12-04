using paySimplexData.Context;
using paySimplexData.Contracts;
using paySimplexData.Entities;
using paySimplexData.Repository.Base;

namespace paySimplexData.Repository
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(paySimplexContext dbContext) : base(dbContext) { }
    }
}
