namespace PaymentNotificationFunction
{
    public interface INotificationService
    {
        Task SendNotification(PaymentEvent payment);
    }
}
