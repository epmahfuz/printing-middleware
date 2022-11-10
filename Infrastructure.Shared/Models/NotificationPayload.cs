using System;
using System.Collections.Generic;

namespace Infrastructure.Shared.Models
{
    public class NotificationPayload
    {
        public string NotificationType { get; set; }
        public string ResponseKey { get; set; }
        public string DenormalizedPayload { get; set; }
        public string ResponseValue { get; set; }
        public IEnumerable<SubscriptionFilter> SubscriptionFilters { get; set; }
        public NotificationPayload()
        {
            SubscriptionFilters = new List<SubscriptionFilter>();
        }
    }
    public class SubscriptionFilter
    {
        public string ActionName { get; set; }
        public string Context { get; set; }
        public string Value { get; set; }
    }
}
