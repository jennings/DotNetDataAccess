using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DataAccess.EFContext.Data
{
    public static class Windsor
    {
        public static void Configure(IWindsorContainer container)
        {
            container.Register(
                Component.For<StudentContext>()
                    .ImplementedBy<StudentContext>()
                );
        }
    }
}
