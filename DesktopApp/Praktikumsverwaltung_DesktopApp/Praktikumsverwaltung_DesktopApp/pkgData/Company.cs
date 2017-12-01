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
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("numberOfEmployees")]
        public float NumberOfEmployees { get; set; }
        [BsonElement("contactPerson")]
        public string ContactPerson { get; set; }

        public Company(string name, string location, int numberOfEmployees, string contactPerson)
        {
            Name = name;
            Location = location;
            NumberOfEmployees = numberOfEmployees;
            ContactPerson = contactPerson;
        }
    }
}
