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
        Thingy thingyWithMock;

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
            thingyWithMock = new Thingy(contextWithMock.Object);
        }

        [Test]
        public void EFContext_DoWork_AddsAStudent_WithMockDbSet()
        {
            thingyWithMock.DoWork();
            mockDbSet.Verify(s => s.Add(It.IsAny<Student>()));
        }
    }
}
