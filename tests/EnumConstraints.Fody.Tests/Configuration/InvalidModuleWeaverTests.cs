using System.Reflection;
using System.Resources;
using System.Xml.Linq;
using EnumConstraints.Fody.Tests.Common;

namespace EnumConstraints.Fody.Tests.Configuration;

[TestClass]
public class InvalidModuleWeaverTests
{
    [TestMethod]
    public void ShouldNotAnException_On_Invalid_Properties()
    {
        var testResult = TestResultFactory.Create("wrong", "true");
        var sut = testResult.GetInstance(typeof(Mixin).FullName!);
        sut.Auto = (StatusAsInt64)0;
        ((StatusAsInt64)sut.Auto).Should().Be(0);
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ShouldThrowAnException_On_Invalid_Log()
    {
        TestResultFactory.Create("true", "wrong");
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ShouldThrowAnException_On_Invalid_LogAndProperties()
    {
        TestResultFactory.Create("wrong", "wrong");
    }
}
