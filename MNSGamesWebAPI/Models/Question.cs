using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Obtains = new HashSet<Obtain>();
            AnswersNavigation = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string? LabelQuestion { get; set; }
        public TimeSpan? Duration { get; set; }
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Obtain> Obtains { get; set; }

        public virtual ICollection<Answer> AnswersNavigation { get; set; }
    }
}
