using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DataAccess.ServiceRepository.Data
{
    public static class Windsor
    {
        public static void Configure(IWindsorContainer container)
        {
            container.Register(
                Component.For<StudentContext>()
                    .ImplementedBy<StudentContext>(),
                Component.For<IStudentServiceRepository>()
                    .ImplementedBy<StudentServiceRepository>()
                );

            // This ensures that EntityFramework.SqlServer.dll gets
            // copied everywhere this assembly goes.
            // It's a hack. Sorry.
            System.Diagnostics.Trace.WriteIf(false, typeof(System.Data.Entity.SqlServer.SqlProviderServices).Name);
        }
    }
}
