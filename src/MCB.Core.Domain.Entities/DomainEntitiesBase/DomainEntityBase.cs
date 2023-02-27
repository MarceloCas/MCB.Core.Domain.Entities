using MCB.Core.Domain.Entities.Abstractions;
using MCB.Core.Domain.Entities.Abstractions.ValueObjects;
using MCB.Core.Infra.CrossCutting.Abstractions.DateTime;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator.Abstractions.Enums;
using MCB.Core.Infra.CrossCutting.DesignPatterns.Validator.Abstractions.Models;

namespace MCB.Core.Domain.Entities.DomainEntitiesBase;

public abstract class DomainEntityBase
    : IDomainEntity
{
    // Private Methods
    private ValidationInfoValueObject _validationInfo;

    // Protected Methods
    protected IDateTimeProvider DateTimeProvider { get; private set; }

    // Properties
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public AuditableInfoValueObject AuditableInfo { get; private set; }
    public DateTime RegistryVersion { get; private set; }
    public ValidationInfoValueObject ValidationInfo => _validationInfo.DeepClone();

    // Constructors
    protected DomainEntityBase(IDateTimeProvider dateTimeProvider)
    {
        DateTimeProvider = dateTimeProvider;
        AuditableInfo = new AuditableInfoValueObject();
        _validationInfo = new ValidationInfoValueObject();
    }

    // Private Methods
    private void SetDateTimeProvider<TDomainEntityBase>(IDateTimeProvider dateTimeProvider)
        where TDomainEntityBase : DomainEntityBase
    {
        DateTimeProvider = dateTimeProvider;
    }
    private void SetId<TDomainEntityBase>(Guid id)
        where TDomainEntityBase : DomainEntityBase
    {
        Id = id;
    }
    private void GenerateNewId<TDomainEntityBase>()
        where TDomainEntityBase : DomainEntityBase
    {
        SetId<TDomainEntityBase>(Guid.NewGuid());
    }
    private void SetTenant<TDomainEntityBase>(Guid tenantId)
        where TDomainEntityBase : DomainEntityBase
    {
        TenantId = tenantId;
    }
    private void SetAuditableInfo<TDomainEntityBase>(string createdBy, DateTime createdAt, string? lastUpdatedBy, DateTime? lastUpdatedAt, string lastSourcePlatform, Guid lastCorrelationId)
        where TDomainEntityBase : DomainEntityBase
    {
        AuditableInfo = new AuditableInfoValueObject(
            createdBy,
            createdAt,
            lastUpdatedBy,
            lastUpdatedAt,
            lastSourcePlatform,
            lastCorrelationId
        );
    }
    private void SetRegistryVersion<TDomainEntityBase>(DateTime registryVersion)
         where TDomainEntityBase : DomainEntityBase
    {
        RegistryVersion = registryVersion;
    }
    private void GenerateNewRegistryVersion<TDomainEntityBase>()
        where TDomainEntityBase : DomainEntityBase
    {
        SetRegistryVersion<TDomainEntityBase>(DateTimeProvider.GetDate().UtcDateTime);
    }
    private void SetValidationInfo<TDomainEntityBase>(ValidationInfoValueObject validationInfoValueObject)
         where TDomainEntityBase : DomainEntityBase
    {
        _validationInfo = validationInfoValueObject;
    }

    // Protected Abstract Methods
    protected abstract DomainEntityBase CreateInstanceForCloneInternal();

    protected void AddValidationMessageInternal(ValidationMessageType validationMessageType, string code, string description)
    {
        _validationInfo.AddValidationMessage(validationMessageType, code, description);
    }
    protected void AddValidationMessageInternal(ValidationMessage validationMessage)
    {
        _validationInfo.AddValidationMessage(
            validationMessage.ValidationMessageType, 
            validationMessage.Code, 
            validationMessage.Description
        );
    }
    protected void AddInformationValidationMessageInternal(string code, string description)
    {
        AddValidationMessageInternal(ValidationMessageType.Information, code, description);
    }
    protected void AddWarningValidationMessageInternal(string code, string description)
    {
        AddValidationMessageInternal(ValidationMessageType.Warning, code, description);
    }
    protected void AddErrorValidationMessageInternal(string code, string description)
    {
        AddValidationMessageInternal(ValidationMessageType.Error, code, description);
    }
    protected void AddFromValidationResultInternal(ValidationResult validationResult)
    {
        if (!validationResult.HasValidationMessage)
            return;

        foreach (var validationMessage in validationResult.ValidationMessageCollection)
            AddValidationMessageInternal(validationMessage);
    }
    protected void AddFromValidationInfoInternal(ValidationInfoValueObject validationInfo)
    {
        if (!validationInfo.HasValidationMessage)
            return;

        foreach (var validationMessage in validationInfo.ValidationMessageCollection)
            AddValidationMessageInternal(validationMessage);
    }

    protected virtual bool Validate(Func<ValidationResult> handle)
    {
        foreach (var validationMessage in handle().ValidationMessageCollection)
            AddValidationMessageInternal(
                validationMessage.ValidationMessageType,
                validationMessage.Code,
                validationMessage.Description
            );

        return _validationInfo.IsValid;
    }
    protected virtual bool Validate(Func<ValidationInfoValueObject> handle)
    {
        foreach (var validationMessage in handle().ValidationMessageCollection)
            AddValidationMessageInternal(
                validationMessage.ValidationMessageType,
                validationMessage.Code,
                validationMessage.Description
            );

        return _validationInfo.IsValid;
    }

    protected void RegisterNewInternal<TDomainEntityBase>(Guid tenantId, string executionUser, string sourcePlatform, Guid correlationId)
        where TDomainEntityBase : DomainEntityBase
    {
        GenerateNewId<TDomainEntityBase>();
        SetTenant<TDomainEntityBase>(tenantId);
        SetAuditableInfo<TDomainEntityBase>(
            createdBy: executionUser,
            createdAt: DateTimeProvider.GetDate().UtcDateTime,
            lastUpdatedBy: null,
            lastUpdatedAt: null,
            lastSourcePlatform: sourcePlatform,
            lastCorrelationId: correlationId
        );
        GenerateNewRegistryVersion<TDomainEntityBase>();
    }
    protected void SetExistingInfoInternal<TDomainEntityBase>(Guid id, Guid tenantId, string createdBy, DateTime createdAt, string? lastUpdatedBy, DateTime? lastUpdatedAt, string lastSourcePlatform, DateTime registryVersion, Guid lastCorrelationId)
        where TDomainEntityBase : DomainEntityBase
    {
        SetId<TDomainEntityBase>(id);
        SetTenant<TDomainEntityBase>(tenantId);
        SetAuditableInfo<TDomainEntityBase>(
            createdBy,
            createdAt,
            lastUpdatedBy,
            lastUpdatedAt,
            lastSourcePlatform,
            lastCorrelationId
        );
        SetRegistryVersion<TDomainEntityBase>(registryVersion);
    }

    protected void RegisterModificationInternal<TDomainEntityBase>(string executionUser, string sourcePlatform, Guid correlationId)
        where TDomainEntityBase : DomainEntityBase
    {
        SetAuditableInfo<TDomainEntityBase>(
            createdBy: AuditableInfo.CreatedBy,
            createdAt: AuditableInfo.CreatedAt,
            lastUpdatedBy: executionUser,
            lastUpdatedAt: DateTimeProvider.GetDate().UtcDateTime,
            lastSourcePlatform: sourcePlatform,
            lastCorrelationId: correlationId
        );
        GenerateNewRegistryVersion<TDomainEntityBase>();
    }

    protected TDomainEntityBase DeepCloneInternal<TDomainEntityBase>()
        where TDomainEntityBase : DomainEntityBase
    {
        var instance = (TDomainEntityBase)CreateInstanceForCloneInternal();

        instance.SetExistingInfoInternal<TDomainEntityBase>(
            Id,
            TenantId,
            AuditableInfo.CreatedBy,
            AuditableInfo.CreatedAt,
            AuditableInfo.LastUpdatedBy,
            AuditableInfo.LastUpdatedAt,
            AuditableInfo.LastSourcePlatform!,
            RegistryVersion,
            AuditableInfo.LastCorrelationId
        );
        instance.SetValidationInfo<TDomainEntityBase>(ValidationInfo);
        instance.SetDateTimeProvider<TDomainEntityBase>(DateTimeProvider);

        return instance;
    }
}
