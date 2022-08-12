using NServiceBus;

namespace job_offer.JobOffers.Messages.Events
{
    public class WithVacanciesOk : IEvent
    {
        public string PostulationId { get; private set; }

        public WithVacanciesOk(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}