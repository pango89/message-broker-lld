namespace MessageBrokerLLD
{
    public class SubscriberWorker
    {
        private readonly Topic topic;
        private readonly TopicSubscriber topicSubscriber;

        public SubscriberWorker(Topic topic, TopicSubscriber topicSubscriber)
        {
            this.topic = topic;
            this.topicSubscriber = topicSubscriber;
        }

        public void Run()
        {
            lock (topicSubscriber)
            {
                do
                {
                    int currOffset = topicSubscriber.Offset;
                    while (currOffset >= topic.Messages.Count)
                    {
                        Monitor.Wait(topicSubscriber);
                    }

                    string message = topic.Messages[currOffset];
                    topicSubscriber.Consume(message);
                    // TODO: Interlocked
                    topicSubscriber.Offset += 1;
                } while (true);
            }
        }

        public void WakeUp()
        {
            lock (topicSubscriber)
            {
                Monitor.Pulse(topicSubscriber);
            }
        }
    }
}