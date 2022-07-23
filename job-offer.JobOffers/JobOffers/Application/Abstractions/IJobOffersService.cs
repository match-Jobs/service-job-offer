using job_offer.JobOffers.JobOffers.Command.Application.Dtos;
using NServiceBus;
using System.Threading.Tasks;

namespace job_offer.JobOffers.JobOffers.Command.Application.Abstractions
{
    public interface IJobOffersApplicationService
    {
        void SetMessageSession(IMessageSession messageSession);
        Task<PerformJobOfferResponseDto> PerformJobOffer(PerformJobOfferRequestDto dto);
    }
}