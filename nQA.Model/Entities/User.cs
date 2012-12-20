using System;

namespace nQA.Model.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string FullName { get; set; }

        public bool Disabled { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        public bool ShowDetails { get; set; }

        public string Website { get; set; }

        public string ShortDescription { get; set; }
    }
}