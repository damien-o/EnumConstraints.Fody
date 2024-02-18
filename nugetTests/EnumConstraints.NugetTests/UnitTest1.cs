using EnumConstraints.NugetTests.Sut;

namespace EnumConstraints.NugetTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CanSetAndGetAValidValue()
    {
        var sut = new Model();

        sut.Comparison = StringComparison.Ordinal;
        sut.Comparison.Should().Be(StringComparison.Ordinal);
    }

    [TestMethod]
    public void CanSetAValidValue()
    {
        var sut = new Model();
        Assert.ThrowsException<InvalidEnumValueException>(
            () => sut.Comparison = (StringComparison)50
        );
    }
}
