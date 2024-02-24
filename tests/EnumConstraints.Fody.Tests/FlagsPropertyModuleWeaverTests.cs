using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class FlagsPropertyModuleWeaverTests : ModuleWeaverBaseTests
{
    [TestMethod]
    [DataRow(FlagStatus.None)]
    [DataRow(FlagStatus.Active)]
    [DataRow(FlagStatus.Hidden)]
    [DataRow(FlagStatus.Hidden | FlagStatus.Active)]
    public void ValidValueCanBeObtained(FlagStatus status)
    {
        var sut = GetInstance<FlagsProperty>();
        sut.Property = status;
        Assert.AreEqual(status, sut.Property);
    }
}
