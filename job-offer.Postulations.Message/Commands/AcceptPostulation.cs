using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class AcceptPostulation : ICommand
    {
        public string PostulationId { get; private set; }

        public AcceptPostulation(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}