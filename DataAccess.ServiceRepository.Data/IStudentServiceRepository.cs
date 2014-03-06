using System;
using System.Collections.Generic;

namespace DataAccess.ServiceRepository.Data
{
    public interface IStudentServiceRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        void PromoteAllClassesAtomic();

        void Add(Student student);
        void Update(Student student);
        void Delete(int studentId);
    }
}
