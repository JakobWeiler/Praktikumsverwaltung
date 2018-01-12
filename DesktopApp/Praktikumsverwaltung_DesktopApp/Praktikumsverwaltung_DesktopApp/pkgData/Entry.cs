﻿using MongoDB.Bson;
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
        public string Id { get; set; }
        public DateTime StartDate { get; set; }           // DateTime as String because otherwise deserialization causes problems
        public DateTime EndDate { get; set; }
        public float Salary { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AllowedTeacher { get; set; }
        public bool AllowedAV { get; set; }
        public string IdPupil { get; set; }
        public string IdCompany { get; set; }
        public string IdClass { get; set; }

        public Entry(string id, DateTime startDate, DateTime endDate, string title, string description, float salary, bool allowedTeacher, bool allowedAV, string idPupil, string idClass, string idCompany)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Title = title;
            Description = description;
            Salary = salary;
            AllowedTeacher = allowedTeacher;
            AllowedAV = allowedAV;
            IdPupil = idPupil;
            IdCompany = idCompany;
            IdClass = idClass;
        }
    }
}
