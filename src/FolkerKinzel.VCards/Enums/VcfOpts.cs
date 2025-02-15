using FolkerKinzel.VCards.Extensions;
using FolkerKinzel.VCards.Models.Properties;
using FolkerKinzel.VCards.Models.Properties.Parameters;

namespace FolkerKinzel.VCards.Enums;

/// <summary>Named constants to specify options for writing VCF files. The constants
/// can be combined.</summary>
/// <remarks>
/// <note type="tip">
/// When working with the enum use the extension methods from the 
/// <see cref="VcfOptsExtension" /> class.
/// </note>
/// <para>
/// The flags <see cref="WriteWabExtensions" />, <see cref="WriteXExtensions" />,  
/// <see cref="WriteEvolutionExtensions" />, and <see cref="WriteKAddressbookExtensions" /> 
/// control the automatic generation of <see cref="NonStandardProperty"/> objects. 
/// Even if these flags are set, the <see cref="NonStandardProperty"/> objects are 
/// only generated automatically, if the selected vCard standard does not allow a 
/// standardized equivalent. 
/// </para>
/// </remarks>
[Flags]
public enum VcfOpts
{
    /// <summary>All flags are set.</summary>
    All = -1,

    /// <summary>All flags are unset.</summary>
    None = 0,

    /// <summary> 
    /// <para>
    /// Default setting
    /// </para>
    /// <para>
    /// (corresponds to  <see cref="WriteGroups" />
    /// | <see cref="WriteRfc6474Extensions" /> | <see cref="WriteRfc6715Extensions" /> 
    /// | <see cref="WriteImppExtension" /> | <see cref="WriteXExtensions" /> 
    /// | <see cref="AllowMultipleAdrAndLabelInVCard21" /> | <see cref="UpdateTimeStamp"/>
    /// | <see cref="WriteRfc2739Extensions"/> | <see cref="WriteRfc8605Extensions"/>
    /// | <see cref="WriteRfc9554Extensions"/>) | <see cref="WriteRfc9555Extensions"/>)
    /// </para>
    /// </summary>
    Default = WriteGroups | WriteRfc6474Extensions | WriteRfc6715Extensions
            | WriteImppExtension | WriteXExtensions | AllowMultipleAdrAndLabelInVCard21
            | UpdateTimeStamp | WriteRfc2739Extensions | WriteRfc8605Extensions
            | WriteRfc9554Extensions | WriteRfc9555Extensions,

    /// <summary>Set the flag to write property group identifiers.</summary>
    WriteGroups = 1,

    /// <summary>Set the flag to also write empty properties to the vCard.</summary>
    WriteEmptyProperties = 1 << 1,

    /// <summary>Set the flag to append in vCard&#160;2.1 and vCard&#160;3.0 embedded 
    /// <c>AGENT</c>-vCards in the VCF file to the main vCard.</summary>
    AppendAgentAsSeparateVCard = 1 << 2,

    /// <summary> Set the flag to write non-standard parameters to the VCF file.
    /// </summary>
    WriteNonStandardParameters = 1 << 3,

    /// <summary> Set the flag to write <see cref="NonStandardProperty" /> objects
    /// to the VCF file.</summary>
    WriteNonStandardProperties = 1 << 4,

    /// <summary>Set the flag to write the extensions from RFC 6474 (<c>BIRTHPLACE</c>,
    /// <c>DEATHPLACE</c>, <c>DEATHDATE</c>). (Beginning from vCard&#160;4.0.)</summary>
    WriteRfc6474Extensions = 1 << 5,

    /// <summary>Set the flag to write the extensions from RFC 6715 (<c>EXPERTISE</c>,
    /// <c>HOBBY</c>, <c>INTEREST</c>, <c>ORG-DIRECTORY</c>). 
    /// (Beginning from vCard&#160;4.0.)</summary>
    WriteRfc6715Extensions = 1 << 6,

