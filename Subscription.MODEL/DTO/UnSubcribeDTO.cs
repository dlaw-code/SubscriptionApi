using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.MODEL.DTO
{
    public class UnSubcribeDTO
    {
        public string SubscriptionId { get; set; }
        public string ServiceId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UnSubscriptionDate { get; set; }
    }
}
