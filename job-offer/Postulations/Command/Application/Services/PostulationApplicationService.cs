using job_offer.Postulations.Command.Application.Abstractions;
using job_offer.Postulations.Command.Application.Dtos;
using job_offer.Postulations.Messages.Commands;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace job_offer.Postulations.Command.Application.Services
{
    public class PostulationApplicationService : IPostulationApplicationService
    {
        private IMessageSession _messageSession;

        public PostulationApplicationService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public void SetMessageSession(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task<PerformPostulationResponseDto> PerformPostulation(PerformPostulationRequestDto performPostulationRequestDto)
        {
            try
            {
                var postulationId = Guid.NewGuid().ToString();
                var performPostulation = new CreatePostulation(
                    postulationId,
                    performPostulationRequestDto.CodOfferer,
                    performPostulationRequestDto.CodOffer
                );
                await _messageSession.Send(performPostulation).ConfigureAwait(false);
                return new PerformPostulationResponseDto
                {
                    Response = "OK"
                };
            }
            catch (Exception ex)
            {
                return new PerformPostulationResponseDto
                {
                    Response = "ERROR: " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }
    }
}