using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.MODEL.Entities
{
    public class Subscriptions
    {
        [Key]
        public string SubscriptionId { get; set; } = Guid.NewGuid().ToString();
        public string ServiceUserId { get; set; }
        public ServiceUser ServiceUser { get; set; }
        public string SubscriptionStatus { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubcribeDate { get; set; }
        public DateTime UnSubscribeDate { get; set; }
    }
}
