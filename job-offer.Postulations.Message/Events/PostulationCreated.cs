using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class PostulationCreated : IEvent
    {
        public string PostulationId { get; protected set; }
        public string JobOfferId { get; protected set; }
        public string OffererId { get; protected set; }

        public PostulationCreated(string postulationId, string jobOfferId, string offererId)
        {
            PostulationId = postulationId;
            JobOfferId = jobOfferId;
            OffererId = offererId;
        }
    }
}