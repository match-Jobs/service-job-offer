using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class StartPostulation : ICommand
    {
        public string PostulationId { get; private set; }
        public string CodOfferer { get; private set; }
        public string CodOffer { get; private set; }

        public StartPostulation(string postulationId, string codOfferer, string codOffer)
        {
            PostulationId = postulationId;
            CodOfferer = codOfferer;
            CodOffer = codOffer;
        }
    }
}