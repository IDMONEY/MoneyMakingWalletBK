﻿#region Libraries
using IDMONEY.IO.Exceptions;
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
            Ensure.IsNotNull(businessRepository);

            this.businessRepository = businessRepository;
        }
        #endregion

        #region Methods
        public SearchBusinessResponse Get(string name)
        {
            SearchBusinessResponse response = new SearchBusinessResponse();
            var businesses = this.businessRepository.Get(name);

            if (businesses.IsNull())
            {
                throw new NotFoundException("Businesses not found");
            }
            response.Businesses = businesses;

            return response;
        } 
        #endregion
    }
}