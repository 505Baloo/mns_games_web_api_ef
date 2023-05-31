using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Game
    {
        public Game()
        {
            Obtains = new HashSet<Obtain>();
        }

        public int Id { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime? EndDatetime { get; set; }
        public int QuizId { get; set; }
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Obtain> Obtains { get; set; }
    }
}
