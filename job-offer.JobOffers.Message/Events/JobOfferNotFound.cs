using NServiceBus;

namespace job_offer.JobOffers.Messages.Events
{
    public class JobOfferNotFound : IEvent
    {
        public string PostulationId { get; protected set; }

        public JobOfferNotFound(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}