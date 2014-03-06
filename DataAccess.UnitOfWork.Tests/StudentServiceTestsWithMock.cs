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
    public class StudentServiceTestsWithMock
    {
        ICollection<Student> mockBackingCollection;
        Mock<IRepository<Student>> mockRepository;
        Mock<IStudentUnitOfWork> unitOfWorkWithMock;
        StudentService serviceWithMock;

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
            serviceWithMock = new StudentService(unitOfWorkWithMock.Object);
        }

        [Test]
        public void UnitOfWork_WithMock_CreateStudents_CreatesStudents()
        {
            serviceWithMock.CreateStudents();
            mockRepository.Verify(r => r.Add(It.IsAny<Student>()), Times.AtLeastOnce);
        }

        [Test]
        public void UnitOfWork_WithMock_PromoteAllClasses_IncrementsYears()
        {
            var student = new Student { YearsCompleted = 2, IsGraduated = false };
            mockBackingCollection.Add(student);

            serviceWithMock.PromoteAllClasses();

            Assert.AreEqual(3, student.YearsCompleted);
        }

        [Test]
        public void UnitOfWork_WithMock_PromoteAllClasses_GraduatesStudents()
        {
            var eligibleStudent = new Student { YearsCompleted = 3, IsGraduated = false };
            var ineligibleStudent = new Student { YearsCompleted = 2, IsGraduated = false };
            mockBackingCollection.Add(eligibleStudent);
            mockBackingCollection.Add(ineligibleStudent);

            serviceWithMock.PromoteAllClasses();

            Assert.True(eligibleStudent.IsGraduated);
            Assert.False(ineligibleStudent.IsGraduated);
        }
    }
}
