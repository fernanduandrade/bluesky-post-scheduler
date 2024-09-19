using AtScheduler.Contracts.Users;
using Microsoft.EntityFrameworkCore;

namespace AtScheduler.Persistence.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _dtSet;

    public UserRepository(AppDbContext context)
    {
        _dtSet = context.Set<User>();
    }

    public void Add(User user)
        => _dtSet.Add(user);

    public async Task<User> FindByDidAsync(string did)
    => await _dtSet.FirstOrDefaultAsync(u => u.Did == did);

    public async Task<User> GetByIdAsync(int id)
     => await _dtSet.FirstOrDefaultAsync(u => u.Id == id);
}