using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EFContext.Data
{
    public class Enrollment
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
