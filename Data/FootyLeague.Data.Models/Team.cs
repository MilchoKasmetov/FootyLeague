namespace FootyLeague.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using FootyLeague.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.Matches = new HashSet<Match>();
            this.HomeMatches = new HashSet<Match>();
            this.AwayMatches = new HashSet<Match>();

        }

        [Required]
        [StringLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Points { get; set; }

        public ICollection<Match> Matches { get; set; }

        public ICollection<Match> HomeMatches { get; set; }

        public ICollection<Match> AwayMatches { get; set; }
    }
}
