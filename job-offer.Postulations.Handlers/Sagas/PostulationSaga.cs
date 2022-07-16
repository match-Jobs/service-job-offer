using job_offer.Postulations.Messages.Commands;
using job_offer.Postulations.Messages.Events;
using job_offer.Postulations.Messages.Sagas;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Handlers.Sagas
{
    public class PostulationSaga :
        Saga<PostulationSagaData>,
        IAmStartedByMessages<PostulationCreated>,
        IHandleMessages<WithVacanciesOk>,
        IHandleMessages<WithVacanciesRejected>,
        IHandleMessages<JobOfferNotFound>
    {
        static readonly ILog log = LogManager.GetLogger<PostulationSaga>();
        private readonly ILogger<PostulationSaga> _logger;

        public PostulationSaga(ILogger<PostulationSaga> logger)
            => _logger = logger;

        public async Task Handle(PostulationCreated postulationCreated, IMessageHandlerContext context)
        {
            try 
            {
                _logger.LogInformation($"Saga PostulationCreated, PostulationId = {postulationCreated.PostulationId}");
                Data.PostulationId = postulationCreated.PostulationId;
                Data.JobOfferId = postulationCreated.JobOfferId;
                Data.OffererId = postulationCreated.OffererId;
                var withdraw = new WithVacancies(
                    Data.PostulationId,
                    Data.JobOfferId,
                    Data.OffererId
                );
                await context.SendLocal(withdraw).ConfigureAwait(false);
            } 
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }

        public async Task Handle(WithVacanciesOk withVacanciesOk, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"Saga WithVacanciesOk, PostulationId = {withVacanciesOk.PostulationId}");
                var sendPostulation = new SendPostulation(
                    Data.PostulationId
                );
                await context.SendLocal(sendPostulation).ConfigureAwait(false);
                MarkAsComplete();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }

        public async Task Handle(WithVacanciesRejected withVacanciesRejected, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"Saga WithVacanciesRejected, PostulationId = {withVacanciesRejected.PostulationId}");
                var rejecPostulation = new RejecPostulation(
                    Data.PostulationId
                );
                await context.SendLocal(rejecPostulation).ConfigureAwait(false);
                MarkAsComplete();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }

        public async Task Handle(JobOfferNotFound jobOfferNotFound, IMessageHandlerContext context)
        {
            try
            {
                log.Info($"Saga JobOfferNotFound, PostulationId = {jobOfferNotFound.PostulationId}");
                var rejecPostulation = new RejecPostulation(
                    Data.PostulationId
                );
                await context.SendLocal(rejecPostulation).ConfigureAwait(false);
                MarkAsComplete();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " ** " + ex.StackTrace);
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PostulationSagaData> mapper)
        {
            mapper.ConfigureMapping<PostulationCreated>(message => message.PostulationId)
                .ToSaga(sagaData => sagaData.PostulationId);

            mapper.ConfigureMapping<WithVacanciesOk>(message => message.PostulationId)
                .ToSaga(sagaData => sagaData.PostulationId);

            mapper.ConfigureMapping<WithVacanciesRejected>(message => message.PostulationId)
                .ToSaga(sagaData => sagaData.PostulationId);

            mapper.ConfigureMapping<JobOfferNotFound>(message => message.PostulationId)
                .ToSaga(sagaData => sagaData.PostulationId);

        }

    }
}