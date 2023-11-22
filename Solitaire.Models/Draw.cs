using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Models
{
    public class Draw
    {
        [Key]
        public int Id { get; set; }
        public int Sort { get; set; }
        public string FromPosition { get; set; } = null!;
        public string ToPosition { get; set; } = null!;

        public virtual int SolitaireSessionId { get; set; }
        public virtual SolitaireSession SolitaireSession { get; set; }
    }
}
