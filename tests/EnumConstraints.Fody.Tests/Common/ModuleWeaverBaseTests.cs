using System.Xml.Linq;
using Fody;
using TestResult = Fody.TestResult;

namespace EnumConstraints.Fody.Tests.Common;

public class ModuleWeaverBaseTests
{
    private readonly bool _log;
    private readonly bool _properties;
    private readonly XElement? _customConfig;

    private static readonly Dictionary<string, TestResult> testResults = new();

    protected ModuleWeaverBaseTests(XElement? customConfig)
    {
        _customConfig = customConfig;
    }

    protected ModuleWeaverBaseTests(bool properties = true, bool log = true)
    {
        _log = log;
        _properties = properties;
    }

    public TestResult GetTestResult()
    {
        var key = GetType().FullName!;
        if (testResults.TryGetValue(key, out var testResult))
            return testResult;

        testResult = TestResultFactory.Create(
            _customConfig
                ?? new(
                    "EnumConstraints",
                    new XAttribute("Properties", _properties.ToString().ToLowerInvariant()),
                    new XAttribute("Log", _log.ToString().ToLowerInvariant())
                )
        );
        testResults[key] = testResult;
        return testResult;
    }

    protected dynamic GetInstance<T>()
        where T : class
    {
        return GetTestResult().GetInstance(typeof(T).FullName!)!;
    }
}
