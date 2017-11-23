using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{    
    class Entry
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }
        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
        [BsonElement("salary")]
        public float Salary { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("allowedTeacher")]
        public bool AllowedTeacher { get; set; }
        [BsonElement("allowedAV")]
        public bool AllowedAV { get; set; }
        [BsonElement("idPupil")]
        public ObjectId IdPupil { get; set; }
        [BsonElement("idCompany")]
        public ObjectId IdCompany { get; set; }
        [BsonElement("idClass")]
        public ObjectId IdClass { get; set; }

        public Entry(DateTime startDate, DateTime endDate, string title, string description, int salary)
        {
            StartDate = startDate;
            EndDate = endDate;
            Title = title;
            Description = description;
            Salary = salary;
            AllowedTeacher = true;
            AllowedAV = true;
        }
    }
}
