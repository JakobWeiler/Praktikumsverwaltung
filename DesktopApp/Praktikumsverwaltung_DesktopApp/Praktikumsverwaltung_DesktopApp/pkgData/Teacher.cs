using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class Teacher : Person
    {
        public bool IsAdmin { get; set; }

        public Teacher(string id, string firstname, string lastname, string email, string username, string password, bool isActive, bool isAdmin)
        : base(id, firstname, lastname, email, username, password, isActive)
        {
            IsAdmin = isAdmin;
        }
    }
}
