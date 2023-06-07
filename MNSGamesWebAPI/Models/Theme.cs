using System;
using System.Collections.Generic;

namespace MNSGamesWebAPI.Models
{
    public partial class Theme
    {
        public Theme()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
