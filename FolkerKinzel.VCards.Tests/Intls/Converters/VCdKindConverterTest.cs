﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FolkerKinzel.VCards.Models.Enums;

namespace FolkerKinzel.VCards.Intls.Converters.Tests
{
    [TestClass()]
    public class VCdKindConverterTest
    {
        [TestMethod()]
        public void Roundtrip()
        {
            foreach (var kind in (VCdKind[])Enum.GetValues(typeof(VCdKind)))
            {
                var kind2 = VCdKindConverter.Parse(kind.ToString());

                Assert.AreEqual(kind, kind2);

                var kind3 = Enum.Parse(typeof(VCdKind), kind.ToVCardString(), true);

                Assert.AreEqual(kind, kind3);

                // Test auf null
                Assert.AreEqual(VCdKind.Individual, VCdKindConverter.Parse(null));

                // Test auf nicht definiert
                Assert.AreEqual(VCdKind.Individual.ToVCardString(), ((VCdKind)4711).ToVCardString());
            }
        }
    }
}