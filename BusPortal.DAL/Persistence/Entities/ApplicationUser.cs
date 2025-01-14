using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.DAL.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; set; }
        public string Email { get; set; }
    }
}
