using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class RejecPostulation : ICommand
    {
        public string PostulationId { get; private set; }

        public RejecPostulation(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}