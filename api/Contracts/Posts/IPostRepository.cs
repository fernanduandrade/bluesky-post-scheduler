using System.Linq.Expressions;

namespace AtScheduler.Contracts.Posts;

public interface IPostRepository
{
    IQueryable<Post> Get(Expression<Func<Post, bool>> filter = null);
    Task<Post?> GetByIdAsync(int id);
    void Add(Post post);
    Task Delete(int id);
    void DeleteEntity(Post post);
    void Update(Post post);
}