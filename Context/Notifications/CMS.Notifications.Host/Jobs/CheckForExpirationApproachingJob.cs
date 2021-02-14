using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CMS.Notifications.Host.JobsScheduler.Jobs
{
    public static class CheckForExpirationApproachingJob
    {
        public static async Task Execute(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CarsDbConnectionString");
            var notificationDaysBefore = configuration.GetValue<int>("NotificationDaysBefore");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var getCarsQuery = @"SELECT * from CARS";

                    var cars = await connection.QueryAsync<Car>(getCarsQuery);

                    var carsToReview = cars.Where(car => 
                        car.IsExpirationApproaching(notificationDaysBefore) ||
                        car.IsInstallmentApproaching(notificationDaysBefore));

                    if (carsToReview.Any())
                    {
                        await SendNotification();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SendNotification()
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Powiadomienie",
                    Body = "Zbliża się koniec terminu."
                },
                Topic = "main"
            };

            var messageInJsonFormat = JsonConvert.SerializeObject(message, Formatting.Indented);
            var messageId = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            Console.WriteLine($"Successfully sent message '{messageId}' with payload: {Environment.NewLine}{messageInJsonFormat}");
        }
    }
}
