namespace MNSGamesWebAPI.Models.DTO
{
    public class AppUserDTO
    {
        public string LoginNickname { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public string Email { get; set; } = null!;

        public AppUserDTO(AppUser appUser)
        {
            LoginNickname = appUser.LoginNickname;
            LoginPassword = appUser.LoginPassword;
            Email = appUser.Email;
        }
    }
}
