using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Events
{
    public class PostulationSentHandler : IHandleMessages<PostulationPending>
    {
        static readonly ILog log = LogManager.GetLogger<PostulationSentHandler>();

        public Task Handle(PostulationPending postulationPending, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"PostulationSentHandler, PostulationId = {postulationPending.PostulationId}");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
            return Task.CompletedTask;
        }
    }
}