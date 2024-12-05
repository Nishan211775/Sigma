using Domain;
using Domain.Model;
using Domain.ViewModel;

namespace Service.Interface
{
    public interface ICandidateService
    {
        Task<ResponseModal> SaveAsync(CandidateViewModel model);
        Task<ResponseModal> UpdateAsync(CandidateViewModel model);
        Task<ResponseModal> DeleteAsync(int id);
        Task<ResponseModal<CandidateViewModel>> GetCandidateByIdAsync(int id);
        Task<ResponseModal<IEnumerable<CandidateViewModel>>> GetAllAsync(int pageNumber, int rowPerPage);

        Task<ResponseModal<CandidateViewModel?>> GetUsingCacheAsync(int id);
    }
}
