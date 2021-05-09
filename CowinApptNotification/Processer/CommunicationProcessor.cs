using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;
namespace CowinApptNotification
{
    public class CommunicationProcessor : ICommunicationProcessor
    {
        private readonly IConfigurationRoot _config;
        public CommunicationProcessor(IConfigurationRoot config)
        {
            _config = config;
        }
        public void SendEmail()
        {
            //TODO
        }

        public void SendSMS(string messageBody, string phoneNumber)
        {
            string accountSid = _config.GetSection("TWILIO_ACCOUNT_SID").Value;
            string authToken = _config.GetSection("TWILIO_AUTH_TOKEN").Value;
            string fromPhoneNumber = _config.GetSection("TWILIO_PHONE_NUMBER").Value;

            TwilioClient.Init(accountSid, authToken);
            var smsMessage = MessageResource.Create(
            body: messageBody,
            from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
            to: new Twilio.Types.PhoneNumber($"+91{ phoneNumber }")
            );

            if (smsMessage.Sid != null)
            {
                Console.WriteLine($"SMS sent to + 91{ phoneNumber }");
            }
        }
    }
}
