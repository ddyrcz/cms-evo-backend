using FirebaseAdmin;
using System.IO;
using Microsoft.Extensions.Configuration;
using CMS.Notifications.Host.JobsScheduler.Jobs;

namespace CMS.Notifications.Host
{
    public class Prorgam
    {
        
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();

            FirebaseApp.Create();

            CheckForExpirationApproachingJob.Execute(configuration).Wait();
        }
    }
}