using EnumConstraints.NugetTests.Sut;

namespace EnumConstraints.NugetTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CanSetAndGetAValidValue()
    {
        var sut = new Model { Value = EnumValue.Value1 };
        sut.Value.Should().Be(EnumValue.Value1);
    }

    [TestMethod]
    public void ThrowAnException_When_SetAnUnvalidValue()
    {
        var sut = new Model();
        var ex = Assert.ThrowsException<InvalidEnumValueException>(() => sut.Value = (EnumValue)50);
        ex.EnumValue.Should().Be((EnumValue)50);
    }

    [TestMethod]
    public void ThrowAnException_When_GetAnUnvalidValue()
    {
        var sut = new Model();
        var ex = Assert.ThrowsException<InvalidEnumValueException>(() => sut.Value);
        ex.EnumValue.Should().Be((EnumValue)0);
    }
}
