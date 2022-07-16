using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class PostulationRejected : IEvent
    {
        public string PostulationId { get; protected set; }

        public PostulationRejected(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}