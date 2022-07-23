using job_offer.JobOffers.Command.DomainModel.Entities;
using job_offer.JobOffers.Command.DomainModel.ValueObjects;
using job_offer.JobOffers.Messages.Commands;
using job_offer.JobOffers.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace job_offer.JobOffers.Handlers.Commands
{
    public class CreateJobOfferHandler : IHandleMessages<CreateJobOffer>
    {
        static readonly ILog log = LogManager.GetLogger<CreateJobOfferHandler>();

        public async Task Handle(CreateJobOffer createJobOffer, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"CreateJobOfferHandler, JobOfferId = {createJobOffer.JobOfferId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var postulationId = JobOfferId.FromExisting(createJobOffer.JobOfferId);
                var jobOfferCreated = new JobOfferCreated
               (
                   createJobOffer.JobOfferId
               );
                await context.Publish(jobOfferCreated);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}