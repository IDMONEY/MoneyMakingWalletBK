﻿using IDMONEY.IO.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Requests;

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

            using (DABusiness da = new DABusiness())
            {
                res.Businesses = da.SearchBusiness(req.Name);
            }
            res.IsSuccessful = true;

            return res;
        }
    }
}