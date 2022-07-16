using job_offer.Postulations.Command.Application.Dtos;
using NServiceBus;
using System.Threading.Tasks;

namespace job_offer.Postulations.Command.Application.Abstractions
{
    public interface IPostulationApplicationService
    {
        void SetMessageSession(IMessageSession messageSession);
        Task<PerformPostulationResponseDto> PerformPostulation(PerformPostulationRequestDto dto);
    }
}