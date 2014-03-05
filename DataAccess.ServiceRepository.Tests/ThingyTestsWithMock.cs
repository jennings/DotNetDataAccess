using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.ServiceRepository.App;
using DataAccess.ServiceRepository.Data;
using Moq;
using NUnit.Framework;

namespace DataAccess.ServiceRepository.Tests
{
    [TestFixture]
    public class ThingyTestsWithMock
    {
        Mock<IStudentServiceRepository> repositoryWithMock;
        Thingy thingyWithMock;

        [SetUp]
        public void SetUp()
        {
            repositoryWithMock = new Mock<IStudentServiceRepository>();
            thingyWithMock = new Thingy(repositoryWithMock.Object);
        }

        [Test]
        public void ServiceRepository_DoWork_AddsAStudent_WithMockDbSet()
        {
            thingyWithMock.DoWork();
            repositoryWithMock.Verify(s => s.Add(It.IsAny<Student>()));
        }
    }
}
