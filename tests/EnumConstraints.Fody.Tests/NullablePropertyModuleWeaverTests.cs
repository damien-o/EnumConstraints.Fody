using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class NullablePropertyModuleWeaverTests : ModuleWeaverBaseTests
{
    [TestMethod]
    public void ValidValueCanBeObtained()
    {
        var sut = GetInstance<AutoProperty>();
        sut.NullableByteAutoProperty = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableByteAutoProperty);
    }

    [TestMethod]
    public void PropertyShouldRetainValue()
    {
        var sut = GetInstance<AutoProperty>();

        sut.NullableByteAutoProperty = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableByteAutoProperty);

        sut.NullableByteAutoProperty = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.NullableByteAutoProperty);
    }

    [TestMethod]
    public void ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<AutoProperty>();
        var sutObject = (object)sut;
        sutObject.Should().NotBeNull();
        Assert.IsNull(sut.NullableByteAutoProperty);
    }
}
