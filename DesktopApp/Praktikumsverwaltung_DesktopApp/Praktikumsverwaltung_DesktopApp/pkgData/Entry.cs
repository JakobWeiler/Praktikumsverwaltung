using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class Entry
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public bool AllowedTeacher { get; set; }
        public bool AllowedAv { get; set; }

        public Entry(DateTime startDate, DateTime endDate, string title, string description, int salary)
        {
            StartDate = startDate;
            EndDate = endDate;
            Title = title;
            Description = description;
            Salary = salary;
            AllowedTeacher = true;
            AllowedAv = true;
        }
    }
}
