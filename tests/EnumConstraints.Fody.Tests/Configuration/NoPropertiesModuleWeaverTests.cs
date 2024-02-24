using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests.Configuration;

[TestClass]
public class NoPropertiesModuleWeaverTests : ModuleWeaverBaseTests
{
    public NoPropertiesModuleWeaverTests()
        : base(false, true) { }

    [TestMethod]
    public void Auto_ShouldNotThrow_When_SetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();

        sut.Auto = (StatusAsInt64)35;
        ((StatusAsInt64)sut.Auto).Should().Be((StatusAsInt64)35);
    }

    [TestMethod]
    public void Auto_ShouldNotThrow_When_GetAnInvalidEnumValue()
    {
        var sut = GetInstance<Mixin>();
        var v = (StatusAsInt64)sut.Auto;
        v.Should().Be(0);
    }
}
