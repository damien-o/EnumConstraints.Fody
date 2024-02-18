using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;

namespace EnumConstraints.Fody
{
    public partial class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
        {
            var properties = bool.Parse(Config?.Attribute("Properties")?.Value ?? "true");
            var log = bool.Parse(Config?.Attribute("Log")?.Value ?? "false");
            if (!properties)
                return;
            var invalidEnumValueExceptionType = FindTypeDefinition(
                "EnumConstraints.InvalidEnumValueException"
            );

            var allTypes = ModuleDefinition.GetTypes().Where(t => t.IsClass).ToArray();

            var processor = new ClassProcessor(
                this,
                log,
                ModuleDefinition,
                invalidEnumValueExceptionType
            );
            foreach (var typeDefinition in allTypes)
            {
                foreach (var propertyDefinition in typeDefinition.Properties)
                {
                    processor.ProcessProperties(typeDefinition, propertyDefinition);
                }
            }
        }

        public override bool ShouldCleanReference => false;

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "EnumConstraints";
        }
    }
}
