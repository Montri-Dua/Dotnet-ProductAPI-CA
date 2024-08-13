using ProductApi.Core.Entities;

namespace ProductApi.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}