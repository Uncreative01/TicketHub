using Azure.Storage.Queues;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TicketHub.Models;

namespace TicketHub.Services
{
    public class QueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(IConfiguration configuration)
        {
            string connectionString = configuration["AzureStorage:ConnectionString"];
            string queueName = configuration["AzureStorage:QueueName"];

            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageToQueueAsync(TicketPurchase ticket)
        {
            string message = JsonSerializer.Serialize(ticket);
            await _queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message)));
        }
    }
}
