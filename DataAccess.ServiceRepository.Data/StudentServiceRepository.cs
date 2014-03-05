using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceRepository.Data
{
    internal class StudentServiceRepository : IStudentServiceRepository
    {
        private readonly StudentContext context;

        public StudentServiceRepository(StudentContext context)
        {
            this.context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return this.context.Students;
        }

        public Student GetStudentById(int id)
        {
            return this.context.Students.Find(id);
        }

        public void Add(Student student)
        {
            this.context.Students.Add(student);
            this.context.SaveChanges();
        }

        public void Update(Student student)
        {
            var existingStudent = this.context.Students.Find(student.Id);
            if (existingStudent == null)
            {
                Add(student);
            }
            else
            {
                existingStudent.Name = student.Name;
                // and more...
                this.context.SaveChanges();
            }
        }

        public void Delete(int studentId)
        {
            var existingStudent = this.context.Students.Find(studentId);
            if (existingStudent != null)
            {
                this.context.Students.Remove(existingStudent);
                this.context.SaveChanges();
            }
        }
    }
}
