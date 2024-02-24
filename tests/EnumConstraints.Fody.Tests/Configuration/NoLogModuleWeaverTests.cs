using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests.Configuration;

[TestClass]
public class NoLogModuleWeaverTests : ModuleWeaverBaseTests
{
    public NoLogModuleWeaverTests()
        : base(true, false) { }

    [TestMethod]
    public void Config_ValidValueCanBeObtained()
    {
        var sut = GetInstance<AutoProperty>();
        sut.NullableByteAutoProperty = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableByteAutoProperty);
    }

    [TestMethod]
    public void Config_PropertyShouldRetainValue()
    {
        var sut = GetInstance<AutoProperty>();

        sut.NullableByteAutoProperty = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableByteAutoProperty);

        sut.NullableByteAutoProperty = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.NullableByteAutoProperty);
    }

    [TestMethod]
    public void Config_ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<AutoProperty>();
        var sutObject = (object)sut;
        sutObject.Should().NotBeNull();
        Assert.IsNull(sut.NullableByteAutoProperty);
    }
}
