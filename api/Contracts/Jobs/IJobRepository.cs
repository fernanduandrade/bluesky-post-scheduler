namespace AtScheduler.Contracts.Jobs;

public interface IJobRepository
{
    void Add(Job job);
    Task<Job> GetBySchedulerId(int id);
}