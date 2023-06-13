namespace MNSGamesWebAPI.Models.DTO
{
    public class LoginAppUserDTO
    {
        public string Email { get; set; } 
        public string LoginPassword { get; set; } 

        public LoginAppUserDTO() { }

        public LoginAppUserDTO(AppUser appUser)
        {
            LoginPassword = appUser.LoginPassword;
            Email = appUser.Email;
        }

        public AppUser ToAppUser()
        {
            return new AppUser
            {
                LoginPassword = LoginPassword,
                Email = Email
            };
        }
    }
}
