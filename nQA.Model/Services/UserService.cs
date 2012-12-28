using System;
using nQA.Model.Entities;
using nQA.Model.Interfaces;

namespace nQA.Model.Services
{
    public class UserService : IUserService
    {
        public IRepository<User> UserRepository { get; set; }

        public UserService(IRepository<User> userRepository)
        {
            UserRepository = userRepository;
        }
        
        public User Login(string claimedIdentifier, string nickname, string fullName, string email)
        {
            var user = UserRepository.FirstOrDefault(x => x.ClaimedIdentifier == claimedIdentifier);

            if (user == null)
            {
                user = new User().CreateUser(claimedIdentifier, nickname, fullName, email);
                UserRepository.Add(user);
                UserRepository.SaveChanges();
            }
            else
            {
                user.LastVisitDate = DateTime.Now;
                UserRepository.Update(user);
                UserRepository.SaveChanges();
            }

            return user;
        }
    }
}