using ParamApi.Dto;
using ParamPracticum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamPracticum.Service.Abstract
{
    public interface ITokenManagementService
    {
        Task<BaseResponse<TokenResponse>> GenerateTokensAsync(TokenRequest loginResource, DateTime now, string userAgent);
    }
}
