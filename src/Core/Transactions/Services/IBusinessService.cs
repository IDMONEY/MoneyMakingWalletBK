#region Libraries
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessService
    {
        BusinessResponse Get(int id);
        SearchBusinessResponse FindByName(string name);
        SearchBusinessResponse GetAll();
    }
}
