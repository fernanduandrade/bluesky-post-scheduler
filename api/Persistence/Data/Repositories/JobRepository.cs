using AtScheduler.Contracts.Jobs;
using Microsoft.EntityFrameworkCore;

namespace AtScheduler.Persistence.Data.Repositories;

public class JobRepository : IJobRepository
{
    private readonly DbSet<Job> _dbSet;

    public JobRepository(AppDbContext context)
    {
        _dbSet = context.Set<Job>();
    }
    
    public void Add(Job job)
    => _dbSet.Add(job);
    
    public async Task<Job> GetBySchedulerId(int id)
        => await _dbSet.FirstOrDefaultAsync(x => x.SchedulerId == id);
}