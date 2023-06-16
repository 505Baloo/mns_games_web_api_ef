namespace MNSGamesWebAPI.Models
{
    public class TokenValidationRequest
    {
        public string? Token { get; set; }

        public TokenValidationRequest(string? token)
        {
            Token = token;
        }
    }
}
