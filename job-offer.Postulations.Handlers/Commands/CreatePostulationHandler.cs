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
    public class CreatePostulationHandler : IHandleMessages<CreatePostulation>
    {
        static readonly ILog log = LogManager.GetLogger<CreatePostulationHandler>();

        public async Task Handle(CreatePostulation createPostulation, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"CreatePostulation, PostulationId = {createPostulation.PostulationId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var postulationId = PostulationId.FromExisting(createPostulation.PostulationId);
                var postulation = nHibernateSession.Get<Postulation>(postulationId) ?? Postulation.NonExisting();
                if (postulation.DoesNotExist())
                {
                    return;
                }
                postulation.Send();
                postulation.ChangeUpdatedAt();
                nHibernateSession.Save(postulation);
                var postulationCreated = new PostulationCreated
                (
                    createPostulation.PostulationId
                );
                await context.Publish(postulationCreated);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}