using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork.Data
{
    internal class StudentContext : DbContext, IStudentUnitOfWork
    {
        public IRepository<Student> Students { get; private set; }
        public IRepository<Course> Courses { get; private set; }
        public IRepository<Enrollment> Enrollments { get; private set; }

        public StudentContext()
        {
            this.Students = new Repository<Student>(this);
            this.Courses = new Repository<Course>(this);
            this.Enrollments = new Repository<Enrollment>(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        }
    }
}
