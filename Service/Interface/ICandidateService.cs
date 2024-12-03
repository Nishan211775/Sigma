using Domain;
using Domain.ViewModel;

namespace Service.Interface
{
    public interface ICandidateService
    {
        Task<ResponseModal> UpsertAsync(CandidateViewModel model);
        Task<ResponseModal> DeleteAsync(int id);
        Task<ResponseModal<CandidateViewModel>> GetCandidateByIdAsync(int id);
    }
}
