using AssemblyToProcess;
using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class MixinModuleWeaverTests : ModuleWeaverBaseTests
{
    #region NullableAuto
    [TestMethod]
    public void NullableValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        sut.NullableAuto = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.NullableAuto);
    }

    [TestMethod]
    public void NullablePropertyShouldRetainValue()
    {
        var sut = GetInstance<Mixin>();

        sut.NullableAuto = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableAuto);

        sut.NullableAuto = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.NullableAuto);
    }
    #endregion

    #region Auto

    [TestMethod]
    public void ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        sut.Auto = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.Auto);
    }

    [TestMethod]
    public void PropertyShouldRetainValue()
    {
        var sut = GetInstance<Mixin>();

        sut.Auto = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.Auto);

        sut.Auto = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.Auto);
    }

    [TestMethod]
    public void ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();
        var result = Assert.ThrowsException<InvalidEnumValueException>(() => sut.Auto);
        result.EnumValue.Should().Be((StatusAsInt64)0);
        result.EnumType.FullName.Should().Be(typeof(StatusAsInt64).FullName);
    }

    #endregion

    #region NullableReadonlyGet

    [TestMethod]
    public void NullableReadonlyGet_ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        Assert.AreEqual(null, sut.NullableReadonlyGet);
    }
    #endregion

    #region NullableAutoReadonlyGet

    [TestMethod]
    public void NullableAutoReadonlyGet_ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        Assert.AreEqual(null, sut.NullableAutoReadonlyGet);

        sut.value = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.NullableAutoReadonlyGet);

        sut.value = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.NullableAutoReadonlyGet);
    }

    [TestMethod]
    public void NullableAutoReadonlyGet_ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();
        sut.value = (StatusAsInt64)25;
        var result = Assert.ThrowsException<InvalidEnumValueException>(
            () => sut.NullableAutoReadonlyGet
        );
        result.EnumValue.Should().Be((StatusAsInt64)25);
    }

    #endregion

    #region NullableAutoReadonlyGet

    [TestMethod]
    public void AutoReadonlyGet_ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();

        sut.value = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.AutoReadonlyGet);

        sut.value = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.AutoReadonlyGet);
    }

    [TestMethod]
    public void AutoReadonlyGet_ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();
        sut.value = (StatusAsInt64)32;
        var result = Assert.ThrowsException<InvalidEnumValueException>(() => sut.AutoReadonlyGet);
        result.EnumValue.Should().Be((StatusAsInt64)32);
    }

    #endregion

    #region Auto
    [TestMethod]
    public void NullableGetInit_ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        Assert.AreEqual(StatusAsInt64.One, sut.NullableGetInit);
    }

    #endregion

    #region UnitializedGetInit


    [TestMethod]
    public void UnitializedGetInit_ShouldThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();
        var result = Assert.ThrowsException<InvalidEnumValueException>(
            () => sut.UnitializedGetInit
        );
        result.EnumValue.Should().Be((StatusAsInt64)0);
    }
    #endregion

    #region SetOnly


    [TestMethod]
    public void SetOnly_ValidValueCanBeObtained()
    {
        var sut = GetInstance<Mixin>();
        sut.SetOnly = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.value);
    }

    [TestMethod]
    public void SetOnly_PropertyShouldRetainValue()
    {
        var sut = GetInstance<Mixin>();

        sut.SetOnly = StatusAsInt64.One;
        Assert.AreEqual(StatusAsInt64.One, sut.value);

        sut.SetOnly = StatusAsInt64.Two;
        Assert.AreEqual(StatusAsInt64.Two, sut.value);
    }

    [TestMethod]
    public void SetOnly_ShouldThrow_When_SetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();

        var result = Assert.ThrowsException<InvalidEnumValueException>(
            () => sut.SetOnly = ((StatusAsInt64)(-10))
        );
        result.EnumValue.Should().Be((StatusAsInt64)(-10));
        result.EnumType.FullName.Should().Be(typeof(StatusAsInt64).FullName);
    }
    #endregion
}
