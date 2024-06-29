
namespace MessageBrokerLLD
{
    public class TopicHandler
    {
        private readonly Topic topic;

        // Key - Subscriber Id, Value - Worker
        private readonly Dictionary<string, SubscriberWorker> subscriberWorkers;

        public TopicHandler(Topic topic)
        {
            this.topic = topic;
            this.subscriberWorkers = new();
        }

        public void Publish()
        {
            foreach (TopicSubscriber subscriber in topic.Subscribers)
            {
                TriggerWorkerForSubscriber(subscriber);
            }
        }

        public void TriggerWorkerForSubscriber(TopicSubscriber subscriber)
        {
            if (!this.subscriberWorkers.ContainsKey(subscriber.Id))
            {
                SubscriberWorker worker = new SubscriberWorker(topic, subscriber);
                this.subscriberWorkers.Add(subscriber.Id, worker);
                // new System.Threading.Thread(subscriberWorker.Run).Start();
                Task.Run(worker.Run);
            }

            this.subscriberWorkers[subscriber.Id].WakeUp();
        }
    }
}