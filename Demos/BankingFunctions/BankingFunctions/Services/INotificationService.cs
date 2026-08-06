using PaymentNotificationFunction.Models;

namespace PaymentNotificationFunction.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(PaymentEvent paymentEvent);
    }
}
