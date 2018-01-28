using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumberOfEmployees { get; set; }
        public string ContactPerson { get; set; }

        public Company(string id, string name, string location, int numberOfEmployees, string contactPerson)
        {
            Id = id;
            Name = name;
            Location = location;
            NumberOfEmployees = numberOfEmployees;
            ContactPerson = contactPerson;
        }
    }
}
