using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFContext.Data
{
    public class StudentContext : DbContext
    {
        public virtual IDbSet<Student> Students { get; set; }

        public StudentContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StudentContext>());
        }
    }
}
