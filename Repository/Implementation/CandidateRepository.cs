using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Implementation
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IMemoryCache _cache;
        private readonly ApplicationDbContext _context;

        public CandidateRepository(IMemoryCache cache, ApplicationDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task SaveAsync(Candidate candidate)
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

        public async Task<IEnumerable<Candidate>> GetAllAsync(int pageNumber, int rowPerPage)
        {
            int skip = (pageNumber - 1) * rowPerPage;
            return await _context.Candidate
                .AsNoTracking()
                .Skip(skip)
                .Take(rowPerPage)
                .ToListAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Update(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate?> GetAsync(Expression<Func<Candidate, bool>> query)
        {
            return await _context.Candidate
                .Where(query)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EmailExistsAsync(int id, string email)
        {
            return await _context.Candidate
                .AsNoTracking()
                .AnyAsync(x => x.Id != id && x.Email == email);
        }

        /***
         * The in memory cache may not suitable for all cases. 
         * Not suitable for distributed systems or multi-server setups since each instance will have its own isolated cache.
         * Large caches can affect application performance and lead to out-of-memory errors, especially if the server also handles other tasks
         * 
         * If we have a distributed system Radis Cache server may be suitable option
         * Redis is a centralized, shared cache, making it ideal for distributed systems or applications running on multiple servers
         * Cache data needs to survive app restarts.
         */
        public async Task<Candidate?> GetUsingCache(int id)
        {
            return await _cache.GetOrCreateAsync("CandidateCacheKey", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _context.Candidate.FirstOrDefaultAsync(x => x.Id == id);
            });
        }
    }
}
