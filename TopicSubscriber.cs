namespace MessageBrokerLLD
{
    public class TopicSubscriber : ISubscriber
    {
        public string Id { get; set; }
        public int Offset { get; set; }

        public TopicSubscriber(string id)
        {
            Id = id;
            Offset = 0;
        }

        public void Consume(string message)
        {
            Console.WriteLine("Subscriber {0} consuming message {1}", Id, message);
        }
    }
}