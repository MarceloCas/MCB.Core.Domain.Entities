using FluentAssertions;
using MCB.Core.Domain.Entities.Abstractions.ValueObjects;
using MCB.Core.Domain.Entities.DomainEntitiesBase;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;
using MCB.Core.Infra.CrossCutting.DateTime;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator.Abstractions.Enums;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MCB.Core.Domain.Entities.Tests;

public class DomainEntityBaseTest
{
    [Fact]
    public void DomainEntityBase_Should_RegisterNew()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var tenantId = Guid.NewGuid();
        var executionUser = "marcelo.castelo@outlook.com";
        var lastSourcePlatform = "AppDemo";
        var initialCreatedAt = customer.AuditableInfo.CreatedAt;
        var initialRegistryVersion = customer.RegistryVersion;
        var correlationId = Guid.NewGuid();

        // Act
        customer.RegisterNewExposed(tenantId, executionUser, lastSourcePlatform, correlationId);

        // Assert
        customer.Id.Should().NotBe(default(Guid));
        customer.TenantId.Should().Be(tenantId);
        customer.AuditableInfo.CreatedBy.Should().Be(executionUser);
        customer.AuditableInfo.CreatedAt.Should().BeAfter(initialCreatedAt);
        customer.AuditableInfo.LastUpdatedBy.Should().BeNull();
        customer.AuditableInfo.LastUpdatedAt.Should().BeNull();
        customer.AuditableInfo.LastSourcePlatform.Should().Be(lastSourcePlatform);
        customer.RegistryVersion.Should().BeAfter(initialRegistryVersion);
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeTrue();
        customer.ValidationInfo.HasValidationMessage.Should().BeFalse();
        customer.ValidationInfo.HasErrorMessages.Should().BeFalse();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(0);
    }

    [Fact]
    public void DomainEntityBase_Should_SetExistingInfo()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var createdBy = "marcelo.castelo@outlook.com";
        var createdAt = dateTimeProvider.GetDate().UtcDateTime;
        var lastUpdatedBy = "marcelo.castelo@github.com";
        var lastUpdatedAt = dateTimeProvider.GetDate().UtcDateTime;
        var lastSourcePlatform = "AppDemo";
        var registryVersion = dateTimeProvider.GetDate().UtcDateTime;
        var correlationId = Guid.NewGuid();

        // Act
        customer.SetExistingInfoExposed(id, tenantId, createdBy, createdAt, lastUpdatedBy, lastUpdatedAt, lastSourcePlatform, registryVersion, correlationId);

        // Assert
        customer.Id.Should().Be(id);
        customer.TenantId.Should().Be(tenantId);
        customer.AuditableInfo.CreatedBy.Should().Be(createdBy);
        customer.AuditableInfo.CreatedAt.Should().Be(createdAt);
        customer.AuditableInfo.LastUpdatedBy.Should().Be(lastUpdatedBy);
        customer.AuditableInfo.LastUpdatedAt.Should().Be(lastUpdatedAt);
        customer.AuditableInfo.LastSourcePlatform.Should().Be(lastSourcePlatform);
        customer.AuditableInfo.LastCorrelationId.Should().Be(correlationId);
        customer.RegistryVersion.Should().Be(registryVersion);
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeTrue();
        customer.ValidationInfo.HasValidationMessage.Should().BeFalse();
        customer.ValidationInfo.HasErrorMessages.Should().BeFalse();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(0);
    }

    [Fact]
    public void DomainEntityBase_Should_RegisterModification()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var tenantId = Guid.NewGuid();
        var executionUser = "marcelo.castelo@outlook.com";
        var lastSourcePlatform = "AppDemo";
        var correlationId = Guid.NewGuid();
        customer.RegisterNewExposed(tenantId, executionUser, lastSourcePlatform, correlationId);

        var initialId = customer.Id;
        var initialCreatedBy = customer.AuditableInfo.CreatedBy;
        var initialCreatedAt = customer.AuditableInfo.CreatedAt;
        var initialUpdatedAt = customer.AuditableInfo.LastUpdatedAt;
        var initialRegistryVersion = customer.RegistryVersion;
        var initialCorrelationId = customer.AuditableInfo.LastCorrelationId;

        var modificationExecutionUser = "marcelo.castelo@github.com";
        var modificationSourcePlatform = "AppDemo2";
        var modificationCorrelationId = Guid.NewGuid();

        // Act
        customer.RegisterModificationExposed(modificationExecutionUser, modificationSourcePlatform, modificationCorrelationId);

        // Assert
        customer.Id.Should().Be(initialId);
        customer.TenantId.Should().Be(tenantId);
        customer.AuditableInfo.CreatedBy.Should().Be(initialCreatedBy);
        customer.AuditableInfo.CreatedAt.Should().Be(initialCreatedAt);
        customer.AuditableInfo.LastCorrelationId.Should().NotBe(initialCorrelationId);
        customer.AuditableInfo.LastUpdatedBy.Should().Be(modificationExecutionUser);
        customer.AuditableInfo.LastUpdatedAt.Should().BeAfter(initialUpdatedAt ?? default);
        customer.AuditableInfo.LastSourcePlatform.Should().Be(modificationSourcePlatform);
        customer.AuditableInfo.LastCorrelationId.Should().Be(modificationCorrelationId);
        customer.RegistryVersion.Should().BeAfter(initialRegistryVersion);
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeTrue();
        customer.ValidationInfo.HasValidationMessage.Should().BeFalse();
        customer.ValidationInfo.HasErrorMessages.Should().BeFalse();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(0);
    }

    [Fact]
    public void DomainEntityBase_Should_Add_Validation_Message()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var customer2 = new Customer(dateTimeProvider);

        // Act
        customer.AddInformationValidationMessage("INFO_1", "INFORMATION");
        customer.AddWarningValidationMessage("WARNING_1", "WARNING");
        customer.AddErrorValidationMessage("ERROR_1", "ERROR");

        foreach (var validationMessage in customer.ValidationInfo.ValidationMessageCollection)
            customer2.AddValidationMessage(validationMessage);

        // Assert
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeFalse();
        customer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");

        customer2.ValidationInfo.Should().NotBeNull();
        customer2.ValidationInfo.IsValid.Should().BeFalse();
        customer2.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer2.ValidationInfo.HasErrorMessages.Should().BeTrue();
        customer2.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer2.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        customer2.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        customer2.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        customer2.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        customer2.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");
    }

    [Fact]
    public void DomainEntityBase_Should_ValidationInfo_Modified_Only_For_DomainEntity()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);

        // Act
        customer.AddInformationValidationMessage("INFO_1", "INFORMATION");
        customer.AddWarningValidationMessage("WARNING_1", "WARNING");
        customer.AddErrorValidationMessage("ERROR_1", "ERROR");
        customer.ValidationInfo.AddErrorValidationMessage("ERROR_2", "ERROR");

        // Assert
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeFalse();
        customer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");
    }

    [Fact]
    public void DomainEntityBase_Should_DeepCloneInternal()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var tenantId = Guid.NewGuid();
        var correlationId = Guid.NewGuid();
        var executionUser = "marcelo.castelo@outlook.com";
        var lastSourcePlatform = "AppDemo";
        var initialCreatedAt = customer.AuditableInfo.CreatedAt;
        var initialRegistryVersion = customer.RegistryVersion;
        var initialLastCorrelationId = customer.AuditableInfo.LastCorrelationId;

        customer.RegisterNewExposed(tenantId, executionUser, lastSourcePlatform, correlationId);

        customer.AddInformationValidationMessage("INFO_1", "INFORMATION");
        customer.AddWarningValidationMessage("WARNING_1", "WARNING");
        customer.AddErrorValidationMessage("ERROR_1", "ERROR");
        customer.ValidationInfo.AddErrorValidationMessage("ERROR_2", "ERROR");

        // Act
        var newCustomer = customer.DeepCloneInternalExposed();
        customer.AddErrorValidationMessage("ERROR_2", "ERROR");
        customer.SetExistingInfoExposed(
            id: Guid.NewGuid(),
            tenantId: Guid.NewGuid(),
            createdBy: Guid.NewGuid().ToString(),
            createdAt: dateTimeProvider.GetDate().UtcDateTime,
            lastUpdatedBy: Guid.NewGuid().ToString(),
            lastUpdatedAt: dateTimeProvider.GetDate().UtcDateTime,
            lastSourcePlatform: Guid.NewGuid().ToString(),
            registryVersion: dateTimeProvider.GetDate().UtcDateTime,
            correlationId
        );

        // Assert
        newCustomer.Id.Should().NotBe(default(Guid));
        newCustomer.TenantId.Should().Be(tenantId);
        newCustomer.AuditableInfo.CreatedBy.Should().Be(executionUser);
        newCustomer.AuditableInfo.CreatedAt.Should().BeAfter(initialCreatedAt);
        newCustomer.AuditableInfo.LastCorrelationId.Should().NotBe(initialLastCorrelationId);
        newCustomer.AuditableInfo.LastUpdatedBy.Should().BeNull();
        newCustomer.AuditableInfo.LastUpdatedAt.Should().BeNull();
        newCustomer.AuditableInfo.LastSourcePlatform.Should().Be(lastSourcePlatform);
        newCustomer.AuditableInfo.LastCorrelationId.Should().Be(correlationId);
        newCustomer.RegistryVersion.Should().BeAfter(initialRegistryVersion);

        newCustomer.ValidationInfo.Should().NotBeNull();
        newCustomer.ValidationInfo.IsValid.Should().BeFalse();
        newCustomer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        newCustomer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        newCustomer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        newCustomer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        newCustomer.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");
    }

    [Fact]
    public void DomainEntityBase_Should_Validate()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);

        // Act
        var isValid = customer.Validate();

        // Assert
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        isValid.Should().BeFalse();
        customer.ValidationInfo.IsValid.Should().Be(isValid);
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        var validationMessageCollection = customer.ValidationInfo.ValidationMessageCollection.ToList();

        validationMessageCollection[0].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        validationMessageCollection[0].Code.Should().Be(Customer.ERROR_CODE);
        validationMessageCollection[0].Description.Should().Be(Customer.ERROR_DESCRIPTION);

        validationMessageCollection[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        validationMessageCollection[1].Code.Should().Be(Customer.WARNING_CODE);
        validationMessageCollection[1].Description.Should().Be(Customer.WARNING_DESCRIPTION);

        validationMessageCollection[2].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        validationMessageCollection[2].Code.Should().Be(Customer.INFO_CODE);
        validationMessageCollection[2].Description.Should().Be(Customer.INFO_DESCRIPTION);
    }

    [Fact]
    public void DomainEntityBase_Should_AddFromValidationResult()
    {
        // Arrange
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var validationResult = new ValidationResult(new[] {
            new ValidationMessage(ValidationMessageType.Information, "INFO_1", "INFORMATION"),
            new ValidationMessage(ValidationMessageType.Warning, "WARNING_1", "WARNING"),
            new ValidationMessage(ValidationMessageType.Error, "ERROR_1", "ERROR"),
        });

        // Act
        customer.AddFromValidationResult(validationResult);

        // Assert
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeFalse();
        customer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");
    }

    [Fact]
    public void DomainEntityBase_Should_AddFromValidationInfo()
    {
        // Arrange and Act
        var dateTimeProvider = new DateTimeProvider();
        var customer = new Customer(dateTimeProvider);
        var validationInfo = new ValidationInfoValueObject();

        validationInfo.AddInformationValidationMessage("INFO_1", "INFORMATION");
        validationInfo.AddWarningValidationMessage("WARNING_1", "WARNING");
        validationInfo.AddErrorValidationMessage("ERROR_1", "ERROR");

        // Act
        customer.AddFromValidationInfo(validationInfo);

        // Assert
        customer.ValidationInfo.Should().NotBeNull();
        customer.ValidationInfo.IsValid.Should().BeFalse();
        customer.ValidationInfo.HasValidationMessage.Should().BeTrue();
        customer.ValidationInfo.HasErrorMessages.Should().BeTrue();
        customer.ValidationInfo.ValidationMessageCollection.Should().NotBeNull();
        customer.ValidationInfo.ValidationMessageCollection.Should().HaveCount(3);

        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].ValidationMessageType.Should().Be(ValidationMessageType.Information);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Code.Should().Be("INFO_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[0].Description.Should().Be("INFORMATION");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].ValidationMessageType.Should().Be(ValidationMessageType.Warning);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Code.Should().Be("WARNING_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[1].Description.Should().Be("WARNING");

        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].ValidationMessageType.Should().Be(ValidationMessageType.Error);
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Code.Should().Be("ERROR_1");
        customer.ValidationInfo.ValidationMessageCollection.ToList()[2].Description.Should().Be("ERROR");
    }

    #region Models
    public class Customer
        : DomainEntityBase
    {
        // Constants
        public static readonly string ERROR_CODE = "code_1";
        public static readonly string ERROR_DESCRIPTION = "description_1";

        public static readonly string WARNING_CODE = "code_2";
        public static readonly string WARNING_DESCRIPTION = "description_2";

        public static readonly string INFO_CODE = "code_3";
        public static readonly string INFO_DESCRIPTION = "description_3";

        // Constructors
        public Customer(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider)
        {
        }

        // Protected Abstract Methods
        protected override DomainEntityBase CreateInstanceForCloneInternal()
        {
            var dateTimeProvider = new DateTimeProvider();
            return new Customer(dateTimeProvider);
        }

        // Protected Methods
        public void AddValidationMessage(ValidationMessageType validationMessageType, string code, string description)
            => AddValidationMessageInternal(validationMessageType, code, description);
        public void AddValidationMessage(ValidationMessage validationMessage)
            => AddValidationMessageInternal(validationMessage);

        public void AddInformationValidationMessage(string code, string description)
            => AddInformationValidationMessageInternal(code, description);

        public void AddWarningValidationMessage(string code, string description)
            => AddWarningValidationMessageInternal(code, description);

        public void AddErrorValidationMessage(string code, string description)
            => AddErrorValidationMessageInternal(code, description);

        public void AddFromValidationResult(ValidationResult validationResult)
            => AddFromValidationResultInternal(validationResult);

        public void AddFromValidationInfo(ValidationInfoValueObject validationInfo)
            => AddFromValidationInfoInternal(validationInfo);

        public bool Validate()
        {
            return Validate(() =>
                new ValidationResult(
                    new List<ValidationMessage> {
                            new ValidationMessage(
                                ValidationMessageType: ValidationMessageType.Error,
                                Code: ERROR_CODE,
                                Description: ERROR_DESCRIPTION
                            ),
                            new ValidationMessage(
                                ValidationMessageType: ValidationMessageType.Warning,
                                Code: WARNING_CODE,
                                Description: WARNING_DESCRIPTION
                            ),
                            new ValidationMessage(
                                ValidationMessageType: ValidationMessageType.Information,
                                Code: INFO_CODE,
                                Description: INFO_DESCRIPTION
                            )
                    }
                )
            );
        }

        public DomainEntityBase RegisterNewExposed(
            Guid tenantId,
            string executionUser,
            string lastSourcePlatform,
            Guid correlationId
        ) => RegisterNewInternal<Customer>(tenantId, executionUser, lastSourcePlatform, correlationId: correlationId);

        public DomainEntityBase SetExistingInfoExposed(
            Guid id,
            Guid tenantId,
            string createdBy,
            DateTime createdAt,
            string lastUpdatedBy,
            DateTime? lastUpdatedAt,
            string lastSourcePlatform,
            DateTime registryVersion,
            Guid correlationId
        ) => SetExistingInfoInternal<Customer>(id, tenantId, createdBy, createdAt, lastUpdatedBy, lastUpdatedAt, lastSourcePlatform, registryVersion, lastCorrelationId: correlationId);

        public DomainEntityBase RegisterModificationExposed(
            string executionUser,
            string lastSourcePlatform,
            Guid correlationId
        ) => RegisterModificationInternal<Customer>(executionUser, lastSourcePlatform, correlationId);

        public Customer DeepCloneInternalExposed()
            => DeepCloneInternal<Customer>();
    } 
    #endregion
}

