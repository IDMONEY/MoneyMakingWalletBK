using System;
using System.Security.Claims;
using IDMONEY.IO.DataAccess;
using IDMONEY.IO.Entities;

namespace IDMONEY.IO.Business
{
    public class BSEntryData : BaseBS
    {
        public BSEntryData(ClaimsPrincipal user) : base(user)
        {
        }

        public ResSearchEntryData SearchEntryData(ReqSearchEntryData req)
        {
            ResSearchEntryData res = new ResSearchEntryData();
            try
            {
                using (DAEntryDataUser daEntryData = new DAEntryDataUser())
                {
                    res.DataEntries = daEntryData.SearchEntryDataByUser(User.Id);
                }
                res.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
            }
            return res;
        }

        public ResSaveEntryData SaveEntryData(ReqSaveEntryData req)
        {
            ResSaveEntryData res = new ResSaveEntryData();
            try
            {
                using (DAEntryDataUser daEntryData = new DAEntryDataUser())
                {
                    daEntryData.InsertEntryDataByUser(User.Id, req.DataEntries);
                }
                res.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Errors.Add(new Error() { Code = ((int)EnumErrorCodes.ErrorNotSpecific).ToString(), Message = "There was a problem. Please try again later" });
            }
            return res;
        }
    }
}