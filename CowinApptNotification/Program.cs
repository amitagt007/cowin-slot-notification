using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CowinApptNotification
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

                //  DI
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IApiProcessor, ApiProcessor>()
                    .AddSingleton<ICommunicationProcessor, CommunicationProcessor>()
                    .AddSingleton<INotifcationProcessor, NotifcationProcessor>()
                    .AddSingleton(configuration)
                    .BuildServiceProvider();
                var notifcationProcessor = serviceProvider.GetService<INotifcationProcessor>();
                await notifcationProcessor.ProcessNotificationByPin();

                Console.ReadKey();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            
        }
    }
}
