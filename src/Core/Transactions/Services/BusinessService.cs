#region Libraries
using System;
using System.Collections.Generic;
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

        public SearchBusinessResponse FindByName(string name)
        {
            return this.Get(() => this.businessRepository.FindByName(name));
        }

        public BusinessResponse Get(int id)
        {
            var business = this.businessRepository.Get(id);

            if (business.IsNull())
            {
                throw new NotFoundException("Business not found");
            }
            return new BusinessResponse()
            {
                Business = business
            };
        }

        public SearchBusinessResponse GetAll()
        {
            return this.Get(() => this.businessRepository.GetAll());
        }
        #endregion

        #region Methods
        private SearchBusinessResponse Get(Func<IList<Business>> action)
        {
            SearchBusinessResponse response = new SearchBusinessResponse();
            response.Businesses = action();
            response.IsSuccessful = true;
            return response;
        } 
        #endregion
    }
}
