using NServiceBus;

namespace job_offer.JobOffers.Messages.Commands
{
    public class CreateJobOffer : ICommand
    {
        public string JobOfferId { get; private set; }
        public string CodOffer { get; private set; }

        public CreateJobOffer(string jobOfferId, string codOffer)
        {
            JobOfferId = jobOfferId;
            CodOffer = codOffer;
        }
    }
}