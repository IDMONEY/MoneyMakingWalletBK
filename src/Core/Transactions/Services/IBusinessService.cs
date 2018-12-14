#region Libraries
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public interface IBusinessService
    {
        Task<BusinessResponse> GetAsync(long id);
        Task<SearchBusinessResponse> FindByNameAsync(string name);
        Task<InsertBusinessResponse> CreateAsync(CreateBusinessRequest request);
        Task<SearchBusinessResponse> GetAllAsync();
        Task<SearchBusinessResponse> GetByUserAsync(ClaimsPrincipal claimsPrincipal);
    }
}
