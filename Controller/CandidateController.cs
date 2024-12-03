using Domain;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService) => _candidateService = candidateService;

        [HttpGet("{id}")]
        public async Task<ResponseModal<CandidateViewModel>> Get(int id)
        {
            return await _candidateService.GetCandidateByIdAsync(id);
        }

        [HttpPost]
        public async Task<ResponseModal> Post([FromBody] CandidateViewModel modal)
        {
            return await _candidateService.UpsertAsync(modal);
        }

        [HttpDelete]
        public async Task<ResponseModal> Delete(int id)
        {
            return await _candidateService.DeleteAsync(id);
        }
    }
}
