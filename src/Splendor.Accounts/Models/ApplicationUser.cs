using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splendor.Accounts.Models {
    public class ApplicationUser : IdentityUser {
        public DateTime BirdDate { get; set; }

    }
}
