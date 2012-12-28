using System;

namespace nQA.Model.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string FullName { get; set; }

        public string ClaimedIdentifier { get; set; }

        public bool Disabled { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        public string Website { get; set; }

        public string ShortDescription { get; set; }

        public User CreateUser(string claimedIdentifier, string nickname, string fullName, string email)
        {
            return new User()
            {
                ClaimedIdentifier = claimedIdentifier,
                Login = nickname,
                FullName = fullName,
                Email = email,
                RegistrationDate = DateTime.Now,
                LastVisitDate = DateTime.Now
            };
        }
    }
}