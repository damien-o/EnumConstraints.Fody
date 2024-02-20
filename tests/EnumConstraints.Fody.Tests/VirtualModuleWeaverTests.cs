using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class VirtualModuleWeaverTests : ModuleWeaverBaseTests
{
    [TestMethod]
    public void ValidValueCanBeObtained()
    {
        var sut = GetInstance<Virtual>();
        sut.Enum = StatusAsByte.One;
        Assert.AreEqual(StatusAsByte.One, sut.Enum);
    }

    [TestMethod]
    public void PropertyShouldRetainValue()
    {
        var sut = GetInstance<Virtual>();

        sut.Enum = StatusAsByte.One;
        Assert.AreEqual(StatusAsByte.One, sut.Enum);

        sut.Enum = StatusAsByte.Two;
        Assert.AreEqual(StatusAsByte.Two, sut.Enum);
    }

    [TestMethod]
    public void ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Virtual>();

        sut.inVirtual = (StatusAsByte)3;
        var result = Assert.ThrowsException<InvalidEnumValueException>(() => sut.Enum);

        result.EnumValue.Should().Be((StatusAsByte)3);
    }

    [TestMethod]
    public void ShouldThrow_When_SetAnInvalidEnumValue()
    {
        var sut = GetInstance<Virtual>();
        Assert.ThrowsException<InvalidEnumValueException>(() => sut.Enum = (StatusAsByte)43);
        ((byte)sut.inVirtual).Should().Be(0);
    }

    [TestMethod]
    public void ShouldOnlyCallLeafMethod_When_VirtualIsOveridden()
    {
        var sut = GetInstance<Virtual>();

        Assert.AreEqual((StatusAsByte)0, sut.inVirtualBase);
        Assert.AreEqual((StatusAsByte)0, sut.inVirtual);

        sut.Enum = StatusAsByte.One;
        Assert.AreEqual((StatusAsByte)0, sut.inVirtualBase);
        Assert.AreEqual(StatusAsByte.One, sut.inVirtual);
    }
}
