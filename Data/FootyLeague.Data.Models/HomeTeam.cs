namespace FootyLeague.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Common.Models;

    public class HomeTeam : BaseDeletableModel<int>
    {
        [Required]
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
