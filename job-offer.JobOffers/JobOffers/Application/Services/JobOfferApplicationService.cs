using job_offer.JobOffers.Messages.Commands;
using job_offer.JobOffers.JobOffers.Command.Application.Abstractions;
using job_offer.JobOffers.JobOffers.Command.Application.Dtos;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace job_offer.JobOffers.JobOffers.Command.Application.Services
{
    public class JobOfferApplicationService : IJobOffersApplicationService
    {
        private IMessageSession _messageSession;

        public JobOfferApplicationService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public void SetMessageSession(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task<PerformJobOfferResponseDto> PerformJobOffer(PerformJobOfferRequestDto performJobOfferRequestDto)
        {
            try
            {
                var jobOfferId = Guid.NewGuid().ToString();
                var performJobOffer = new CreateJobOffer(
                    jobOfferId,
                    performJobOfferRequestDto.CodOffer
                );
                await _messageSession.Send(performJobOffer).ConfigureAwait(false);
                return new PerformJobOfferResponseDto
                {
                    Response = "OK"
                };
            }
            catch (Exception ex)
            {
                return new PerformJobOfferResponseDto
                {
                    Response = "ERROR: " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }
    }
}