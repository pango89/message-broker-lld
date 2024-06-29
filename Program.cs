using MessageBrokerLLD;

public class Program
{
    public static void Main(string[] args)
    {
        Broker broker = new();

        Topic t1 = broker.CreateTopic("T1");
        Topic t2 = broker.CreateTopic("T2");

        TopicSubscriber ts1 = new TopicSubscriber("TS1");
        TopicSubscriber ts2 = new TopicSubscriber("TS2");
        TopicSubscriber ts3 = new TopicSubscriber("TS3");
        TopicSubscriber ts4 = new TopicSubscriber("TS4");
        TopicSubscriber ts5 = new TopicSubscriber("TS5");
        TopicSubscriber ts6 = new TopicSubscriber("TS6");

        broker.Subscribe(t1, ts1);
        broker.Subscribe(t1, ts2);
        broker.Subscribe(t1, ts6);

        broker.Subscribe(t2, ts3);
        broker.Subscribe(t2, ts4);
        broker.Subscribe(t2, ts5);

        for (int i = 1; i <= 5; i++)
        {
            broker.Publish(t1, $"Message {i}");
            broker.Publish(t2, $"Message {i}");
        }

        // broker.Publish(t1, "Message 1-1");
        // broker.Publish(t1, "Message 1-2");
        // broker.Publish(t2, "Message 2-1");

        // Task.Run() spawns Thread Pool threads which are background threads. Background threads 
        // will terminate as soon as the main thread ends. So any work which they were doing will 
        // end abruptly if main thread ends. So it is necessary to put a sleep on main thread to 
        // let the other threads finish their work.
        Thread.Sleep(10000);

        Console.WriteLine("Main End");
    }
}