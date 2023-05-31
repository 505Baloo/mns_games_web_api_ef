using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Games = new HashSet<Game>();
            Questions = new HashSet<Question>();
            Registrates = new HashSet<Registrate>();
            Badges = new HashSet<Badge>();
        }

        public int Id { get; set; }
        public string? QuizName { get; set; }
        public TimeSpan? Duration { get; set; }
        public int ThemeId { get; set; }
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Theme Theme { get; set; } = null!;
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Registrate> Registrates { get; set; }

        public virtual ICollection<Badge> Badges { get; set; }
    }
}
