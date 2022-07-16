using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class SendPostulation : ICommand
    {
        public string PostulationId { get; private set; }

        public SendPostulation(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}