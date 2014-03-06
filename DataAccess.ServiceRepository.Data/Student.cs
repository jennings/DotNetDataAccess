using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceRepository.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearsCompleted { get; set; }
        public bool IsGraduated { get; set; }
    }
}
