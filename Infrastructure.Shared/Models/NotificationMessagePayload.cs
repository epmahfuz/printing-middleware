using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Shared.Models
{
    public class NotificationMessagePayload
    {
        public string KioskId { get; set; }
        public string SerializedPayload { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
