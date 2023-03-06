using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParamApi.Dto;
using ParamPracticum.Base;
using ParamPracticum.Base.Jwt;
using ParamPracticum.Data.Models;
using ParamPracticum.Data.Repository.Abstract;
using ParamPracticum.Data.Uow;
using ParamPracticum.Service.Abstract;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParamPracticum.Service.Concrete
{
    public class TokenManagementService : ITokenManagementService
    {
        private readonly IGenericRepository<Account> genericRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly JwtConfig _jwtConfig;
        private readonly byte[] _secret;
        public TokenManagementService(IGenericRepository<Account> genericRepository, IMapper mapper, IUnitOfWork unitOfWork,IOptionsMonitor<JwtConfig> jwtConfig) 
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this._jwtConfig = jwtConfig.CurrentValue;
            this._secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        }
        public async Task<BaseResponse<TokenResponse>> GenerateTokensAsync(TokenRequest tokenRequest, DateTime now, string userAgent)
        {
            try
            {
               var account = genericRepository.Where(x=> x.UserName == tokenRequest.UserName).FirstOrDefault();
                if (account == null)
                {
                    Log.Error("InvalidUserInformation");
                    return new BaseResponse<TokenResponse>("InvalidUserInformation");
                }
                if (account.Password != tokenRequest.Password)
                {
                    Log.Error("InvalidUserInformation");
                    return new BaseResponse<TokenResponse>("InvalidUserInformation");
                }

                var token = GenerateAccessToken(account, now);
                account.LastActivity= DateTime.Now;
                unitOfWork.AccountRepository.Update(account);
                await unitOfWork.CompleteAsync();

                TokenResponse response = new TokenResponse
                {
                    AccessToken= token,
                    ExpireTime = now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                    Role = account.Role,
                    UserName = account.UserName
                };
                return new BaseResponse<TokenResponse>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenerateToken_Error");
                return new BaseResponse<TokenResponse>("GenerateToken_Error");
            }
        }



        private string GenerateAccessToken(Account account, DateTime now)
        {
            // Get claim value
            Claim[] claims = GetClaim(account);

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _jwtConfig.Issuer,
                shouldAddAudienceClaim ? _jwtConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }



        private static Claim[] GetClaim(Account account)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim("AccountId", account.Id.ToString()),
                new Claim("LastActivity", account.LastActivity.ToLongTimeString())
            };
            return claims;
        }


    }
}
