using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MNSGamesWebAPI.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Obtains = new HashSet<Obtain>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string LabelAnswer { get; set; }

        [DefaultValue(false)]
        public bool IsCorrect { get; set; }
        public int? Points { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual ICollection<Obtain> Obtains { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
