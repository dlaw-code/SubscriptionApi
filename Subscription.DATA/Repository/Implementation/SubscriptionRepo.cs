using Microsoft.EntityFrameworkCore;
using Subscription.DATA.Context;
using Subscription.DATA.Repository.Interface;
using Subscription.MODEL.Entities;

namespace Subscription.DATA.Repository.Implementation
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly SubscriptionContextDb _context;

        public SubscriptionRepo(SubscriptionContextDb context)
        {
            _context = context;
        }
        public async Task<Subscriptions> AddSubscription(Subscriptions subscription)
        {
            var add = await _context.subscriptions.AddAsync(subscription);
            if (await _context.SaveChangesAsync() > 0)
            {
                return add.Entity;
            }
            return null;
        }

        public async Task<Subscriptions> GetSubscriptionbyServiceUserId(string serviceUserId)
        {
            var result = await _context.subscriptions.FirstOrDefaultAsync(s => s.ServiceUserId == serviceUserId);
            return result;
        }

        public async Task<string> UpdateSubscription(Subscriptions subscription)
        {
            _context.subscriptions.Update(subscription);
            if (await _context.SaveChangesAsync() > 0)
            {
                return "success updated";
            }
            return null;

        }
    }
}
