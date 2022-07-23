using job_offer.JobOffers.JobOffers.Command.Application.Abstractions;
using job_offer.JobOffers.JobOffers.Command.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace job_offer.JobOffers.API.Controllers.JobOffer
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("job-offer")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOffersApplicationService _jobOfferApplicationService;

        public JobOfferController(IJobOffersApplicationService jobOfferApplicationService)
        {
            _jobOfferApplicationService = jobOfferApplicationService;
        }

        [HttpPost("joboffer")]
        public async Task<IActionResult> PerformTransfer([FromBody] PerformJobOfferRequestDto performJobOfferRequestDto)
        {
            try
            {
                PerformJobOfferResponseDto response = await _jobOfferApplicationService.PerformJobOffer(performJobOfferRequestDto);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}