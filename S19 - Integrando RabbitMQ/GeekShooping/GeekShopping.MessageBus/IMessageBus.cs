namespace GeekShopping.MessageBus
{
    public interface IMessageBus
    {
        Task publicMessage(BaseMessage message, string queueName);
    }
}