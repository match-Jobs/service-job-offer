using NServiceBus;

namespace job_offer.Postulations.Messages.Events
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