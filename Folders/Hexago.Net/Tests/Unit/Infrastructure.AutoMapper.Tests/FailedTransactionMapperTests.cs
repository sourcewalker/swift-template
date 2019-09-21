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
    public class FailedTransactionMapperTests
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
        public void Convert_failed_transaction_entity_to_dto_should_work()
        {
            // Arrange
            var entity = new FailedTransaction
            {
                TermsConsent = true,
                NewsletterOptin = true,
                CreatedDate = DateTimeOffset.UtcNow,
                ModifiedDate = DateTimeOffset.UtcNow
            };

            // Act
            var dto = _mapper.toDto<FailedTransactionDto>(entity);

            //Assert
            Assert.AreEqual<bool>(dto.TermsConsent, entity.TermsConsent);
            Assert.AreEqual<bool>(dto.NewsletterOptin, entity.NewsletterOptin);
        }

        [TestMethod]
        public void Convert_failed_transaction_dto_to_entity_should_work()
        {
            // Arrange
            var dto = new FailedTransactionDto
            {
                TermsConsent = true,
                NewsletterOptin = true
            };

            // Act
            var entity = _mapper.toEntity<FailedTransaction>(dto);

            //Assert
            Assert.AreEqual<bool>(dto.TermsConsent, entity.TermsConsent);
            Assert.AreEqual<bool>(dto.NewsletterOptin, entity.NewsletterOptin);
        }

        [TestMethod]
        public void Convert_colletion_failed_transaction_dto_to_entity_should_work()
        {
            // Arrange
            List<FailedTransactionDto> dtos = new List<FailedTransactionDto>();
            for (int i = 0; i < 5; i++)
            {
                var dto = new FailedTransactionDto
                {
                    TermsConsent = true,
                    NewsletterOptin = true
                };
                dtos.Add(dto);
            }


            // Act
            var entities = _mapper.toEntities<FailedTransaction>(dtos).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<bool>(dtos[i].TermsConsent, entities[i].TermsConsent);
                Assert.AreEqual<bool>(dtos[i].NewsletterOptin, entities[i].NewsletterOptin);
            }
        }

        [TestMethod]
        public void Convert_collection_failed_transaction_entity_to_dto_should_work()
        {
            // Arrange
            List<FailedTransaction> entities = new List<FailedTransaction>();
            for (int i = 0; i < 5; i++)
            {
                var entity = new FailedTransaction
                {
                    TermsConsent = true,
                    NewsletterOptin = true,
                    CreatedDate = DateTimeOffset.UtcNow,
                    ModifiedDate = DateTimeOffset.UtcNow
                };
                entities.Add(entity);
            }

            // Act
            var dtos = _mapper.toDtos<FailedTransactionDto>(entities).ToList();

            //Assert
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual<bool>(dtos[i].TermsConsent, entities[i].TermsConsent);
                Assert.AreEqual<bool>(dtos[i].NewsletterOptin, entities[i].NewsletterOptin);
            }
        }
    }
}