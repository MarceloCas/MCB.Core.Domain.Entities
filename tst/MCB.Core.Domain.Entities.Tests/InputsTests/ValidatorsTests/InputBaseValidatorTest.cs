using FluentAssertions;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;
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
            sourcePlatform: new string('a', 150)
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.HasError.Should().BeFalse();
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
            sourcePlatform: null
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.HasError.Should().BeTrue();
        validationResult.HasValidationMessage.Should().BeTrue();
        validationResult.ValidationMessageCollection.Should().NotBeEmpty();
        validationResult.ValidationMessageCollection.Should().HaveCount(3);
    }

    [Fact]
    public void InputBaseValidator_Should_Validate_Maximum_Length()
    {
        // Arrange
        var dummyInput = new DummyInput(
            tenantId: Guid.NewGuid(),
            executionUser: new string('a', 151),
            sourcePlatform: new string('a', 151)
        );
        var dummyInputValidator = new DummyInputValidator();

        // Act
        var validationResult = dummyInputValidator.Validate(dummyInput);

        // Assert
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.HasError.Should().BeTrue();
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
        string sourcePlatform
    ) : base(tenantId, executionUser, sourcePlatform)
    {
    }
}

public class DummyInputValidator
    : InputBaseValidator<DummyInput>
{
    protected override void ConfigureFluentValidationConcreteValidatorInternal(ValidatorBase<DummyInput>.FluentValidationValidatorWrapper fluentValidationValidatorWrapper)
    {

    }
}
