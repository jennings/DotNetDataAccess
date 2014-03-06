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
    public class StudentServiceTestsWithMock
    {
        Mock<IStudentServiceRepository> repositoryWithMock;
        StudentService serviceWithMock;

        [SetUp]
        public void SetUp()
        {
            repositoryWithMock = new Mock<IStudentServiceRepository>();
            serviceWithMock = new StudentService(repositoryWithMock.Object);
        }

        [Test]
        public void ServiceRepository_WithMock_CreateStudents_CreatesStudents()
        {
            serviceWithMock.CreateStudents();
            repositoryWithMock.Verify(s => s.Add(It.IsAny<Student>()), Times.AtLeastOnce);
        }

        [Test]
        public void ServiceRepository_WithMock_PromoteAllClasses_IncrementsYears()
        {
            var student = new Student { YearsCompleted = 2, IsGraduated = false };
            repositoryWithMock.Setup(r => r.GetAllStudents()).Returns(Enumerable.Repeat(student, 1));

            serviceWithMock.PromoteAllClasses();

            Assert.AreEqual(3, student.YearsCompleted);
        }

        [Test]
        public void ServiceRepository_WithMock_PromoteAllClasses_GraduatesStudents()
        {
            var eligibleStudent = new Student { YearsCompleted = 3, IsGraduated = false };
            var ineligibleStudent = new Student { YearsCompleted = 2, IsGraduated = false };
            repositoryWithMock.Setup(r => r.GetAllStudents()).Returns(new List<Student>() { eligibleStudent, ineligibleStudent });

            serviceWithMock.PromoteAllClasses();

            Assert.True(eligibleStudent.IsGraduated);
            Assert.False(ineligibleStudent.IsGraduated);
        }
    }
}
