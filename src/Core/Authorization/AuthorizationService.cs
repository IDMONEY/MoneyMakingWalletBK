#region Libraries
using System;
using System.Text;
using IDMONEY.IO.Cryptography;
using IDMONEY.IO.Exceptions;
using IDMONEY.IO.Requests;
using IDMONEY.IO.Responses;
using IDMONEY.IO.Users;

#endregion
namespace IDMONEY.IO.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Members
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator tokenGenerator;
        #endregion


        #region Constructor
        public AuthorizationService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            Ensure.IsNotNull(userRepository);
            Ensure.IsNotNull(tokenGenerator);

            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }
        #endregion

        #region Methods
        public LoginUserResponse Authorize(LoginUserRequest request)
        {

            LoginUserResponse response = new LoginUserResponse();
            try
            {
                var user = this.userRepository.GetByCredentials(request.Email, request.Password);


                if (user.IsNotNull())
                {
                    response.User = user;
                    response.Token = this.tokenGenerator.Generate(user.Id.ToString());
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = "Email or Password is incorrect" });
                }
            }
            catch (Exception)
            {
                throw new IDMoneyException(new Error() { Code = ((int)ErrorCodes.Unknown).ToString(), Message = "There was a problem. Please try again later" });
            }
            return response;
        } 
        #endregion
    }
}
