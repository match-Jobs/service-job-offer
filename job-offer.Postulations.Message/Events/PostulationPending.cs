using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class PostulationPending : IEvent
    {

        public string PostulationId { get; protected set; }

        public PostulationPending(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}