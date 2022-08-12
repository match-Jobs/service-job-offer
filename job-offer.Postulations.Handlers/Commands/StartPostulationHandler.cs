using job_offer.JobOffers.Command.DomainModel.Entities;
using job_offer.Offerers.Command.DomainModel.Entities;
using job_offer.Postulations.Command.DomainModel.Entities;
using job_offer.Postulations.Command.DomainModel.Enums;
using job_offer.Postulations.Command.DomainModel.ValueObjects;
using job_offer.Postulations.Messages.Commands;
using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Commands
{
    public class StartPostulationHandler : IHandleMessages<StartPostulation>
    {
        static readonly ILog log = LogManager.GetLogger<StartPostulationHandler>();

        public async Task Handle(StartPostulation createPostulation, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"CreatePostulationHandler, PostulationId = {createPostulation.PostulationId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var postulationId = PostulationId.FromExisting(createPostulation.PostulationId);
                var jobOffer = nHibernateSession.Query<JobOffer>().FirstOrDefault
                    (x => x.CodOffer == createPostulation.CodOffer) ?? JobOffer.NonExisting();
                if (jobOffer.DoesNotExist())
                {
                    return;
                }
                var offerer = nHibernateSession.Query<Offerer>().FirstOrDefault
                    (x => x.CodOfferer == createPostulation.CodOfferer) ?? Offerer.NonExisting();
                if (jobOffer.DoesNotExist())
                {
                    return;
                }

                var postulationState = PostulationStateId.CREATED;
                var now = DateTime.UtcNow;
                var postulation = new Postulation(
                    postulationId,
                    jobOffer.JobOfferId,
                    offerer.OffererId,
                    postulationState,
                    now,
                    now
                );
                nHibernateSession.Save(postulation);
                var postulationStarted = new PostulationStarted
                (
                    createPostulation.PostulationId,
                    jobOffer.JobOfferId.Id,
                    offerer.OffererId.Id
                );
                await context.Publish(postulationStarted);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}