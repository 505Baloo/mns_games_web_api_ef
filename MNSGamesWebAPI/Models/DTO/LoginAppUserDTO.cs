namespace MNSGamesWebAPI.Models.DTO
{
    public class LoginAppUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string LoginPassword { get; set; } 

        public LoginAppUserDTO() { }

        public LoginAppUserDTO(AppUser appUser)
        {
            Id = appUser.Id;
            LoginPassword = appUser.LoginPassword;
            Email = appUser.Email;
        }

        public AppUser ToAppUser()
        {
            return new AppUser
            {
                Id = Id,
                LoginPassword = LoginPassword,
                Email = Email
            };
        }
    }
}
