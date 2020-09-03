﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RedeHortaRayanRalile.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Usuario class
    public class Usuario : IdentityUser
    {
        [PersonalData]
        public DateTime DataNascimento { get; set; }
    }
}
