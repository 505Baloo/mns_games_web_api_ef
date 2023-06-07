using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Registrate
    {
        public int QuizId { get; set; }
        public int AppUserId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
