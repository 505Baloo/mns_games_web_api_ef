namespace MNSGamesWebAPI.Models.DTO
{
    public class LoginAppUserDTO
    {
        public string? LoginNickname { get; set; }
        public string LoginPassword { get; set; } 
        public string? Email { get; set; } 

        public LoginAppUserDTO() { }

        public LoginAppUserDTO(AppUser appUser)
        {
            LoginNickname = appUser.LoginNickname;
            LoginPassword = appUser.LoginPassword;
            Email = appUser.Email;
        }

        public AppUser ToAppUser()
        {
            return new AppUser
            {
                LoginNickname = LoginNickname,
                LoginPassword = LoginPassword,
                Email = Email
            };
        }
    }
}
