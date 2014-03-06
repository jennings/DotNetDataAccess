using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.UnitOfWork.Data;

namespace DataAccess.UnitOfWork.App
{
    public class StudentService
    {
        private readonly IStudentUnitOfWork context;

        public StudentService(IStudentUnitOfWork context)
        {
            this.context = context;
        }

        public int CreateStudents()
        {
            this.context.Students.Add(new Student { Name = "Husker", YearsCompleted = 4, IsGraduated = true });
            this.context.Students.Add(new Student { Name = "Apollo", YearsCompleted = 3, IsGraduated = false });
            this.context.Students.Add(new Student { Name = "Athena", YearsCompleted = 2, IsGraduated = false });
            this.context.Students.Add(new Student { Name = "Hotdog", YearsCompleted = 1, IsGraduated = false });
            return this.context.SaveChanges();
        }

        public IEnumerable<Student> GetCurrentStudents()
        {
            return this.context.Students.Where(s => !s.IsGraduated);
        }

        public int PromoteAllClasses()
        {
            foreach (var student in this.context.Students.Where(s => !s.IsGraduated))
            {
                student.YearsCompleted += 1;
                if (student.YearsCompleted >= 4)
                    student.IsGraduated = true;
            }

            return this.context.SaveChanges();
        }
    }
}
