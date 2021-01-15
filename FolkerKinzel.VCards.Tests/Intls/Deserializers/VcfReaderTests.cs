﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolkerKinzel.VCards.Intls.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FolkerKinzel.VCards.Tests;

namespace FolkerKinzel.VCards.Intls.Deserializers.Tests
{
    [TestClass()]
    public class VcfReaderTests
    {
        [TestMethod()]
        public void VcfReaderCtorTest()
        {
            using var reader = new StringReader("");
            _ = new VcfReader(reader, new VCardDeserializationInfo());
        }


        [TestMethod()]
        public void GetEnumeratorTest()
        {
            using StreamReader reader = File.OpenText(TestFiles.V3vcf);
            var info = new VCardDeserializationInfo();

            var list = new List<VcfRow>();

            var vcReader = new VcfReader(reader, info);

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreNotEqual(0, list.Count);
            Assert.IsFalse(vcReader.EOF);

            list.Clear();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreNotEqual(0, list.Count);
            Assert.IsFalse(vcReader.EOF);

            list.Clear();


            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(vcReader.EOF);
        }

        [TestMethod]
        public void LineWrappingV4()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:4.0")
                .AppendLine("FN:1234")
                .AppendLine(" 5678")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("12345678", list[1].Value);
        }


        [TestMethod]
        public void LineWrappingV3()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:3.0")
                .AppendLine("FN:1234")
                .AppendLine(" 5678")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("12345678", list[1].Value);
        }

        [TestMethod]
        public void LineWrappingV2()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:2.1")
                .AppendLine("FN:1234")
                .AppendLine(" 5678")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("1234 5678", list[1].Value);
        }

        [TestMethod]
        public void SimplePropsV4()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:4.0")
                .AppendLine("FN:KMS WSF")
                .AppendLine("N:KMS;WSF;;;")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void SimplePropsV3()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:3.0")
                .AppendLine("FN:KMS WSF")
                .AppendLine("N:KMS;WSF;;;")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(3, list.Count);
        }


        [TestMethod]
        public void SimplePropsV2()
        {
            StringBuilder? sb = new StringBuilder()
                .AppendLine("BEGIN:VCARD")
                .AppendLine("VERSION:2.1")
                .AppendLine("FN:KMS WSF")
                .AppendLine("N:KMS;WSF;;;")
                .AppendLine("END:VCARD");

            using var reader = new StringReader(sb.ToString());
            var vcReader = new VcfReader(reader, new VCardDeserializationInfo());

            var list = new List<VcfRow>();

            foreach (VcfRow vcfRow in vcReader)
            {
                list.Add(vcfRow);
            }

            Assert.AreEqual(3, list.Count);
        }
    }
}