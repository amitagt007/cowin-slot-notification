# cowin-slot-notification
###### 

### Getting started

This is a console app which sends sms notification when vaccination slots are available for pin codes using .NET Core and Twilio SMS integration. This can be easily integrated with Windows Task Scheduler or Azure to send SMS notification once slots are available at the requested pincodes

##### Usage
To use this we require free twilio account to get Twilio API key, Sid and phone number. Just replace them in the appsettings.json and enter your phone number along with the pincodes you are looking slot for in masterData.Json file. Once done, you can use the dll to schedule in windows task scheduler and just wait and relax til you gee notified of slots.
