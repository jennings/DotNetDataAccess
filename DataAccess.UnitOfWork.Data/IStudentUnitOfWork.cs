using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.UnitOfWork.Data
{
    public interface IStudentUnitOfWork
    {
        IRepository<Student> Students { get; }

        int SaveChanges();
    }
}
