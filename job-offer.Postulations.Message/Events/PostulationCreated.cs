using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class PostulationCreated : IEvent
    {

        public string PostulationId { get; protected set; }

        public PostulationCreated(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}