using NServiceBus;

namespace job_offer.JobOffers.Messages.Events
{
    public class JobOfferCreated : IEvent
    {
        public string JobOfferId { get; protected set; }

        public JobOfferCreated(string jobOfferId)
        {
            JobOfferId = jobOfferId;
        }
    }
}