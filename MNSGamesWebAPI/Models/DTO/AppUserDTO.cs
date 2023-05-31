namespace MNSGamesWebAPI.Models.DTO
{
    public class AppUserDTO
    {
        public string LoginNickname { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? Zipcode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
