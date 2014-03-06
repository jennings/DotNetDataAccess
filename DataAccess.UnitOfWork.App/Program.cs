using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork.App
{
    class Program
    {
        static StudentService service;

        static void Main(string[] args)
        {
            service = Windsor.Container.Resolve<StudentService>();

            Console.WriteLine("IUNITOFWORK");

            Console.WriteLine("Current students:");
            ListCurrentStudents();

            Console.WriteLine("Creating new students");
            service.CreateStudents();

            Console.WriteLine("Current students:");
            ListCurrentStudents();

            Console.WriteLine("Promoting all classes");
            service.PromoteAllClasses();

            Console.WriteLine("Current students:");
            ListCurrentStudents();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void ListCurrentStudents()
        {
            foreach (var student in service.GetCurrentStudents())
                Console.WriteLine("  Name: {0}, Years: {1}, IsGraduated: {2}", student.Name, student.YearsCompleted, student.IsGraduated);
        }
    }
}
