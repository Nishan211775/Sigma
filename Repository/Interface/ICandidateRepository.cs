using Domain.Model;

namespace Repository.Interface
{
    public interface ICandidateRepository
    {
        Task UpsertAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
        Task<Candidate?> GetCandidateByIdAsync(int id);
    }
}
