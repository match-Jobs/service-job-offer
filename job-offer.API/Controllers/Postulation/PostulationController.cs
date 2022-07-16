using job_offer.Postulations.Command.Application.Abstractions;
using job_offer.Postulations.Command.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace job_offer.API.Controllers.JobOffer
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("job-offer")]
    [ApiController]
    public class PostulationController : ControllerBase
    {
        private readonly IPostulationApplicationService _postulationApplicationService;

        public PostulationController(IPostulationApplicationService postulationApplicationService)
        {
            _postulationApplicationService = postulationApplicationService;
        }

        [HttpPost("postulation")]
        public async Task<IActionResult> PerformTransfer([FromBody] PerformPostulationRequestDto performPostulationRequestDto)
        {
            try
            {
                PerformPostulationResponseDto response = await _postulationApplicationService.PerformPostulation(performPostulationRequestDto);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}