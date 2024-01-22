using BillManager.Model;

namespace BillManager.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Company user, IEnumerable<string> roles);
    }
}
