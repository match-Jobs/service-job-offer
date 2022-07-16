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
    public class RejectPostulationHandler : IHandleMessages<RejecPostulation>
    {
        static readonly ILog log = LogManager.GetLogger<RejectPostulationHandler>();

        public async Task Handle(RejecPostulation rejecPostulation, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"RejectPostulationHandler, PostulationId = {rejecPostulation.PostulationId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var postulationId = PostulationId.FromExisting(rejecPostulation.PostulationId);
                var postulation = nHibernateSession.Get<Postulation>(postulationId) ?? Postulation.NonExisting();
                if (postulation.DoesNotExist())
                {
                    return;
                }
                postulation.Rejec();
                postulation.ChangeUpdatedAt();
                nHibernateSession.Save(postulation);
                var postulationRejected = new PostulationRejected
                (
                    rejecPostulation.PostulationId
                );
                await context.Publish(postulationRejected);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}