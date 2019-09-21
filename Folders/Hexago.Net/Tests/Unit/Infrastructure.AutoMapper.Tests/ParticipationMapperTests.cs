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
    public class ParticipationMapperTests
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
        public void Convert_vote_entity_to_dto_should_work()
        {
            // Arrange
            var entity = new Participation
            {
                Status = "Tested",
                ApiStatus = "Success",
                ApiMessage = "Transaction success",
                CreatedDate = DateTimeOffset.UtcNow,
                ModifiedDate = DateTimeOffset.UtcNow
            };

            // Act
            var dto = _mapper.toDto<ParticipationDto>(entity);

            //Assert
            Assert.AreEqual<string>(dto.Status, entity.Status);
            Assert.AreEqual<string>(dto.ApiStatus, entity.ApiStatus);
            Assert.AreEqual<string>(dto.ApiMessage, entity.ApiMessage);
        }

        [TestMethod]
        public void Convert_vote_dto_to_entity_should_work()
        {
            // Arrange
            var dto = new ParticipationDto
            {
                Status = "Tested",
                ApiStatus = "Success",
                ApiMessage = "Transaction success",
            };

            // Act
            var entity = _mapper.toEntity<Participation>(dto);

            //Assert
            Assert.AreEqual<string>(dto.Status, entity.Status);
            Assert.AreEqual<string>(dto.ApiStatus, entity.ApiStatus);
            Assert.AreEqual<string>(dto.ApiMessage, entity.ApiMessage);
        }

        [TestMethod]
        public void Convert_colletion_vote_dto_to_entity_should_work()
        {
            // Arrange
            List<ParticipationDto> dtos = new List<ParticipationDto>();
            for (int i = 0; i < 5; i++)
            {
                var dto = new ParticipationDto
                {
                    Status = "Tested" + i,
                    ApiStatus = "Success" + i,
                    ApiMessage = "Transaction success" + i,
                };
                dtos.Add(dto);
            }


            // Act
            var entities = _mapper.toEntities<Participation>(dtos).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<string>(dtos[i].Status, entities[i].Status);
                Assert.AreEqual<string>(dtos[i].ApiStatus, entities[i].ApiStatus);
                Assert.AreEqual<string>(dtos[i].ApiMessage, entities[i].ApiMessage);
            }
        }

        [TestMethod]
        public void Convert_collection_vote_entity_to_dto_should_work()
        {
            // Arrange
            List<Participation> entities = new List<Participation>();
            for (int i = 0; i < 5; i++)
            {
                var entity = new Participation
                {
                    Status = "Tested" + i,
                    ApiStatus = "Success" + i,
                    ApiMessage = "Transaction success" + i,
                    CreatedDate = DateTimeOffset.UtcNow,
                    ModifiedDate = DateTimeOffset.UtcNow
                };
                entities.Add(entity);
            }

            // Act
            var dtos = _mapper.toDtos<ParticipationDto>(entities).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<string>(dtos[i].Status, entities[i].Status);
                Assert.AreEqual<string>(dtos[i].ApiStatus, entities[i].ApiStatus);
                Assert.AreEqual<string>(dtos[i].ApiMessage, entities[i].ApiMessage);
            }
        }
    }
}