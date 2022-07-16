using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class WithVacanciesRejected : IEvent
    {
        public string PostulationId { get; protected set; }

        public WithVacanciesRejected(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}