using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context) => _context = context;

        public async Task UpsertAsync(Candidate candidate)
        {
            _context.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Candidate candidate)
        {
            _context.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id) => await _context.Candidate
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
