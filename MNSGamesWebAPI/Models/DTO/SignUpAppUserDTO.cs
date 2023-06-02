namespace MNSGamesWebAPI.Models.DTO
{
    public class SignUpAppUserDTO
    {
        public string Email { get; set; }
        public string LoginNickname { get; set; }
        public string LoginPassword { get; set; }

        public SignUpAppUserDTO() { }

        public SignUpAppUserDTO(AppUser appUser)
        {
            Email = appUser.Email;
            LoginNickname = appUser.LoginNickname;
            LoginPassword = appUser.LoginPassword;
        }

        public AppUser ToAppUser()
        {
            return new AppUser
            {
                Email = Email,
                LoginNickname = LoginNickname,
                LoginPassword = LoginPassword,
            };
        }
    }
}
