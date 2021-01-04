﻿using FolkerKinzel.VCards.Intls;
using FolkerKinzel.VCards.Intls.Attributes;
using FolkerKinzel.VCards.Intls.Converters;
using FolkerKinzel.VCards.Intls.Extensions;
using FolkerKinzel.VCards.Intls.Serializers;
using FolkerKinzel.VCards.Models.Enums;
using System;
using System.Text;
using FolkerKinzel.VCards.Models.PropertyParts;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FolkerKinzel.VCards.Models.Interfaces;


namespace FolkerKinzel.VCards.Models
{
    /// <summary>
    /// Repräsentiert die in vCard 4.0 eingeführte vCard-Property <c>GENDER</c>, die Informationen über das Geschlecht speichert.
    /// </summary>
    public sealed class GenderProperty : VCardProperty, IDataContainer<Gender>, IVCardData, IVcfSerializable, IVcfSerializableData
    {
        /// <summary>
        /// Initialisiert ein neues <see cref="GenderProperty"/>-Objekt.
        /// </summary>
        /// <param name="sex">Standardisierte Geschlechtsangabe.</param>
        /// <param name="genderIdentity">Freie Beschreibung des Geschlechts.</param>
        /// <param name="propertyGroup">(optional) Bezeichner der Gruppe,
        /// der die <see cref="VCardProperty">VCardProperty</see> zugehören soll, oder <c>null</c>,
        /// um anzuzeigen, dass die <see cref="VCardProperty">VCardProperty</see> keiner Gruppe angehört.</param>
        public GenderProperty(VCdSex? sex, string? genderIdentity = null, string? propertyGroup = null)
        {
            Value = new Gender(sex, genderIdentity);
            Group = propertyGroup;
        }


        /// <inheritdoc/>
        public Gender Value
        {
            get;
        }


        ///// <summary>
        ///// True, wenn das <see cref="GenderProperty"/>-Objekt keine Daten enthält.
        ///// </summary>
        /// <inheritdoc/>
        public override bool IsEmpty => Value.IsEmpty; // Value ist nie null


        /// <inheritdoc/>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        protected override object? GetContainerValue() => Value;




        internal GenderProperty(VcfRow vcfRow, StringBuilder builder)
            : base(vcfRow.Parameters, vcfRow.Group)
        {
            VCdSex? sex = null;
            string? genderIdentity = null;

            List<string> list = vcfRow.Value.SplitValueString(';');

            if (list.Count >= 1)
            {
                sex = VCdSexConverter.Parse(list[0]);
            }

            if (list.Count >= 2)
            {
                builder.Clear().Append(list[1]).UnMask(VCdVersion.V4_0).Trim();
                genderIdentity = builder.ToString();
            }

            Value = new Gender(sex, genderIdentity);
        }



        [InternalProtected]
        internal override void AppendValue(VcfSerializer serializer)
        {
            InternalProtectedAttribute.Run();

            Value.AppendVCardStringTo(serializer);
        }


    }
}
