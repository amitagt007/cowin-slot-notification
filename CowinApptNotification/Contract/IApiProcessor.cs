using CowinApptNotification.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CowinApptNotification
{
	public interface IApiProcessor
	{
		public Task<int> FetchAvailablePincodes(int pincode);
	}
}
