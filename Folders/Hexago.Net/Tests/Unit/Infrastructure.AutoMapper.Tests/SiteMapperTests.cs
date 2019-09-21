using AutoMapper;
using Core.Model;
using Core.Shared.DTO;
using Infrastructure.AutoMapper.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Shared.Tests.Mapping.Helper
{
    [TestClass]
    public class SiteMapperTests
    {
        private Mock<IMapper> _imapper;
        private MappingProvider _mapper;

        [TestInitialize]
        public void Setup()
        {
            _imapper = new Mock<IMapper>();
            _mapper = new MappingProvider(_imapper.Object);
        }

        [TestCleanup]
        public void Clean()
        {
            _mapper = null;
        }

        [TestMethod]
        public void Convert_site_entity_to_dto_should_work()
        {
            // Arrange
            var entity = new Site
            {
                Culture = "en-GB",
                Name = "uk",
                Domain = "cadbury.co.uk",
                CreatedDate = DateTimeOffset.UtcNow,
                ModifiedDate = DateTimeOffset.UtcNow
            };

            // Act
            var dto = _mapper.toDto<SiteDto>(entity);

            //Assert
            Assert.AreEqual<string>(dto.Culture, entity.Culture);
            Assert.AreEqual<string>(dto.Name, entity.Name);
            Assert.AreEqual<string>(dto.Domain, entity.Domain);
        }

        [TestMethod]
        public void Convert_site_dto_to_entity_should_work()
        {
            // Arrange
            var dto = new SiteDto
            {
                Culture = "en-GB",
                Name = "uk",
                Domain = "cadbury.co.uk"
            };

            // Act
            var entity = _mapper.toEntity<Site>(dto);

            //Assert
            Assert.AreEqual<string>(dto.Culture, entity.Culture);
            Assert.AreEqual<string>(dto.Name, entity.Name);
            Assert.AreEqual<string>(dto.Domain, entity.Domain);
        }

        [TestMethod]
        public void Convert_colletion_site_dto_to_entity_should_work()
        {
            // Arrange
            List<SiteDto> dtos = new List<SiteDto>();
            for (int i = 0; i < 5; i++)
            {
                var dto = new SiteDto
                {
                    Culture = "en-GB",
                    Name = "uk",
                    Domain = "cadbury.co.uk"
                };
                dtos.Add(dto);
            }


            // Act
            var entities = _mapper.toEntities<Site>(dtos).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<string>(dtos[i].Culture, entities[i].Culture);
                Assert.AreEqual<string>(dtos[i].Name, entities[i].Name);
                Assert.AreEqual<string>(dtos[i].Domain, entities[i].Domain);
            }
        }

        [TestMethod]
        public void Convert_collection_site_entity_to_dto_should_work()
        {
            // Arrange
            List<Site> entities = new List<Site>();
            for (int i = 0; i < 5; i++)
            {
                var entity = new Site
                {
                    Culture = "en-GB",
                    Name = "uk",
                    Domain = "cadbury.co.uk",
                    CreatedDate = DateTimeOffset.UtcNow,
                    ModifiedDate = DateTimeOffset.UtcNow
                };
                entities.Add(entity);
            }


            // Act
            var dtos = _mapper.toDtos<SiteDto>(entities).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<string>(dtos[i].Culture, entities[i].Culture);
                Assert.AreEqual<string>(dtos[i].Name, entities[i].Name);
                Assert.AreEqual<string>(dtos[i].Domain, entities[i].Domain);
            }
        }
    }
}