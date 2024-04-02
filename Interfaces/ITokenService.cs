using dating_backend.Entities;

namespace dating_backend.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
