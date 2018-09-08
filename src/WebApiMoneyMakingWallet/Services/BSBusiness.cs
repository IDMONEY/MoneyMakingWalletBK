using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IDMONEY.IO.Transactions;

namespace IDMONEY.IO.Services
{
    public class BSBusiness : BaseBS
    {
        public BSBusiness(ClaimsPrincipal user) : base(user)
        {
        }

        public ResSearchBusiness SearchBusiness(ReqSearchBusiness req)
        {
            ResSearchBusiness res = new ResSearchBusiness();

            using (DABusiness da = new DABusiness())
            {
                res.Businesses = da.SearchBusiness(req.Name);
            }
            res.IsSuccessful = true;

            return res;
        }
    }
}
