using Moq;
using System;
using Core.Infrastructure.Interfaces.DAL;
using Core.Service.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $safeprojectname$.Domain
{
    [TestClass]
    public class ParticipationServiceTests
    {
        private Mock<IParticipationRepository> _participationRepository;
        private ParticipationService _sut;
        private Guid _siteId;

        [TestInitialize]
        public void Setup()
        {
            _participationRepository = new Mock<IParticipationRepository>();
            _sut = new ParticipationService(_participationRepository.Object);
            _siteId = Guid.NewGuid();
        }

        [TestCleanup]
        public void Clean()
        {
            _participationRepository = null;
            _sut = null;
        }

        [TestMethod]
        public void CreateVoteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteFailedParticipationTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetBetweenTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTotalVoteNumberTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTotalVoteNumberByChocolateBarTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTotalVoteNumberBySiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetVoteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetVotesBySiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetVotesPagedBySiteTest()
        {
            Assert.Fail();
        }
    }
}