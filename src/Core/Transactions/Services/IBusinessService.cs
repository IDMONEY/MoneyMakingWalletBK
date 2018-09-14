#region Libraries
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessService
    {
        SearchBusinessResponse Get(string name);
    }
}
