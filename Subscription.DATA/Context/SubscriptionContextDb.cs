using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Subscription.MODEL.Entities;

namespace Subscription.DATA.Context
{
    public class SubscriptionContextDb : IdentityDbContext<ServiceUser>
    {
        public DbSet<Subscriptions> subscriptions { get; set; }
        public SubscriptionContextDb(DbContextOptions options) : base(options)
        {

        }

    }
}
