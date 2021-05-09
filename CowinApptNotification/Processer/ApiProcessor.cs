using CowinApptNotification.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace CowinApptNotification
{
	public class ApiProcessor : IApiProcessor
    {
        private IList<Result> finalResult;
        public async Task<int> FetchAvailablePincodes(int pincode)
        {
            var date = DateTime.Now.ToString("dd-MM-yy");
            var url = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByPin?pincode="+ pincode +"&date="+ date +"";
            Response modelData;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                modelData =  JsonConvert.DeserializeObject<Response>(jsonString);
            }
            if (modelData != null)
            {
                return ProcessData(modelData);
            }
            else 
            {
                return 0;
            }
        }

        private int ProcessData(Response allCenters)
        {
            finalResult = new List<Result>();
            foreach (var center in allCenters.centers)
            {
                string centerName = center.name;
                foreach (var session in center.sessions)
                {
                    if (session.available_capacity > 0)
                    {
                        var obj = new Result() {pincode = center.pincode };
                        finalResult.Add(obj);
                    }
                }
            }
            if(finalResult.Count > 0)
            {
                return finalResult.Select(x => x.pincode).Distinct().ToArray()[0];
            } else
            {
                return 0;
            }
        }
    }
}
