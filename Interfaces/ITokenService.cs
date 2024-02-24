using dating_backend.Entities;

namespace dating_backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
