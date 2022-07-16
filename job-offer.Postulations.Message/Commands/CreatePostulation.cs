using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class CreatePostulation : ICommand
    {
        public string PostulationId { get; private set; }
        public string CodOfferer { get; private set; }
        public string CodOffer { get; private set; }

        public CreatePostulation(string postulationId, string codOfferer, string codOffer)
        {
            PostulationId = postulationId;
            CodOfferer = codOfferer;
            CodOffer = codOffer;
        }
    }
}