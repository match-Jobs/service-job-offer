using NServiceBus;

namespace job_offer.Postulations.Messages.Commands
{
    public class CreatePostulation : ICommand
    {
        public string PostulationId { get; private set; }

        public CreatePostulation(string postulationId)
        {
            PostulationId = postulationId;
        }
    }
}