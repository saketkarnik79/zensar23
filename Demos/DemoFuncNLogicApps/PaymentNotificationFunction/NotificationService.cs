using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace PaymentNotificationFunction
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NotificationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendNotification(PaymentEvent payment)
        {
            // Implementation for sending notification
            var _logicAppUrl = _configuration["LogicAppEndpoint"];
            var payload = new
            {
                PaymentId = payment.PaymentId,
                CustomerName = payment.CustomerName,
                Email = payment.Email,
                PhoneNumber = payment.PhoneNumber,
                Amount = payment.Amount,
                Status = payment.Status,
                Currency = payment.Currency
            };
            var response = await _httpClient.PostAsJsonAsync(_logicAppUrl, payload);
            response.EnsureSuccessStatusCode();
        }
    }
}
