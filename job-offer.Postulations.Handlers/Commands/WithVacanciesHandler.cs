using job_offer.Postulations.Command.DomainModel.Entities;
using job_offer.Common.DomainModel.ValueObjects;
using job_offer.JobOffers.Command.DomainModel.Entities;
using job_offer.Postulations.Command.DomainModel.Enums;
using job_offer.Postulations.Command.DomainModel.ValueObjects;
using job_offer.Postulations.Messages.Commands;
using job_offer.Postulations.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using job_offer.JobOffers.Command.DomainModel.ValueObjects;
using job_offer.Offerers.Command.DomainModel.ValueObjects;
using job_offer.Offerers.Command.DomainModel.Entities;

namespace job_offer.Postulations.Handlers.Commands
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
                        withVacancies.PostulationId,
                        withVacancies.JobOfferId,
                        withVacancies.OffererId


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