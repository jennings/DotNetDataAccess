using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.ServiceRepository.Data;

namespace DataAccess.ServiceRepository.App
{
    public class Thingy
    {
        private readonly IStudentServiceRepository repository;
        public bool WaitWhenFinished { get; set; }

        public Thingy(IStudentServiceRepository repository)
        {
            this.repository = repository;
        }

        public void DoWork()
        {
            Console.WriteLine("ServiceRepository App started.");

            var allStudents = this.repository.GetAllStudents();
            Console.WriteLine("Number of students: " + allStudents.Count().ToString());

            Console.WriteLine("Adding a student");
            var newStudent = new Student { Name = "William Adama" };
            this.repository.Add(newStudent);

            var allStudentsAgain = this.repository.GetAllStudents();
            Console.WriteLine("Number of students: " + allStudentsAgain.Count().ToString());

            if (WaitWhenFinished)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
