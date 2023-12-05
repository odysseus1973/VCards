﻿using System.ComponentModel;
using FolkerKinzel.VCards.Enums;
using FolkerKinzel.VCards.Extensions;
using FolkerKinzel.VCards.Models;
using FolkerKinzel.VCards.Models.PropertyParts;
using FolkerKinzel.VCards.Resources;

namespace FolkerKinzel.VCards.BuilderParts;

[SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
public readonly struct DateAndOrTimeBuilder
{
    private readonly VCardBuilder? _builder;
    private readonly Prop _prop;

    [MemberNotNull(nameof(_builder))]
    private VCardBuilder Builder => _builder ?? throw new InvalidOperationException(Res.DefaultCtor);

    internal DateAndOrTimeBuilder(VCardBuilder builder, Prop prop)
    {
        _builder = builder;
        _prop = prop;
    }

    public VCardBuilder Add(int year, int month, int day, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromDate(year, month, day, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Add(int month, int day, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromDate(month, day, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Add(DateOnly date, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromDate(date, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Add(DateTimeOffset dateTime, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromDateTime(dateTime, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Add(TimeOnly time, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromTime(time, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Add(string? text, Action<ParameterSection>? parameters = null, Func<VCard, string?>? group = null)
    {
        Builder.VCard.Set(_prop, VCardBuilder.Add(DateAndOrTimeProperty.FromText(text, group?.Invoke(_builder.VCard)),
                                                  Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop),
                                                  parameters,
                                                  false));
        return _builder;
    }

    public VCardBuilder Clear()
    {
        Builder.VCard.Set(_prop, null);
        return _builder;
    }

    public VCardBuilder Remove(Func<DateAndOrTimeProperty, bool> predicate)
    {
        Builder.VCard.Set(_prop, Builder.VCard.Get<IEnumerable<DateAndOrTimeProperty?>?>(_prop).Remove(predicate));
        return _builder;
    }

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals([NotNullWhen(true)] object? obj) => base.Equals(obj);

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode() => base.GetHashCode();

    /// <inheritdoc/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string ToString() => base.ToString()!;
}
