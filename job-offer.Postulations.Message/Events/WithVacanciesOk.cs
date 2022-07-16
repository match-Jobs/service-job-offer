using NServiceBus;

namespace job_offer.Postulations.Messages.Events
{
    public class WithVacanciesOk : IEvent
    {
        public string PostulationId { get; private set; }
        public string CodOfferer { get; private set; }
        public string CodOffer { get; private set; }

        public WithVacanciesOk(string postulationId, string codOfferer, string codOffer)
        {
            PostulationId = postulationId;
            CodOfferer = codOfferer;
            CodOffer = codOffer;
        }
    }
}