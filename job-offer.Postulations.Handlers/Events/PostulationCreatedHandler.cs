using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Events
{
    public class PostulationCreatedHandler : IHandleMessages<PostulationCreated>
    {
        static readonly ILog log = LogManager.GetLogger<PostulationCreatedHandler>();

        public Task Handle(PostulationCreated postulationCreated, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"PostulationCreatedHandler, PostulationId = {postulationCreated.PostulationId}");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
            return Task.CompletedTask;
        }
    }
}