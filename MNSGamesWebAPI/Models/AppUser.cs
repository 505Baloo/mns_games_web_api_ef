using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            Games = new HashSet<Game>();
            Quizzes = new HashSet<Quiz>();
            Registrates = new HashSet<Registrate>();
        }

        public int Id { get; set; }
        public string LoginNickname { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? Zipcode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Registrate> Registrates { get; set; }
    }
}
