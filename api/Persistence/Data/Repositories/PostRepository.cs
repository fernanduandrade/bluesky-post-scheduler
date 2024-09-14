using System.Linq.Expressions;
using AtScheduler.Contracts.Posts;
using Microsoft.EntityFrameworkCore;

namespace AtScheduler.Persistence.Data.Repositories;

public class PostRepository(AppDbContext context) : IPostRepository
{

    private readonly DbSet<Post> _dbSet = context.Set<Post>();

    public IQueryable<Post> Get(Expression<Func<Post, bool>> filter = null)
    {
        var query = _dbSet.AsQueryable();

        if (filter is not null)
            query = query.Where(filter);
        
        return query;
    }

    public async Task<Post?> GetByIdAsync(int id)
    => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public void Add(Post post)
    => _dbSet.Add(post);

    public async Task Delete(int id)
    {
        var scheduler = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        _dbSet.Remove(scheduler);
    }

    public void DeleteEntity(Post post)
        => _dbSet.Remove(post);

    public void Update(Post post)
    => _dbSet.Update(post);
}