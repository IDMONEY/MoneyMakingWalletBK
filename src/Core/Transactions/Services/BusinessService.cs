#region Libraries
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Exceptions;
using IDMONEY.IO.Requests;
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

        public async Task<InsertBusinessResponse> CreateAsync(CreateBusinessRequest request)
        {
            InsertBusinessResponse response = new InsertBusinessResponse();

            var candidateBusiness = new Business()
            {
                Name = request.Name,
                Description = request.Description,
                Image = request.Image
            };

            var id = await this.businessRepository.AddAsync(candidateBusiness);
            candidateBusiness.Id = id;
            response.IsSuccessful = true;

            response.Business = candidateBusiness;

            return response;
        }

        public async Task<SearchBusinessResponse> FindByNameAsync(string name)
        {
            return await this.Get(() => this.businessRepository.FindByNameAsync(name));
        }

        public async Task<BusinessResponse> GetAsync(int id)
        {
            var business = await this.businessRepository.GetAsync(id);

            if (business.IsNull())
            {
                throw new NotFoundException("Business not found");
            }
            return new BusinessResponse()
            {
                Business = business
            };
        }

        public async Task< SearchBusinessResponse> GetAllAsync()
        {
            return await this.Get(() => this.businessRepository.GetAllAsync());
        }

        public async Task<SearchBusinessResponse> GetByUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return await this.Get(() => this.businessRepository.GetByUserAsync(claimsPrincipal.GetUserId()));
            }
            catch (Exception exc)
            {

                throw;
            }
        }
        #endregion

        #region Methods
        private async Task<SearchBusinessResponse> Get(Func<Task<IList<Business>>> action)
        {
            SearchBusinessResponse response = new SearchBusinessResponse();
            response.Businesses = await action();
            response.IsSuccessful = true;
            return response;
        }
        #endregion
    }
}
