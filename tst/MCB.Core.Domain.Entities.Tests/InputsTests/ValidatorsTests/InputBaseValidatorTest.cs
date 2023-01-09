using FluentAssertions;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Validators;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator;
using System;
using System.Linq;
using Xunit;

namespace MCB.Core.Domain.Entities.Tests.InputsTests.ValidatorsTests;

public class InputBaseValidatorTest
{
    [Fact]
    public void InputBaseValidator_Should_Be_Valid()
    {
        // Arrange
        var dummyInput = new DummyInput(
            tenantId: Guid.NewGuid(),
            executionUser: new string('a', 150),
            sourcePlatform: new string('a', 150),
            correlationId: Guid.NewGuid()
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.HasInformationMessages.Should().BeFalse();
        validationResult.HasErrorMessages.Should().BeFalse();
        validationResult.HasWariningMessages.Should().BeFalse();
        validationResult.HasValidationMessage.Should().BeFalse();
        validationResult.ValidationMessageCollection.Should().BeEmpty();
    }

    [Fact]
    public void InputBaseValidator_Should_Validate_Required_Fields()
    {
        // Arrange
        var dummyInput = new DummyInput(
            tenantId: Guid.Empty,
            executionUser: null,
            sourcePlatform: null,
            correlationId: Guid.Empty
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.HasErrorMessages.Should().BeTrue();
        validationResult.HasValidationMessage.Should().BeTrue();
        validationResult.ValidationMessageCollection.Should().NotBeEmpty();
        validationResult.ValidationMessageCollection.Should().HaveCount(4);
    }

    [Fact]
    public void InputBaseValidator_Should_Validate_Maximum_Length()
    {
        // Arrange
        var dummyInput = new DummyInput(
            tenantId: Guid.NewGuid(),
            executionUser: new string('a', 151),
            sourcePlatform: new string('a', 151),
            correlationId: Guid.NewGuid()
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.HasErrorMessages.Should().BeTrue();
        validationResult.HasValidationMessage.Should().BeTrue();
        validationResult.ValidationMessageCollection.Should().NotBeEmpty();
        validationResult.ValidationMessageCollection.Should().HaveCount(2);

        var validationMessageCollection = validationResult.ValidationMessageCollection.ToList();
    }
}

public record DummyInput
    : InputBase
{
    public DummyInput(
        Guid tenantId,
        string executionUser,
        string sourcePlatform,
        Guid correlationId
    ) : base(tenantId, executionUser, sourcePlatform, correlationId)
    {
    }
}

public class DummyInputValidator
    : InputBaseValidator<DummyInput>
{
    public DummyInputValidator() 
        : base(new InputBaseSpecifications())
    {
    }

    protected override void ConfigureFluentValidationConcreteValidatorInternal(ValidatorBase<DummyInput>.FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {

    }
}
