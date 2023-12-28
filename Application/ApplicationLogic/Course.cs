using EduBase1.Data;
using EduBase1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduBase1.Application.ApplicationLogic
{
    internal class Course
    {
        private HighSchoolSystemContext Context { get; set; }
        public Course()
        {
            Context = new();
        }
        public void CourseSummary() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Översikt: Alla kurser:");
            Console.WriteLine();

            var query = Context.Courses.ToList();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            foreach (var course in query) 
            {
                Console.WriteLine("Kurs-ID: {0}", course.CourseId);
                Console.WriteLine("Namn: {0}", course.Title);
                Console.WriteLine("Startdatum: {0}", course.StartDate);
                Console.WriteLine("Slutdatum: {0}", course.EndDate);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        public void ActiveCourses() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Visar aktiva kurser:");
            Console.WriteLine();

            var query = from course in Context.Courses
                        join schoolClass in Context.SchoolClasses on course.FkclassId equals schoolClass.ClassId
                        join employee in Context.Employees on schoolClass.FkemployeeId equals employee.EmployeeId
                        where course.EndDate == null
                        group new { course, employee } by new { course.Title, employee.FirstName, employee.LastName } into g
                        select new
                        {
                            Kurs = g.Key.Title,
                            Lärare = g.Key.FirstName + " " + g.Key.LastName
                        };

            Console.ForegroundColor = ConsoleColor.DarkGray;

            foreach (var result in query)
            {
                Console.WriteLine("Kurs: {0}", result.Kurs);
                Console.WriteLine("Ansvarig lärare: {0}", result.Lärare);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        public void ShowGrade() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Visar betyg:");
            Console.WriteLine();

            var query = from enrollment in Context.Enrollments
                        join course in Context.Courses on enrollment.FkcourseId equals course.CourseId
                        join schoolClass in Context.SchoolClasses on course.FkclassId equals schoolClass.ClassId
                        join employee in Context.Employees on schoolClass.FkemployeeId equals employee.EmployeeId
                        select new
                        {
                            Kurs = course.Title,
                            Betyg = enrollment.Grade,
                            Lärare = employee.FirstName + " " + employee.LastName,
                            Datum = enrollment.SetDate
                        };

            Console.ForegroundColor = ConsoleColor.DarkGray;

            foreach (var enrollment in query)
            {
                Console.WriteLine("Kurs: {0}", enrollment.Kurs);
                Console.WriteLine("Betyg: {0}", enrollment.Betyg);
                Console.WriteLine("Ansvarig lärare: {0}", enrollment.Lärare);
                Console.WriteLine("Datum: {0}", enrollment.Datum);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        public void SetGrade() { }
    }
}
