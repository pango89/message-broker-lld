namespace MessageBrokerLLD
{
    public class Topic
    {
        public string Id { get; }
        public string Name { get; }
        public List<string> Messages { get; }
        public List<TopicSubscriber> Subscribers { get; }
        private readonly object padLock = new object();

        public Topic(string id, string name)
        {
            Id = id;
            Name = name;
            Messages = new();
            Subscribers = new();
        }

        public void AddMessage(string message)
        {
            lock (padLock)
            {
                Messages.Add(message);
            }
        }

        public void AddSubscriber(TopicSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }
    }
}