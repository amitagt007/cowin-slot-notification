using System;
using System.Collections.Generic;
using System.Text;

namespace CowinApptNotification
{
    public class NotificationData
    {
        public string phone { get; set; }
        public List<int> pins { get; set; }
    }

    public class MasterData
	{
		public List<NotificationData> notificationData { get; set; }
	}
}
