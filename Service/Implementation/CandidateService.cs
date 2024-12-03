using Domain;
using Domain.Model;
using Domain.ViewModel;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository) => _candidateRepository = candidateRepository;

        public async Task<ResponseModal> DeleteAsync(int id)
        {
            var candidate = await _candidateRepository.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return new() { ResponseType = ResponseType.Failed, Message = "Candidate not found." };
            }

            await _candidateRepository.DeleteAsync(candidate);
            return new() { ResponseType = ResponseType.Success };
        }

        public async Task<ResponseModal<CandidateViewModel>> GetCandidateByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return new() { ResponseType = ResponseType.Failed, Message = "Candidate not found." };
            }

            return new ResponseModal<CandidateViewModel>()
            {
                ResponseType = ResponseType.Success,
                Data = new CandidateViewModel()
                {
                    Id = candidate.Id,
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    PhoneNumber = candidate.PhoneNumber,
                    Email = candidate.Email,
                    TimeInterval = candidate.TimeInterval,
                    LinkdinProfile = candidate.LinkdinProfile,
                    GithubProfile = candidate.GithubProfile,
                    Comment = candidate.Comment
                }
            };
        }

        public async Task<ResponseModal> UpsertAsync(CandidateViewModel model)
        {
            var candidate = new Candidate()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                TimeInterval = model.TimeInterval,
                LinkdinProfile = model.LinkdinProfile,
                GithubProfile = model.GithubProfile,
                Comment = model.Comment
            };

            await _candidateRepository.UpsertAsync(candidate);
            return new ResponseModal() { ResponseType = ResponseType.Success };
        }
    }
}
