using FluentAssertions;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Inputs;
using System;
using Xunit;

namespace MCB.Core.Domain.Entities.Tests.InputsTests;

public class InputBaseTest
{

    [Fact]
    public void InputBase_Should_Correctly_initialized()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var executionUser = "marcelo.castelo";
        var sourcePlatform = "dummyPlatform";

        // Act
        var dummyInput = new DummyInput(tenantId, executionUser, sourcePlatform);

        // Assert
        dummyInput.Should().NotBeNull();
        dummyInput.TenantId.Should().Be(tenantId);
        dummyInput.ExecutionUser.Should().Be(executionUser);
        dummyInput.SourcePlatform.Should().Be(sourcePlatform);
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
}
