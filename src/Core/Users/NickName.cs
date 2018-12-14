#region Libraries
using System; 
#endregion

namespace IDMONEY.IO.Users
{
    public class NickName
    {
        #region Properties
        public string Value { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion


        #region Methods
        public static NickName Create(string value) =>
            new NickName() { Value = value, CreationDate = SystemTime.Now() };
        #endregion
    }
}