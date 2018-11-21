#region Libraries
using System.Security.Claims;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessService
    {
        BusinessResponse Get(int id);
        SearchBusinessResponse FindByName(string name);
        InsertBusinessResponse Create(CreateBusinessRequest request);
        SearchBusinessResponse GetAll();
        SearchBusinessResponse GetByUser(ClaimsPrincipal claimsPrincipal);
    }
}
