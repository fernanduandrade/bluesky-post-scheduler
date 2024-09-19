namespace AtScheduler.Contracts.Users;

public interface IUserRepository
{
    void Add(User user);
    Task<User> FindByDidAsync(string did);
    Task<User> GetByIdAsync(int id);
}