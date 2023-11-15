using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject.Models;
using MySql.Data.MySqlClient;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of Teachers including (ids, firstnames, last names, employeeNumber, hireDate and salary)
        /// </returns>
        /// 
        /// <example>
        /// 
        /// GET api/TeacherData/ListTeachers -> 
        /// 
        /// <Teacher>
        /// <EmployeeNumber>T378</EmployeeNumber>
        /// <HireDate>2016-08-05T00:00:00</HireDate>
        /// <Salary>55.30</Salary>  
        /// <TeacherFname>Alexander</TeacherFname>
        /// <TeacherId>1</TeacherId>
        /// <TeacherLname>Bennett</TeacherLname>
        /// </Teacher>
        ///
        /// <Teacher>
        /// <EmployeeNumber>T381</EmployeeNumber>
        /// <HireDate>2014-06-10T00:00:00</HireDate>
        /// <Salary>62.77</Salary>
        /// <TeacherFname>Caitlin</TeacherFname>
        /// <TeacherId>2</TeacherId>
        /// <TeacherLname>Cummings</TeacherLname>
        /// </Teacher>
        /// ....
        /// </example>
        

        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //create a command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Query
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //While Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                 //Add the Teachers to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection 
            Conn.Close();

            //Return the final list of teachers
            return Teachers;
        }


        /// <summary>
        /// Finds an teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>The information of that particular teacher (employee no, hire date, salary, teacher fname, teacher id, teacher lname) </returns>
        /// 
        /// <example>
        /// 
        /// GET api/TeacherData/ListTeachers/5 ->
        /// 
        /// <EmployeeNumber>T389</EmployeeNumber>
        /// <HireDate>2012-06-04T00:00:00</HireDate>
        /// <Salary>48.62</Salary>
        /// <TeacherFname>Jessica</TeacherFname>
        /// <TeacherId>5</TeacherId>
        /// <TeacherLname>Morris</TeacherLname>
        /// 
        /// </example>


        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //create a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //While Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            //Returns that particular teacher info
            return NewTeacher;
        }


        /// <summary>
        /// Finds the courses taught by a teacher using teacher ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>The courses taught by that particular teacher</returns>
        /// 
        /// <example>
        /// 
        /// GET api/CourseData/ListCoursesByTeacherId/2 ->
        /// 
        /// <CourseViewModel>
        /// <ClassCode>http5102</ClassCode>
        /// <ClassId>2</ClassId>
        /// <ClassName>Project Management</ClassName>
        /// </CourseViewModel>
        ///
        /// <CourseViewModel>
        /// <ClassCode>http5201</ClassCode>
        /// <ClassId>6</ClassId>
        /// <ClassName>Security & Quality Assurance</ClassName>
        /// </CourseViewModel>
        /// 
        /// </example>


        [HttpGet]
        [Route("api/CourseData/ListCoursesByTeacherId/{id}")]
        public IEnumerable<CourseViewModel> GetCoursesByTeacherId(int id)
        {
            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "SELECT * FROM classes WHERE teacherid = " + id;

            // Gather the result set of the query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of courses
            List<CourseViewModel> courses = new List<CourseViewModel> { };

            // While loop through each row in the result set
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                
                CourseViewModel NewCourses = new CourseViewModel();
                NewCourses.ClassId = ClassId;
                NewCourses.ClassCode = ClassCode;
                NewCourses.ClassName = ClassName;

                // Add the course to the list
                courses.Add(NewCourses);
            }

            // Close the connection
            Conn.Close();

            // Return the list of courses
            return courses;
        }


        /// <summary>
        /// Returns a list of Courses in the system
        /// </summary>
        /// <returns>
        /// A list of Courses including (classids, classcodes, classnames)
        /// </returns>
        /// 
        /// <example>
        /// 
        /// GET api/CourseData/ListCourses -> 
        /// 
        ///<CourseViewModel>
        ///<ClassCode>http5101</ClassCode>
        ///<ClassId>1</ClassId>
        ///<ClassName>Web Application Development</ClassName>
        ///</CourseViewModel>
        ///
        ///<CourseViewModel>
        ///<ClassCode>http5102</ClassCode>
        ///<ClassId>2</ClassId>
        ///<ClassName>Project Management</ClassName>
        ///</CourseViewModel>
        ///<CourseViewModel>
        /// ....
        /// 
        /// </example>


        [HttpGet]
        [Route("api/CourseData/ListCourses")]
        public IEnumerable<CourseViewModel> ListCourses()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM classes";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<CourseViewModel> Courses = new List<CourseViewModel>();

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                CourseViewModel newCourse = new CourseViewModel
                {
                    ClassId = ClassId,
                    ClassCode = ClassCode,
                    ClassName = ClassName
                };

                Courses.Add(newCourse);
            }

            Conn.Close();

            return Courses;
        }


        /// <summary>
        /// Finds course info in the system given an ID
        /// </summary>
        /// <param name="id">The course primary key</param>
        /// <returns>The information of that particular course</returns>
        /// 
        /// <example>
        /// 
        /// GET api/CourseData/ListCourses/5 ->
        /// 
        ///<ClassCode>http5105</ClassCode>
        ///<ClassId>5</ClassId>
        ///<ClassName>Database Development</ClassName>
        /// 
        /// </example>


        [HttpGet]
        [Route("api/CourseData/ListCourses/{id}")]
        public CourseViewModel FindCourse(int id)
        {
            CourseViewModel NewCourse = new CourseViewModel();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //create a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from classes where classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //While Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                NewCourse.ClassId = ClassId;
                NewCourse.ClassCode = ClassCode;
                NewCourse.ClassName = ClassName;
            }

            //Returns that particular teacher info
            return NewCourse;
        }


    }
}
