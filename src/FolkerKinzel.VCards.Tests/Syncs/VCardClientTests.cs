﻿#if !NETCOREAPP3_1
using FolkerKinzel.Strings.Polyfills;
#endif

namespace FolkerKinzel.VCards.Syncs.Tests;

[TestClass]
public class VCardClientTests
{
    [TestMethod]
    public void PropertyIDMappingTest1()
    {
        var pidMap = new AppID(5, "http://folkerkinzel.de/");
        Assert.AreEqual(5, pidMap.LocalID);
    }

    [DataTestMethod()]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void PropertyIDMappingTest2(int mappingNumber) => _ = new AppID(mappingNumber, "http://folker.de/");

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException), AllowDerivedTypes = false)]
    public void PropertyIDMappingTest3() => _ = new AppID(0, "http://folkerkinzel.de/");

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public void PropertyIDMappingTest4() => _ = new AppID(3, null!);


    [TestMethod]
    public void TryParseTest1()
    {
        string pidMap = "2;urn:uuid:d89c9c7a-2e1b-4832-82de-7e992d95faa5";

        Assert.IsTrue(AppID.TryParse(pidMap, out AppID? client));

        Assert.AreEqual(2, client.LocalID);
        Assert.AreEqual(new Uri(pidMap.Substring(2)), client.GlobalID);
    }

    [TestMethod]
    public void ParseTest2()
    {
        string pidMap = "  2 ; urn:uuid:d89c9c7a-2e1b-4832-82de-7e992d95faa5";

        Assert.IsTrue(AppID.TryParse(pidMap, out AppID? client));

        Assert.AreEqual(2, client.LocalID);
        Assert.AreEqual(new Uri(pidMap.Substring(6)), client.GlobalID);
    }

    [TestMethod]
    public void ParseTest3()
    {
        string pidMap = "22;urn:uuid:d89c9c7a-2e1b-4832-82de-7e992d95faa5";

        Assert.IsTrue(AppID.TryParse(pidMap, out _));
    }

    [TestMethod]
    public void ParseTest4()
    {
        string pidMap = "2;http://d89c9c7a-2e1b-4832-82de-7e992d95faa5";

        Assert.IsTrue(AppID.TryParse(pidMap, out _));
    }

    [TestMethod]
    public void ParseTest5()
    {
        string pidMap = "2";

        Assert.IsFalse(AppID.TryParse(pidMap, out _));
    }

    [TestMethod]
    public void ParseTest6()
    {
        string pidMap = "";

        Assert.IsFalse(AppID.TryParse(pidMap, out _));
    }

    [TestMethod]
    public void ParseTest7()
    {
        string pidMap = "a";

        Assert.IsFalse(AppID.TryParse(pidMap, out _));
    }

    //[TestMethod]
    //public void ParseTest8()
    //{
    //    string pidMap = "2;http:////////////////// ";
    //    Assert.IsFalse(VCardClient.TryParse(pidMap, out _));
    //}


    [TestMethod]
    public void ToStringTest1()
    {
        int i = 4;

        var pidmap = new AppID(i, "http://folkerkinzel.de/");

        string s = pidmap.ToString();

        Assert.IsNotNull(s);
        Assert.IsTrue(s.Contains(';'));
        Assert.IsTrue(5 < s.Length);
    }

}
