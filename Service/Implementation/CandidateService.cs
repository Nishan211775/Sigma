using Domain;
using Domain.Model;
using Domain.ViewModel;
using Repository.Interface;
using Service.Interface;
using System.Xml.Linq;

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
                return new() { ResponseType = ResponseType.Failed, Message = "Candidate not found." };

            await _candidateRepository.DeleteAsync(candidate);
            return new() { ResponseType = ResponseType.Success };
        }

        public async Task<ResponseModal<IEnumerable<CandidateViewModel>>> GetAllAsync(int pageNumber, int rowPerPage)
        {
            var paginatedList = await _candidateRepository.GetAllAsync(pageNumber, rowPerPage);
            if (paginatedList == null)
                return new() { ResponseType = ResponseType.Failed };

            // If we need to show total count and calculate page number properly we need to send total row count to ui
            return new()
            {
                Data = paginatedList.Select(x => new CandidateViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    TimeInterval = x.TimeInterval,
                    LinkdinProfile = x.LinkdinProfile,
                    GithubProfile = x.GithubProfile,
                    Comment = x.Comment
                }).ToList(),
                ResponseType = ResponseType.Success,
            };
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

        public async Task<ResponseModal<CandidateViewModel?>> GetUsingCacheAsync(int id)
        {
            var candidate = await _candidateRepository.GetUsingCache(id);
            if (candidate == null)
            {
                return new() { ResponseType = ResponseType.Failed, Message = "Candidate not found." };
            }

            return new()
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

        public async Task<ResponseModal> SaveAsync(CandidateViewModel model)
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

            if (await _candidateRepository.EmailExistsAsync(candidate.Id, candidate.Email))
            {
                return new() { Message = "Email address already exists", ResponseType = ResponseType.Failed };
            }

            await _candidateRepository.SaveAsync(candidate);
            return new ResponseModal() { ResponseType = ResponseType.Success };
        }

        public async Task<ResponseModal> UpdateAsync(CandidateViewModel model)
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

            if (await _candidateRepository.EmailExistsAsync(candidate.Id, candidate.Email))
            {
                return new() { Message = "Email address already exists", ResponseType = ResponseType.Failed };
            }

            await _candidateRepository.UpdateAsync(candidate);
            return new ResponseModal() { ResponseType = ResponseType.Success };
        }
    }
}
