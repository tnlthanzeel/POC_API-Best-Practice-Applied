using Ardalis.Specification.EntityFrameworkCore;

namespace VPMS.Persistence.Repositories;

public abstract class BaseRepository
{
    protected readonly VPMSDbContext _dbContext;

    public BaseRepository(VPMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _dbContext.SaveChangesAsync(token);
    }
}

