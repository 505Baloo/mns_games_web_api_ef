using Microsoft.AspNetCore.Mvc;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Services;

namespace MNSGamesWebAPI.Controllers
{
    [Route("api/Token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly TokenService _tokenService;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult ValidateToken(TokenValidationRequest request)
        {
            bool isValid = _tokenService.ValidateToken(request.Token!);

            return Ok(new TokenValidationResponse { IsValid = isValid });
        }
    }
}
