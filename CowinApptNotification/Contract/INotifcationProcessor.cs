using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CowinApptNotification
{
	public interface INotifcationProcessor
	{
		public Task ProcessNotificationByPin();
	}
}
