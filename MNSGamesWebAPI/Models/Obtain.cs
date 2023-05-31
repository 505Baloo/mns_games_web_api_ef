using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Obtain
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int GameId { get; set; }
        public DateTime? DatetimeQuestionDisplay { get; set; }
        public DateTime? DatetimeAnswerDisplay { get; set; }

        public virtual Answer Answer { get; set; } = null!;
        public virtual Game Game { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
    }
}
