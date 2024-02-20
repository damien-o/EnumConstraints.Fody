using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fody;
using TestResult = Fody.TestResult;

namespace EnumConstraints.Fody.Tests.Common;

internal static class TestResultFactory
{
    public static TestResult Create(XElement config)
    {
        var moduleWeaver = new ModuleWeaver { Config = config };

#if (NET46)
        return moduleWeaver.ExecuteTestRun("AssemblyToProcess.dll");
#else
        return moduleWeaver.ExecuteTestRun("AssemblyToProcess.dll", false);
#endif
    }

    public static TestResult Create(string properties, string log)
    {
        return Create(
            new(
                "EnumConstraints",
                new XAttribute("Properties", properties),
                new XAttribute("Log", log)
            )
        );
    }

    public static TestResult Create(bool properties, bool log)
    {
        return Create(properties.ToString().ToLower(), log.ToString().ToLower());
    }
}
