using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CowinApptNotification
{
    class NotifcationProcessor : INotifcationProcessor
    {
        private readonly IApiProcessor _apiProcessor;
        private readonly ICommunicationProcessor _communicationProcessor;

        public NotifcationProcessor(IApiProcessor apiProcessor, ICommunicationProcessor communicationProcessor)
        {
            _apiProcessor = apiProcessor;
            _communicationProcessor = communicationProcessor;
        }
        public async Task ProcessNotificationByPin()
        {
            var masterData = GetMasterData();
            foreach(var data in masterData.notificationData)
            {
                IList<int> availablePincodes = new List<int>();
                foreach (var code in data.pins)
                {
                    var eligiblePincode = await _apiProcessor.FetchAvailablePincodes(code);
                    if (eligiblePincode > 0)
                    {
                        availablePincodes.Add(eligiblePincode);
                    }
                }
                _communicationProcessor.SendSMS(formattedMessage(availablePincodes), data.phone);
            }
        }

        private string formattedMessage(IList<int> availablePincodes)
        {
            string msgHeader = "Vaccination Slots available at ";
            string pincodeMessage = "pincode ";
            string appenedMessage = string.Empty;
            var counter = 0;
            foreach (var pincode in availablePincodes)
            {
                appenedMessage = appenedMessage + " " + pincode + ",";
                counter++;
            }
            appenedMessage = appenedMessage.TrimEnd(',');
            if (counter > 1)
            {
                pincodeMessage = "pincodes ";
            }
            return msgHeader + pincodeMessage + appenedMessage;
        }

        private MasterData GetMasterData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "masterData.json");
            var jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<MasterData>(jsonString);
        }

    }
}
