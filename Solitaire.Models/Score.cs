using System.ComponentModel.DataAnnotations;

namespace Solitaire.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public TimeSpan GameDuration { get; set; }
        public int ScoreCount { get; set; }

        public virtual string ApplicationUserId { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
