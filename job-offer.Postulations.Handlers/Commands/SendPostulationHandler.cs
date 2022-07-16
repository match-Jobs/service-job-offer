using job_offer.Postulations.Command.DomainModel.Entities;
using job_offer.Postulations.Command.DomainModel.ValueObjects;
using job_offer.Postulations.Messages.Commands;
using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Commands
{
    public class SendPostulationHandler : IHandleMessages<SendPostulation>
    {
        static readonly ILog log = LogManager.GetLogger<SendPostulationHandler>();

        public async Task Handle(SendPostulation sendPostulation, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"SendPostulation, PostulationId = {sendPostulation.PostulationId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var postulationId = PostulationId.FromExisting(sendPostulation.PostulationId);
                var postulation = nHibernateSession.Get<Postulation>(postulationId) ?? Postulation.NonExisting();
                if (postulation.DoesNotExist())
                {
                    return;
                }
                postulation.Send();
                postulation.ChangeUpdatedAt();
                nHibernateSession.Save(postulation);
                var postulationPending = new PostulationPending
                (
                    sendPostulation.PostulationId
                );
                await context.Publish(postulationPending);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}