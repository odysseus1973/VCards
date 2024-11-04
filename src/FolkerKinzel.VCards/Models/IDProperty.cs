using System.ComponentModel;
using FolkerKinzel.VCards.Enums;
using FolkerKinzel.VCards.Intls.Converters;
using FolkerKinzel.VCards.Intls.Deserializers;
using FolkerKinzel.VCards.Intls.Encodings;
using FolkerKinzel.VCards.Intls.Extensions;
using FolkerKinzel.VCards.Intls.Serializers;
using FolkerKinzel.VCards.Models.PropertyParts;

namespace FolkerKinzel.VCards.Models;

/// <summary>Represents the vCard property <c>UID</c>, which stores a unique identifier
/// for the vCard subject.</summary>
/// <seealso cref="VCard.ID"/>
/// <seealso cref="ContactID"/>
/// <seealso cref="RelationProperty"/>
public sealed class IDProperty : VCardProperty
{
    [Obsolete("Use the constructor that takes a ContactID.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ExcludeFromCodeCoverage]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IDProperty(string? group = null)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        : base(new ParameterSection(), group) => throw new NotImplementedException();

    /// <summary>Copy ctor.</summary>
    /// <param name="prop">The <see cref="IDProperty"/> instance
    /// to clone.</param>
    private IDProperty(IDProperty prop) : base(prop)
        => Value = prop.Value;

    /// <summary> Initializes a new <see cref="IDProperty" /> object with a 
    /// specified <see cref="ContactID"/>. </summary>
    /// <param name="id">A <see cref="Guid" /> value.</param>
    /// <param name="group">Identifier of the group of <see cref="VCardProperty"
    /// /> objects, which the <see cref="VCardProperty" /> should belong to, or <c>null</c>
    /// to indicate that the <see cref="VCardProperty" /> does not belong to any group.</param>
    /// <exception cref="ArgumentNullException"><paramref name="id"/> is <c>null</c>.</exception>
    public IDProperty(ContactID id, string? group = null)
        : base(new ParameterSection(), group)
        => Value = id ?? throw new ArgumentNullException(nameof(id));

    internal IDProperty(VcfRow vcfRow, VCdVersion version) : base(vcfRow.Parameters, vcfRow.Group)
    {
        if (Parameters.DataType == Data.Text)
        {
            string val = vcfRow.Parameters.Encoding == Enc.QuotedPrintable
                ? vcfRow.Value.Span.UnMaskAndDecodeValue(vcfRow.Parameters.CharSet)
                : vcfRow.Value.Span.UnMaskValue(version);

            Value = string.IsNullOrWhiteSpace(val) ? ContactID.Empty : ContactID.Create(val);
            return;
        }

        if (UuidConverter.TryAsGuid(vcfRow.Value.Span, out Guid uuid))
        {
            Value = ContactID.Create(uuid);
        }

        string uriString = vcfRow.Value.Span.UnMaskValue(version);

        if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri? uri))
        {
            Value = ContactID.Create(uri);
        }

        Value = string.IsNullOrWhiteSpace(uriString) ? ContactID.Empty : ContactID.Create(uriString);
    }

    /// <summary> The <see cref="ContactID"/> provided by the <see cref="IDProperty" />.
    /// </summary>
    public new ContactID Value
    {
        get;
    }

    /// <inheritdoc />
    public override bool IsEmpty => Value.IsEmpty;

    /// <inheritdoc />
    public override object Clone() => new IDProperty(this);


    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override object? GetVCardPropertyValue() => Value;

    internal override void PrepareForVcfSerialization(VcfSerializer serializer)
    {
        base.PrepareForVcfSerialization(serializer);

        if (Value.IsString) { Parameters.DataType = Data.Text; }

        if (serializer.Version == VCdVersion.V2_1 && Value.IsString && Value.String.NeedsToBeQpEncoded())
        {
            this.Parameters.Encoding = Enc.QuotedPrintable;
            this.Parameters.CharSet = VCard.DEFAULT_CHARSET;
        }
    }

    internal override void AppendValue(VcfSerializer serializer)
    {
        Debug.Assert(serializer is not null);
        StringBuilder builder = serializer.Builder;

        if (Value.IsGuid)
        {
            _ = builder.AppendUuid(this.Value.Guid.Value, serializer.Version);
        }
        else if (Value.IsUri)
        {
            // URIs are not masked according to the "Verifier notes" in
            // https://www.rfc-editor.org/errata/eid3845
            // It says that "the ABNF does not support escaping for URIs."
            _ = builder.Append(Value.Uri.AbsoluteUri);
        }
        else
        {
            _ = serializer.Version == VCdVersion.V2_1
                ? this.Parameters.Encoding == Enc.QuotedPrintable
                    ? builder.AppendQuotedPrintable(Value.String.AsSpan(), builder.Length)
                    : builder.Append(Value.String)
                : builder.AppendValueMasked(Value.String, serializer.Version);
        }
    }
}
