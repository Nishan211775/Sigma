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

        [HttpGet("cache/{id}")]
        public async Task<ResponseModal<CandidateViewModel?>> GetUsingCache(int id)
        {
            return await _candidateService.GetUsingCacheAsync(id);
        }

        [HttpGet("get-all")]
        public async Task<ResponseModal<IEnumerable<CandidateViewModel>>> GetAll([FromQuery] int pageNumber, [FromQuery] int rowPerPage)
        {
            return await _candidateService.GetAllAsync(pageNumber, rowPerPage);
        }

        [HttpPost]
        public async Task<ResponseModal> Post([FromBody] CandidateViewModel modal)
        {
            return await _candidateService.SaveAsync(modal);
        }

        [HttpPut]
        public async Task<ResponseModal> Put([FromBody] CandidateViewModel modal)
        {
            return await _candidateService.UpdateAsync(modal);
        }

        [HttpDelete]
        public async Task<ResponseModal> Delete(int id)
        {
            return await _candidateService.DeleteAsync(id);
        }
    }
}
