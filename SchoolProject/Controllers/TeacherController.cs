using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;


namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Returns the list of teachers using server rendering
        /// </summary>
        /// <returns>Out the list of teachers with link</returns>
        /// <example>
        /// 
        /// GET Teacher/List -> 
        /// 
        /// Alexander Bennett
        /// Caitlin Cummings
        /// ....
        /// </example>
       

        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }


        /// <summary>
        /// Returns info about a particular teacher using srever rendering
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Output the infoof teacher(firstname, lastname, emplyeeNumber, HireDAte and salary) and courses taught by that particular teacher</returns>
        /// 
        /// <example>
        /// 
        /// GET  Teacher/Show/3
        /// 
        /// Teacher Name : Linda Chan
        /// Employee Number : T382
        /// Hire Date : 8/22/2015 12:00:00 AM
        /// Salary : 60.22
        ///
        /// Courses Taught :
        /// Course ID: 7
        /// Course Code: http5202
        /// Course Name: Web Application Development 2
        /// 
        /// </example>
        

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            IEnumerable<CourseViewModel> courses = controller.GetCoursesByTeacherId(id);

            TeacherViewModel viewModel = new TeacherViewModel
            {
                Teacher = SelectedTeacher,
            //Teacher1 = SelectedTeacher,
            Courses = courses.ToList()
            };
            return View(viewModel);
        }

        /// <summary>
        /// Returns the list of courses using server rendering
        /// </summary>
        /// <returns>Out the list of courses with link</returns>
        /// <example>
        /// 
        /// GET Teacher/ListCourses -> 
        /// 
        /// </example>
        /// 
        /// List of Courses
        /// http5101
        /// http5102
        /// ...
        ///
        /// </example>


        public ActionResult ListCourses()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<CourseViewModel> Courses = controller.ListCourses();
            return View(Courses);
        }


        /// <summary>
        /// Returns info about a particular course using srever rendering
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Output the info(classid, classcode, classname) of that particular course</returns>
        /// 
        /// <example>
        /// 
        /// GET  Teacher/ShowCourses/7
        /// 
        /// Course ID : 7
        /// Course Code : http5202
        /// Course Name : Web Application Development 2
        ///
        /// </example>


        public ActionResult ShowCourses(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            CourseViewModel SelectedCourse = controller.FindCourse(id);
            return View(SelectedCourse);
        }


        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }


        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }


        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(int TeacherId, string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary)
        {


            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherId = TeacherId;
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

    }
}