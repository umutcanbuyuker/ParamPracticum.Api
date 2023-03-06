using Microsoft.AspNetCore.Mvc;
using ParamApi.Dto;
using ParamPracticum.Service.Abstract;
using Serilog;

namespace ParamPracticum.Api.Auth
{
    [Route("param/api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenManagementService tokenManagementService;
        public TokenController(ITokenManagementService tokenManagementService)
        {
            this.tokenManagementService = tokenManagementService;
        }
        [HttpPost("token")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequest request)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var result = await tokenManagementService.GenerateTokensAsync(request, DateTime.Now, userAgent);

            if (result.Success)
            {
                Log.Information($"User: {result.Response.UserName}, Role: {result.Response.Role} is logged in.");
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
