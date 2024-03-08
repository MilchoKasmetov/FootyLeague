﻿using FootyLeague.Data.Common.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootyLeague.Data.Models
{
    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.Matches = new HashSet<Match>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public ICollection<Match> Matches { get; set; }
    }
}
