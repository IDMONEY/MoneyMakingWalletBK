﻿#region Libraries
using IDMONEY.IO.Databases;
using System.Security.Claims;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Requests; 
#endregion

namespace IDMONEY.IO.Services
{
    public class BSBusiness : BaseBS
    {
        public BSBusiness(ClaimsPrincipal user) : base(user)
        {
        }

        public SearchBusinessResponse SearchBusiness(SearchBusinessRequest req)
        {
            SearchBusinessResponse res = new SearchBusinessResponse();

            using (BusinessDatabase da = new BusinessDatabase())
            {
                res.Businesses = da.SearchBusiness(req.Name);
            }
            res.IsSuccessful = true;

            return res;
        }
    }
}
