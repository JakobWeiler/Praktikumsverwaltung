using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class Pupil
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public Pupil(string firstname, string lastname, string email, string username, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Username = username;
            Password = password;
            IsActive = true;
        }
    }
}
