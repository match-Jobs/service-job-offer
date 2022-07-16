using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class WithVacancies : ICommand
    {
        public string PostulationId { get; private set; }
        public string JobOfferId { get; protected set; }
        public string OffererId { get; protected set; }

        public WithVacancies(string postulationId, string jobOfferId, string offererId)
        {
            PostulationId = postulationId;
            JobOfferId = jobOfferId;
            OffererId = offererId;
        }
    }
}