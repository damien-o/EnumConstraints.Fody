using System;
using System.Collections.Generic;
using System.Linq;
using Fody;

namespace EnumConstraints.Fody
{
    public partial class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
        {
            var properties = bool.Parse(Config?.Attribute("Properties")?.Value ?? "true");

            if (!properties)
                return;

            var allTypes = ModuleDefinition.GetTypes().Where(t => t.IsClass).ToArray();

            var processor = new ClassProcessor(ModuleDefinition);
            foreach (var typeDefinition in allTypes)
            {
                foreach (var propertyDefinition in typeDefinition.Properties)
                {
                    processor.ProcessProperties(typeDefinition, propertyDefinition);
                }
            }
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            return Array.Empty<string>();
        }
    }
}
