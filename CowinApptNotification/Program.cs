using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CowinApptNotification
{
    class Program
    {
        static void Main(string[] args)
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
            _ = notifcationProcessor.ProcessNotificationByPin();

            Console.ReadKey();
        }
    }
}
