using AssemblyToProcess;
using AssemblyToProcess.Extensions;
using Fody;
using TestResult = Fody.TestResult;

namespace EnumConstraints.Fody.Tests;

[TestClass]
public class ModuleWeaverBaseTests
{
#if (NET46)
    static TestResult testResult = new ModuleWeaver().ExecuteTestRun("AssemblyToProcess.dll");
#else
    protected static readonly TestResult testResult = new ModuleWeaver().ExecuteTestRun(
        "AssemblyToProcess.dll",
        false
    );
#endif

    protected static dynamic GetInstance<T>()
        where T : class
    {
        return testResult.GetInstance(typeof(T).FullName!)!;
    }
}
