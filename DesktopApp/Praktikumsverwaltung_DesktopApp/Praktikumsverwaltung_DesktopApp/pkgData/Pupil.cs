using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class Pupil : Person
    {
        public string IdDepartment { get; set; }
        public string IdClass { get; set; }                

        public Pupil(string id, string firstname, string lastname, string email, string username, string password, bool isActive, string idDepartment, string idClass)
        : base(id, firstname, lastname, email, username, password, isActive)
        {
            IdDepartment = idDepartment;
            IdClass = idClass;
        }
    }
}
