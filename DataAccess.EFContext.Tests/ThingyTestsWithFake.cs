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
    public class ThingyTestsWithFake
    {
        ICollection<Student> fakeBackingCollection;
        Mock<StudentContext> contextWithFake;
        StudentService serviceWithFake;

        [SetUp]
        public void SetUp()
        {
            var fakeDbSet = new FakeDbSet<Student>();
            fakeBackingCollection = fakeDbSet.BackingCollection;
            contextWithFake = new Mock<StudentContext>();
            contextWithFake.SetupGet(c => c.Students).Returns(fakeDbSet);
            serviceWithFake = new StudentService(contextWithFake.Object);
        }

        [Test]
        public void EFContext_WithFake_CreateStudents_CreatesStudents()
        {
            var startingCount = fakeBackingCollection.Count;
            serviceWithFake.CreateStudents();
            var endingCount = fakeBackingCollection.Count;
            Assert.Greater(endingCount, startingCount);
        }

        [Test]
        public void EFContext_WithFake_PromoteAllClasses_IncrementsYears()
        {
            var student = new Student { YearsCompleted = 2, IsGraduated = false };
            fakeBackingCollection.Add(student);

            serviceWithFake.PromoteAllClasses();

            Assert.AreEqual(3, student.YearsCompleted);
        }

        [Test]
        public void EFContext_WithFake_PromoteAllClasses_GraduatesStudents()
        {
            var eligibleStudent = new Student { YearsCompleted = 3, IsGraduated = false };
            var ineligibleStudent = new Student { YearsCompleted = 2, IsGraduated = false };
            fakeBackingCollection.Add(eligibleStudent);
            fakeBackingCollection.Add(ineligibleStudent);

            serviceWithFake.PromoteAllClasses();

            Assert.True(eligibleStudent.IsGraduated);
            Assert.False(ineligibleStudent.IsGraduated);
        }
    }
}
