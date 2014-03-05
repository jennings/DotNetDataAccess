using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.EFContext.Data;

namespace DataAccess.EFContext.App
{
    public class Thingy
    {
        private readonly StudentContext context;
        public bool WaitWhenFinished { get; set; }

        public Thingy(StudentContext context)
        {
            this.context = context;
        }

        public void DoWork()
        {
            Console.WriteLine("DoWork started.");

            var allStudents = this.context.Students;
            Console.WriteLine("Number of students: " + allStudents.Count().ToString());

            Console.WriteLine("Adding a student");
            var newStudent = new Student { Name = "William Adama" };
            this.context.Students.Add(newStudent);
            this.context.SaveChanges();

            var allStudentsAgain = this.context.Students;
            Console.WriteLine("Number of students: " + allStudentsAgain.Count().ToString());

            if (WaitWhenFinished)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
