using Solitaire.Models;
using System.ComponentModel.DataAnnotations;

namespace Solitaire.ViewModels
{
    public class ScoreResponse
    {
        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public string Minutes { get; set; } = string.Empty;
        public int ScoreCount { get; set; }

        public virtual string ApplicationUserId { get; set; } = null!;
    }
}
