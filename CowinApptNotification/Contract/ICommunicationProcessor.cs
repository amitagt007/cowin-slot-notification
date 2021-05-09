using System;
using System.Collections.Generic;
using System.Text;

namespace CowinApptNotification
{
	public interface ICommunicationProcessor
	{
		public void SendSMS(string messageBody, string phoneNumber);

		public void SendEmail();
	}
}
