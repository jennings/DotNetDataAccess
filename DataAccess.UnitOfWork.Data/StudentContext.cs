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

        public StudentContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StudentContext>());
            this.Students = new Repository<Student>(this);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
        }
    }
}
