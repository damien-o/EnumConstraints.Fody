using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace EnumConstraints.Fody
{
    internal class ClassProcessor
    {
        private readonly MethodReference _throwIfInvalid;

        public ClassProcessor(ModuleDefinition module)
        {
            _throwIfInvalid = module.ImportReference(
                typeof(InvalidEnumValueException)
                    .GetMethods()
                    .Single(m => m.Name == nameof(InvalidEnumValueException.ThrowIfInvalid))
            );
        }

        internal void ProcessProperties(
            TypeDefinition typeDefinition,
            PropertyDefinition propertyDefinition
        )
        {
            if (propertyDefinition.SetMethod.HasBody)
            {
                AddSetDecorator(typeDefinition, propertyDefinition);
            }
            if (propertyDefinition.GetMethod.HasBody)
            {
                AddGetDecorator(typeDefinition, propertyDefinition);
            }
        }

        private void AddGetDecorator(TypeDefinition typeDefinition, PropertyDefinition property)
        {
            var getMethod = property.GetMethod;

            var newGetMethod = new MethodDefinition(
                "get_Constraint" + getMethod.Name,
                getMethod.Attributes,
                getMethod.ReturnType
            );

            var il = newGetMethod.Body.Instructions;

            var tempVar = new VariableDefinition(
                typeDefinition.Module.ImportReference(getMethod.ReturnType)
            );
            newGetMethod.Body.Variables.Add(tempVar);
            newGetMethod.Body.InitLocals = true;

            /// call Property;
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Call, property.GetMethod));

            il.Add(Instruction.Create(OpCodes.Dup));

            // throwIfInvalid(tempVar)
            il.Add(Instruction.Create(OpCodes.Stloc, tempVar));
            il.Add(Instruction.Create(OpCodes.Box, getMethod.ReturnType));
            il.Add(Instruction.Create(OpCodes.Call, _throwIfInvalid));

            // return tempVar
            il.Add(Instruction.Create(OpCodes.Ldloc, tempVar));
            il.Add(Instruction.Create(OpCodes.Ret));
            typeDefinition.Methods.Add(newGetMethod);

            property.GetMethod = newGetMethod;
        }

        private void AddSetDecorator(TypeDefinition typeDefinition, PropertyDefinition property)
        {
            var setMethod = property.SetMethod;

            var newSetMethod = new MethodDefinition(
                "set_Constraint" + setMethod.Name,
                setMethod.Attributes,
                setMethod.ReturnType
            );

            var valueParameter = setMethod.Parameters[0];

            newSetMethod.Parameters.Add(new ParameterDefinition(valueParameter.ParameterType));

            var il = newSetMethod.Body.Instructions;

            /// call _throwIfInvalid;
            il.Add(Instruction.Create(OpCodes.Ldarg_1));
            il.Add(Instruction.Create(OpCodes.Box, valueParameter.ParameterType));
            il.Add(Instruction.Create(OpCodes.Call, _throwIfInvalid));

            /// call set property;
            il.Add(Instruction.Create(OpCodes.Ldarg_0));
            il.Add(Instruction.Create(OpCodes.Ldarg_1));
            il.Add(Instruction.Create(OpCodes.Call, property.SetMethod));

            il.Add(Instruction.Create(OpCodes.Ret));
            typeDefinition.Methods.Add(newSetMethod);

            property.SetMethod = newSetMethod;
        }
    }
}
