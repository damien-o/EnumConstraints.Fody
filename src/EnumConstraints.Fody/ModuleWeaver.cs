using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Fody;

namespace EnumConstraints.Fody;

public partial class ModuleWeaver : BaseModuleWeaver
{
    public override void Execute()
    {
        var allTypes = ModuleDefinition.GetTypes().Where(t => t.IsClass).ToArray();

        var processor = new ClassProcessor(ModuleDefinition);
        foreach (var typeDefinition in allTypes)
        {
            foreach (var propertyDefinition in typeDefinition.Properties)
            {
                processor.Process(typeDefinition, propertyDefinition);
            }
        }
    }

    public override IEnumerable<string> GetAssembliesForScanning()
    {
        /// return Array.Empty<string>();
        return new[] { typeof(InvalidEnumValueException).Assembly.FullName! };
    }
}
