using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class PropertyModuleWeaverTests : ModuleWeaverBaseTests
{
    [TestMethod]
    public void ValidValueCanBeObtained()
    {
        var sut = GetInstance<AutoProperty>();
        sut.ByteAutoProperty = StatusAsByte.One;
        Assert.AreEqual(StatusAsByte.One, (StatusAsByte)sut.ByteAutoProperty);
    }

    [TestMethod]
    public void PropertyShouldRetainValue()
    {
        var sut = GetInstance<AutoProperty>();

        sut.ByteAutoProperty = StatusAsByte.One;
        Assert.AreEqual(StatusAsByte.One, sut.ByteAutoProperty);

        sut.ByteAutoProperty = StatusAsByte.Two;
        Assert.AreEqual(StatusAsByte.Two, sut.ByteAutoProperty);
    }

    [TestMethod]
    public void ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<AutoProperty>();
        var sutObject = (object)sut;
        sutObject.Should().NotBeNull();

        var result = Assert.ThrowsException<InvalidEnumValueException>(() => sut.ByteAutoProperty);

        result.EnumValue.Should().Be((StatusAsByte)0);
    }
}