    /// <summary>Set the flag to write the extension <c>IMPP</c> from RFC 4770 in 
    /// vCard&#160;3.0.</summary>
    WriteImppExtension = 1 << 7,

    /// <summary>Set the flag to write the following vCard&#160;properties (if necessary):
    /// <c>X-AIM</c>, <c>X-GADUGADU</c>, <c>X-GOOGLE-TALK</c>, <c>X-GTALK</c>, <c>X-ICQ</c>,
    /// <c>X-JABBER</c>, <c>X-MSN</c>, <c>X-SKYPE</c>, <c>X-TWITTER</c>, <c>X-YAHOO</c>,
    /// <c>X-MS-IMADDRESS</c>, <c>X-GENDER</c>, <c>X-ANNIVERSARY</c>, <c>X-SPOUSE</c>,
    /// <c>X-SOCIALPROFILE</c>, and the <c>X-SERVICE-TYPE</c> parameter.</summary>
    WriteXExtensions = 1 << 8,

    /// <summary>Set the flag to write the following vCard properties if necessary:
    /// <c>X-EVOLUTION-ANNIVERSARY</c>, <c>X-EVOLUTION-SPOUSE</c>.</summary>
    WriteEvolutionExtensions = 1 << 9,

    /// <summary>Set the flag to write the following vCard properties if necessary:
    /// <c>X-KADDRESSBOOK-X-IMAddress</c>, <c>X-KADDRESSBOOK-X-Anniversary</c>, 
    /// <c>X-KADDRESSBOOK-X-SpouseName</c>.</summary>
    WriteKAddressbookExtensions = 1 << 10,

    /// <summary>Set the flag to write the following vCard properties if necessary:
    /// <c>X-WAB-GENDER</c>, <c>X-WAB-WEDDING-ANNIVERSARY</c>, <c>X-WAB-SPOUSE-NAME</c>.</summary>
    WriteWabExtensions = 1 << 11,

    /// <summary>Set the flag to allow multiple "ADR" and "LABEL" properties to be written
    /// into a vCard&#160;2.1.</summary>
    AllowMultipleAdrAndLabelInVCard21 = 1 << 12,

    /// <summary>
    /// Set the flag to update the <see cref="VCard.Updated"/> property automatically when
    /// serializing VCF.
    /// </summary>
    UpdateTimeStamp = 1 << 13,

    /// <summary>
    /// Set the flag to call <see cref="SyncOperation.SetPropertyIDs"/> automatically when serializing
    /// a <see cref="VCard"/> as vCard&#160;4.0 (or higher).
    /// </summary>
    SetPropertyIDs = 1 << 14,

    /// <summary>
    /// Set the flag to set the <see cref="ParameterSection.Index"/> properties in all <see cref="VCardProperty"/>
    /// collections that contains more than one item automatically when serializing vCard&#160;4.0 (or higher). 
    /// The values depend on the positions of the <see cref="VCardProperty"/> objects in the collection 
    /// and on the flag <see cref="WriteEmptyProperties"/>. Any previously existing values will be overwritten.
    /// </summary>
    SetIndexes = 1 << 15,

    /// <summary>Set the flag to write the extensions from RFC 2739 (<c>FBURL</c>,
    /// <c>CALURI</c>, <c>CALADRURI</c>) in vCard&#160;3.0.</summary>
    WriteRfc2739Extensions = 1 << 16,

    /// <summary>Set the flag to write the extensions from RFC 8605 (<c>FBURL</c>,
    /// <c>CONTACT-URI</c> property, <c>CC</c> parameter) in vCard&#160;4.0.</summary>
    WriteRfc8605Extensions = 1 << 17,

    /// <summary>Set the flag to write the extensions from RFC 9554 in vCard&#160;4.0.</summary>
    WriteRfc9554Extensions = 1 << 18,

    /// <summary>Set the flag to write the extensions from RFC 9555 in vCard&#160;4.0.</summary>
    WriteRfc9555Extensions = 1 << 19,
}
