namespace MessageBrokerLLD
{
    public class Broker
    {
        // Key - Topic Id, Value - Topic Handler
        private readonly Dictionary<string, TopicHandler> topicHandlers;

        public Broker()
        {
            this.topicHandlers = new();
        }

        public Topic CreateTopic(string name)
        {
            Topic topic = new Topic(Guid.NewGuid().ToString(), name);
            this.topicHandlers.Add(topic.Id, new TopicHandler(topic));
            Console.WriteLine("Topic {0} created successfully.", name);
            return topic;
        }

        public void Subscribe(Topic topic, TopicSubscriber subscriber)
        {
            topic.AddSubscriber(subscriber);
        }

        public void Publish(Topic topic, string message)
        {
            topic.AddMessage(message);
            // Task.Run(() => topicHandlers[topic.Id].Publish());
            Task.Run(topicHandlers[topic.Id].Publish);
        }
    }
}