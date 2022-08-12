using job_offer.JobOffers.Command.DomainModel.Entities;
using job_offer.JobOffers.Messages.Commands;
using job_offer.JobOffers.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;
using job_offer.JobOffers.Offerers.Command.DomainModel.Entities;
using System;
using job_offer.JobOffers.Offerers.Command.DomainModel.ValueObjects;
using job_offer.JobOffers.Command.DomainModel.ValueObjects;

namespace job_offer.JobOffers.Handlers.Commands
{
    public class WithVacanciesHandler : IHandleMessages<WithVacancies>
    {
        static readonly ILog log = LogManager.GetLogger<WithVacanciesHandler>();

        public async Task Handle(WithVacancies withVacancies, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"WithVacanciesHandler, PostulationId = {withVacancies.PostulationId}");
                var nHibernateSession = context.SynchronizedStorageSession.Session();
                var jobOfferId = JobOfferId.FromExisting(withVacancies.JobOfferId);
                var jobOffer = nHibernateSession.Get<JobOffer>(jobOfferId) ?? JobOffer.NonExisting();

                var offererId = OffererId.FromExisting(withVacancies.OffererId);
                var offerer = nHibernateSession.Get<Offerer>(offererId) ?? Offerer.NonExisting();
                if (jobOffer.DoesNotExist())
                {
                    var jobOfferNotFound = new JobOfferNotFound(withVacancies.PostulationId);
                    await context.Publish(jobOfferNotFound);
                    return;
                }

                if (jobOffer.Available())
                {
                    jobOffer.Vacancie.AddVacancie();
                    jobOffer.ChangeUpdatedAt();
                    nHibernateSession.Save(jobOffer);
                    var withVacanciesOk = new WithVacanciesOk
                    (
                        withVacancies.PostulationId


                    );
                    await context.Publish(withVacanciesOk);
                    return;
                }
                var withVacanciesRejected = new WithVacanciesRejected
                (
                    withVacancies.PostulationId
                );
                await context.Publish(withVacanciesRejected);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }
    }
}