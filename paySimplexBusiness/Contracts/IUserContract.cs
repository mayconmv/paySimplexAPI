using paySimplexBusiness.Models;

namespace paySimplexBusiness.Contracts
{
    public interface IUserContract : IBaseContract<UserModel> {
        object GetMany(string arguments);
    }
}
