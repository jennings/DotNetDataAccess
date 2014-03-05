using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.UnitOfWork.App;
using DataAccess.UnitOfWork.Data;
using DataAccess.UnitOfWork.Tests.Support;
using Moq;
using NUnit.Framework;

namespace DataAccess.UnitOfWork.Tests
{
    [TestFixture]
    public class ThingyTestsWithMock
    {
        ICollection<Student> mockBackingCollection;
        Mock<IRepository<Student>> mockRepository;
        Mock<IStudentUnitOfWork> unitOfWorkWithMock;
        Thingy thingyWithMock;

        [SetUp]
        public void SetUp()
        {
            mockBackingCollection = new List<Student>();
            mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(r => r.ElementType).Returns(() => mockBackingCollection.AsQueryable().ElementType);
            mockRepository.Setup(r => r.Expression).Returns(() => mockBackingCollection.AsQueryable().Expression);
            mockRepository.Setup(r => r.Provider).Returns(() => mockBackingCollection.AsQueryable().Provider);
            unitOfWorkWithMock = new Mock<IStudentUnitOfWork>();
            unitOfWorkWithMock.SetupGet(u => u.Students).Returns(mockRepository.Object);
            thingyWithMock = new Thingy(unitOfWorkWithMock.Object);
        }

        [Test]
        public void UnitOfWork_DoWork_AddsAStudent_WithMockRepository()
        {
            thingyWithMock.DoWork();
            mockRepository.Verify(r => r.Add(It.IsAny<Student>()));
        }
    }
}
