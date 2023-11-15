using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Teacher
    {
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public DateTime HireDate;
        public decimal Salary;
    }
    public class CourseViewModel
    {
        public int ClassId;
        public string ClassCode;
        public string ClassName;
    }
    public class TeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }

}