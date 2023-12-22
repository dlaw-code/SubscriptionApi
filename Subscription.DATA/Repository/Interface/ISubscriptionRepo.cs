using Subscription.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.DATA.Repository.Interface
{
    public interface ISubscriptionRepo
    {
        Task<Subscriptions> AddSubscription(Subscriptions subscription);
        Task <string> UpdateSubscription(Subscriptions subscription);
        Task <Subscriptions> GetSubscriptionbyServiceUserId(string serviceUserId);
    }
}
