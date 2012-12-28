using nQA.Model.Entities;

namespace nQA.Model.Interfaces
{
    public interface IUserService
    {
        User Login(string claimedIdentifier, string nickname, string fullName, string email);
    }
}