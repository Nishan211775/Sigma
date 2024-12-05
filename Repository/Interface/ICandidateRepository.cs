using Domain;
using Domain.Model;
using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface ICandidateRepository
    {
        // Later this all function can be moved to base repository
        // As all the model request CRUD functionality
        Task SaveAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
        Task<Candidate?> GetCandidateByIdAsync(int id);
        Task<Candidate?> GetAsync(Expression<Func<Candidate, bool>> @query);
        Task<IEnumerable<Candidate>> GetAllAsync(int pageNumber, int rowPerPage);

        Task<bool> EmailExistsAsync(int id, string email);
        Task<Candidate?> GetUsingCache(int id);
    }
}
