using FluentAssertions;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;
using MCB.Core.Infra.CrossCutting.DateTime;
using System;
using Xunit;

namespace MCB.Core.Domain.Entities.Tests.SpecificationsTests
{
    public class DomainEntitySpecificationsBaseTest
    {
        [Fact]
        public void DomainEntitySpecifications_IdShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var id = Guid.NewGuid();

            // Act
            var result = domainEntitySpecifications.IdShouldRequired(id);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecifications_IdShouldRequired_Fail()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var id = Guid.Empty;

            // Act
            var result = domainEntitySpecifications.IdShouldRequired(id);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecifications_TenantIdShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var teiantId = Guid.NewGuid();

            // Act
            var result = domainEntitySpecifications.TenantIdShouldRequired(teiantId);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecifications_TenantIdShouldRequired_Fail()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var tenantId = Guid.Empty;

            // Act
            var result = domainEntitySpecifications.TenantIdShouldRequired(tenantId);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_CreatedAtShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var createdAt = dateTimeProvider.GetDate();
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.CreatedAtShouldRequired(createdAt);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_CreatedAtShouldRequired_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var createdAt = DateTimeOffset.MinValue;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.CreatedAtShouldRequired(createdAt);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_CreatedAtShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var createdAt1 = dateTimeProvider.GetDate();
            var createdAt2 = dateTimeProvider.GetDate().AddDays(-1);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.CreatedAtShouldValid(createdAt1);
            var result2 = domainEntitySpecifications.CreatedAtShouldValid(createdAt2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_CreatedAtShouldValid_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var createdAt1 = DateTimeOffset.MinValue;
            var createdAt2 = dateTimeProvider.GetDate().AddDays(1);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.CreatedAtShouldValid(createdAt1);
            var result2 = domainEntitySpecifications.CreatedAtShouldValid(createdAt2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_CreatedByShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var createdBy = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.CreatedByShouldRequired(createdBy);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_CreatedByShouldRequired_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var createdBy = default(string);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.CreatedByShouldRequired(createdBy);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_CreatedByShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var createdBy = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.CreatedByShouldValid(createdBy);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_CreatedByShouldValid_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var createdBy1 = default(string);
            var createdBy2 = new string('a', 251);

            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.CreatedByShouldValid(createdBy1);
            var result2 = domainEntitySpecifications.CreatedByShouldValid(createdBy2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastUpdatedAtShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var lastUpdatedAt = dateTimeProvider.GetDate();
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastUpdatedAtShouldRequired(lastUpdatedAt);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastUpdatedAtShouldRequired_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var lastUpdatedAt1 = default(DateTimeOffset?);
            var lastUpdatedAt2 = DateTimeOffset.MinValue;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.LastUpdatedAtShouldRequired(lastUpdatedAt1);
            var result2 = domainEntitySpecifications.LastUpdatedAtShouldRequired(lastUpdatedAt2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastUpdatedAtShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var createdAt = dateTimeProvider.GetDate().AddDays(-1);
            var lastUpdatedAt = dateTimeProvider.GetDate();
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt, createdAt);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastUpdatedAtShouldValid_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var createdAt = dateTimeProvider.GetDate().AddDays(-1);
            var lastUpdatedAt1 = default(DateTimeOffset?);
            var lastUpdatedAt2 = DateTimeOffset.MinValue;
            var lastUpdatedAt3 = createdAt.AddDays(-1);
            var lastUpdatedAt4 = createdAt;
            var lastUpdatedAt5 = dateTimeProvider.GetDate().AddDays(1);

            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt1, createdAt);
            var result2 = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt2, createdAt);
            var result3 = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt3, createdAt);
            var result4 = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt4, createdAt);
            var result5 = domainEntitySpecifications.LastUpdatedAtShouldValid(lastUpdatedAt5, createdAt);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
            result3.Should().Be(expectedResult);
            result4.Should().Be(expectedResult);
            result5.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastUpdatedByShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var lastUpdatedBy = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastUpdatedByShouldRequired(lastUpdatedBy);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastUpdatedByShouldRequired_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var lastUpdatedBy = default(string);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastUpdatedByShouldRequired(lastUpdatedBy);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastUpdatedByShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var lastUpdatedBy = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastUpdatedByShouldValid(lastUpdatedBy);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastUpdatedByShouldValid_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var lastUpdatedBy1 = default(string);
            var lastUpdatedBy2 = new string('a', 251);

            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.LastUpdatedByShouldValid(lastUpdatedBy1);
            var result2 = domainEntitySpecifications.LastUpdatedByShouldValid(lastUpdatedBy2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastSourcePlatformShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var lastSourcePlatform = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastSourcePlatformShouldRequired(lastSourcePlatform);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastSourcePlatformShouldRequired_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var lastSourcePlatform = default(string);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastSourcePlatformShouldRequired(lastSourcePlatform);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecification_LastSourcePlatformShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var lastSourcePlatform = new string('a', 250);
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result = domainEntitySpecifications.LastSourcePlatformShouldValid(lastSourcePlatform);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecification_LastSourcePlatformShouldValid_Error()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var lastSourcePlatform1 = default(string);
            var lastSourcePlatform2 = new string('a', 251);

            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            // Act
            var result1 = domainEntitySpecifications.LastSourcePlatformShouldValid(lastSourcePlatform1);
            var result2 = domainEntitySpecifications.LastSourcePlatformShouldValid(lastSourcePlatform2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecifications_RegistryVersionShouldRequired_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var registryVersion = dateTimeProvider.GetDate();

            // Act
            var result = domainEntitySpecifications.RegistryVersionShouldRequired(registryVersion);

            // Assert
            result.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecifications_RegistryVersionShouldRequired_Fail()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var registryVersion = DateTimeOffset.MinValue;

            // Act
            var result = domainEntitySpecifications.RegistryVersionShouldRequired(registryVersion);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DomainEntitySpecifications_RegistryVersionShouldValid_Success()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = true;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);

            var registryVersion1 = dateTimeProvider.GetDate();
            var registryVersion2 = dateTimeProvider.GetDate().AddDays(-1);

            // Act
            var result1 = domainEntitySpecifications.RegistryVersionShouldValid(registryVersion1);
            var result2 = domainEntitySpecifications.RegistryVersionShouldValid(registryVersion2);

            // Assert
            result1.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);
        }
        [Fact]
        public void DomainEntitySpecifications_RegistryVersionShouldValid_Fail()
        {
            // Arrange
            var dateTimeProvider = new DateTimeProvider();
            var expectedResult = false;
            var domainEntitySpecifications = new DummyDomainEntitySpecifications(dateTimeProvider);
            var registryVersion = dateTimeProvider.GetDate().AddDays(1);

            // Act
            var result = domainEntitySpecifications.RegistryVersionShouldValid(registryVersion);

            // Assert
            result.Should().Be(expectedResult);
        }

        public class DummyDomainEntitySpecifications
            : DomainEntitySpecifications
        {
            public DummyDomainEntitySpecifications(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider)
            {
            }
        }
    }
}
