﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DataAccess.ServiceRepository.App
{
    static class Windsor
    {
        public static IWindsorContainer Container { get { return lazyContainer.Value; } }

        private static Lazy<IWindsorContainer> lazyContainer = new Lazy<IWindsorContainer>(() =>
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<StudentService>()
                    .ImplementedBy<StudentService>()
                );
            DataAccess.ServiceRepository.Data.Windsor.Configure(container);
            return container;
        });
    }
}
