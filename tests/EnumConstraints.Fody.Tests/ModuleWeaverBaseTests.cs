using System.Xml.Linq;
using Fody;
using TestResult = Fody.TestResult;

namespace EnumConstraints.Fody.Tests;

public class ModuleWeaverBaseTests
{
    protected ModuleWeaverBaseTests() { }

    [TestInitialize]
    public void Initilize()
    {
        if (testResult is not null)
            return;
        var moduleWeaver = new ModuleWeaver();
        moduleWeaver.Config = (
            XDocument.Parse(@"<EnumConstraints Properties=""true"" Log=""true""/>").Root!
        );

#if (NET46)
        testResult = moduleWeaver.ExecuteTestRun("AssemblyToProcess.dll");
#else
        testResult = moduleWeaver.ExecuteTestRun("AssemblyToProcess.dll", false);
#endif
    }

    private static TestResult? testResult;

    protected dynamic GetInstance<T>()
        where T : class
    {
        ArgumentNullException.ThrowIfNull(testResult);
        return testResult.GetInstance(typeof(T).FullName!)!;
    }
}
