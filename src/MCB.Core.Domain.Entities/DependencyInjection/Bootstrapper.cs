﻿using MCB.Core.Domain.Entities.Abstractions.Specifications;
using MCB.Core.Domain.Entities.DomainEntitiesBase.Specifications;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Abstractions.Interfaces;

namespace MCB.Core.Domain.Entities.DependencyInjection;
public class Bootstrapper
{
    public static void ConfigureDependencyInjection(
        IDependencyInjectionContainer dependencyInjectionContainer
    )
    {
        dependencyInjectionContainer.RegisterSingleton<IDomainEntitySpecifications, DomainEntitySpecifications>();
    }
}
