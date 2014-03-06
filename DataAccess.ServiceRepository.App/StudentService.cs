using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.ServiceRepository.Data;

namespace DataAccess.ServiceRepository.App
{
    public class StudentService
    {
        private readonly IStudentServiceRepository repository;

        public StudentService(IStudentServiceRepository repository)
        {
            this.repository = repository;
        }

        public void CreateStudents()
        {
            this.repository.Add(new Student { Name = "Husker", YearsCompleted = 4, IsGraduated = true });
            this.repository.Add(new Student { Name = "Apollo", YearsCompleted = 3, IsGraduated = false });
            this.repository.Add(new Student { Name = "Athena", YearsCompleted = 2, IsGraduated = false });
            this.repository.Add(new Student { Name = "Hotdog", YearsCompleted = 1, IsGraduated = false });
        }

        public IEnumerable<Student> GetCurrentStudents()
        {
            return this.repository.GetAllStudents().Where(s => !s.IsGraduated);
        }

        public void PromoteAllClasses()
        {
            var eligibleStudents =
                this.repository.GetAllStudents()
                .Where(s => !s.IsGraduated)
                .ToList(); // We must .ToList() it so we can call Update while iterating

            foreach (var student in eligibleStudents)
            {
                student.YearsCompleted += 1;
                if (student.YearsCompleted >= 4)
                    student.IsGraduated = true;

                this.repository.Update(student);
            }
        }
    }
}
