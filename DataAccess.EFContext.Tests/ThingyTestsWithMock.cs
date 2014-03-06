using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EFContext.App;
using DataAccess.EFContext.Data;
using DataAccess.EFContext.Tests.Support;
using Moq;
using NUnit.Framework;

namespace DataAccess.EFContext.Tests
{
    [TestFixture]
    public class ThingyTestsWithMock
    {
        ICollection<Student> mockBackingCollection;
        Mock<IDbSet<Student>> mockDbSet;
        Mock<StudentContext> contextWithMock;
        StudentService serviceWithMock;

        [SetUp]
        public void SetUp()
        {
            // Setting up the test using a mock
            mockBackingCollection = new List<Student>();
            mockDbSet = new Mock<IDbSet<Student>>();
            mockDbSet.Setup(s => s.ElementType).Returns(mockBackingCollection.AsQueryable().ElementType);
            mockDbSet.Setup(s => s.Expression).Returns(mockBackingCollection.AsQueryable().Expression);
            mockDbSet.Setup(s => s.Provider).Returns(mockBackingCollection.AsQueryable().Provider);
            contextWithMock = new Mock<StudentContext>();
            contextWithMock.SetupGet(s => s.Students).Returns(mockDbSet.Object);
            serviceWithMock = new StudentService(contextWithMock.Object);
        }

        [Test]
        public void EFContext_WithMock_CreateStudents_CreatesStudents()
        {
            serviceWithMock.CreateStudents();
            mockDbSet.Verify(r => r.Add(It.IsAny<Student>()), Times.AtLeastOnce);
        }

        [Test]
        public void EFContext_WithMock_PromoteAllClasses_IncrementsYears()
        {
            var student = new Student { YearsCompleted = 2, IsGraduated = false };
            mockBackingCollection.Add(student);

            serviceWithMock.PromoteAllClasses();

            Assert.AreEqual(3, student.YearsCompleted);
        }

        [Test]
        public void EFContext_WithMock_PromoteAllClasses_GraduatesStudents()
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
