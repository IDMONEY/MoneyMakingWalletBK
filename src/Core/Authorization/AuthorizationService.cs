#region Libraries
using System;
using System.Text;
using System.Threading.Tasks;
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
        private readonly INicknameRepository nicknameRepository;
        private readonly ITokenGenerator tokenGenerator;
        #endregion

        #region Constructor
        public AuthorizationService(IUserRepository userRepository, INicknameRepository nicknameRepository, ITokenGenerator tokenGenerator)
        {
            Ensure.IsNotNull(userRepository);
            Ensure.IsNotNull(nicknameRepository);
            Ensure.IsNotNull(tokenGenerator);

            this.userRepository = userRepository;
            this.nicknameRepository = nicknameRepository;
            this.tokenGenerator = tokenGenerator;
        }
        #endregion

        #region Methods
        public async Task<LoginUserResponse> AuthorizeAsync(LoginUserRequest request)
        {

            LoginUserResponse response = new LoginUserResponse();
            try
            {
                var user = await this.userRepository.GetByCredentialsAsync(request.Email, $"{request.Email}:{request.Password}".GenerateSHA512());


                if (user.IsNotNull())
                {
                    response.User = user;
                    response.Token = this.tokenGenerator.Generate(user.Id.ToString());
                    response.Nickname = await this.nicknameRepository.GetAByUserAsync(user);
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Errors.Add(new Error() { Code = ((int)ErrorCodes.NotFound).ToString(), Message = "Email or Password is incorrect" });
                }
            }
            catch (Exception exception)
            {
                throw new IDMoneyException(new Error() { Code = ((int)ErrorCodes.Unknown).ToString(), Message = "There was a problem. Please try again later" });
            }
            return response;
        }
        #endregion
    }
}