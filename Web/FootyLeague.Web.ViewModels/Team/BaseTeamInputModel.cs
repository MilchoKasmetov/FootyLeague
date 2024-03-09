﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootyLeague.Web.ViewModels.Team
{
    public abstract class BaseTeamInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}