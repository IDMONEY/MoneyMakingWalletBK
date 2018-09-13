#region Libraries
using IDMONEY.IO.Responses;
#endregion

namespace IDMONEY.IO.Transactions
{
    public class BusinessService : IBusinessService
    {
        #region Members
        private readonly IBusinessRepository businessRepository;
        #endregion

        #region Constructor
        public BusinessService(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }
        #endregion

        #region Methods
        public SearchBusinessResponse Get(string name)
        {
            SearchBusinessResponse response = new SearchBusinessResponse();
            response.Businesses = this.businessRepository.Get(name);

            return response;
        } 
        #endregion
    }
}
