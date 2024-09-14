using AtScheduler.Common.Interfaces;
using AtScheduler.Persistence.Data;

namespace AtScheduler.Persistence.Common;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}