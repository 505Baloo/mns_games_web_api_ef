﻿using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Badge
    {
        public Badge()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Descript { get; set; }
        public string? ObtainingConditions { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
