using System;
using Core.Shared.DTO;
using Core.Infrastructure.Interfaces.Crm;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Scheduler;
using Core.Service.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core.Infrastructure.Interfaces.DAL;

namespace $safeprojectname$.Domain
{
    [TestClass]
    public class SiteServiceTests
    {
        private Mock<ILoggingProvider> _logger;
        private Mock<ICrmConsumerProvider> _crmProvider;
        private Mock<ISchedulerProvider> _schedulerRepository;
        private Mock<ISiteRepository> _siteRepository;
        private SiteService _sut;
        private SiteDto _siteDto;

        [TestInitialize]
        public void Setup()
        {
            _logger = new Mock<ILoggingProvider>();
            _crmProvider = new Mock<ICrmConsumerProvider>();
            _schedulerRepository = new Mock<ISchedulerProvider>();
            _siteRepository = new Mock<ISiteRepository>();
            _sut = new SiteService(
                    _siteRepository.Object
                    );
            _siteDto = new SiteDto
            {
                Id = Guid.NewGuid(),
                Name = "UK",
                Culture = "en-GB",
                Domain = "com",
                CreatedDate = DateTimeOffset.UtcNow,
                ModifiedDate = null
            };
        }

        [TestCleanup]
        public void Clean()
        {
            _siteRepository = null;
            _logger = null;
            _crmProvider = null;
            _schedulerRepository = null;
            _sut = null;
        }

        [TestMethod]
        public void CreateSiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteSiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetSiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetSiteByDomainTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetSitesPagedTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateSiteTest()
        {
            Assert.Fail();
        }
    }
}