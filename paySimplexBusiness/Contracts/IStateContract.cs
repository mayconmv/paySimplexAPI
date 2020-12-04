using paySimplexBusiness.Models;

namespace paySimplexBusiness.Contracts
{
    public interface IStateContract : IBaseContract<StateModel>
    {
        object GetMany(string arguments);
    }
}
