namespace MessageBrokerLLD
{
    public interface ISubscriber
    {
        void Consume(string message);
    }
}