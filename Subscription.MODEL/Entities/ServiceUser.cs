using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.MODEL.Entities
{
    public class ServiceUser : IdentityUser
    {
        public string ServiceId { get; set; } 
        
    }
}
