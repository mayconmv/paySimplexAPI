using paySimplexBusiness.Models;

namespace paySimplexBusiness.Contracts
{
    public interface ITaskContract : IBaseContract<TaskModel>
    {
        object GetMany(string arguments);

        object UploadFile(long taskId, string fileName, byte[] fileBytes, long userId);

        object GetTimeInProgress(long id);
    }
}
