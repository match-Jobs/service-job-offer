using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Events
{
    public class PostulationRejectedHandler : IHandleMessages<PostulationRejected>
    {
        static readonly ILog log = LogManager.GetLogger<PostulationRejectedHandler>();

        public Task Handle(PostulationRejected postulationRejected, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"PostulationRejectedHandler, PostulationId = {postulationRejected.PostulationId}");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
            return Task.CompletedTask;
        }
    }
}