using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class FullPropertyModuleWeaverTests : ModuleWeaverBaseTests
{
    [TestMethod]
    public void ValidValueCanBeObtained()
    {
        var sut = GetInstance<FullProperty>();
        sut.Property = StatusAsShort.One;
        Assert.AreEqual(StatusAsShort.One, sut.Property);
    }

    [TestMethod]
    public void PropertyShouldRetainValue()
    {
        var sut = GetInstance<FullProperty>();

        sut.Property = StatusAsShort.One;
        Assert.AreEqual(StatusAsShort.One, sut.Property);

        sut.Property = StatusAsShort.Two;
        Assert.AreEqual(StatusAsShort.Two, sut.Property);
    }

    [TestMethod]
    public void ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<FullProperty>();
        var sutObject = (object)sut;
        sutObject.Should().NotBeNull();

        sut.field = (StatusAsShort)3;
        var result = Assert.ThrowsException<InvalidEnumValueException>(() => sut.Property);

        result.EnumValue.Should().Be((StatusAsShort)3);
    }

    [TestMethod]
    public void ShouldThrow_When_SetAnInvalidEnumValue()
    {
        var sut = GetInstance<FullProperty>();
        Assert.ThrowsException<InvalidEnumValueException>(() => sut.Property = (StatusAsShort)43);

        Assert.IsNull(sut.Property);
    }
}
