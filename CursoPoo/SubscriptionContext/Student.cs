using Balta.NotificationContext;
using Balta.SharedContext;

namespace Balta.SubscriptionContext
{
    internal class Student : Base
    {
        public Student() 
        {
            Subscriptions = new List<Subscription>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public Users User { get; set; }
        public IList<Subscription> Subscriptions { get; set; }
        public void CreateSubscription(Subscription subscription)
        {
            if (IsPremium)
                AddNotification(new Notification("Premium", "O aluno já tem uma assinatura ativa"));

            Subscriptions.Add(subscription);
        }
        public bool IsPremium => Subscriptions.Any(s => !s.IsInactive);
    }
}
