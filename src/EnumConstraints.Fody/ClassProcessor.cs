using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace EnumConstraints.Fody
{
    internal class ClassProcessor
    {
        private readonly MethodReference _throwIfInvalid;
        private readonly ModuleWeaver _moduleWeaver;
        private readonly bool _log;

        public ClassProcessor(
            ModuleWeaver moduleWeaver,
            bool log,
            ModuleDefinition module,
            TypeDefinition invalidEnumValueExceptionType
        )
        {
            if (invalidEnumValueExceptionType is null)
                throw new TypeLoadException(
                    "Can't find typeDef: EnumConstraints.InvalidEnumValueException"
                );
            var type = module.ImportReference(invalidEnumValueExceptionType).Resolve();
            if (type is null)
                throw new TypeLoadException(
                    "Can't resolve type: EnumConstraints.InvalidEnumValueException"
                );
            var method = type.GetMethods().FirstOrDefault(m => m.Name == "ThrowIfInvalid");
            if (method is null)
                throw new TypeLoadException(
                    "Can't find method: EnumConstraints.InvalidEnumValueException.ThrowIfInvalid"
                );
            _throwIfInvalid = module.ImportReference(method);
            _moduleWeaver = moduleWeaver;
            _log = log;
        }

        internal void ProcessProperties(
            TypeDefinition typeDefinition,
            PropertyDefinition propertyDefinition
        )
        {
            if (!CanOverrideBehavior(propertyDefinition))
            {
                return;
            }
            bool getter = false;
            bool setter = false;
            if (propertyDefinition.SetMethod.HasBody)
            {
                AddSetDecorator(typeDefinition, propertyDefinition);
                setter = true;
            }
            if (propertyDefinition.GetMethod.HasBody)
            {
                AddGetDecorator(typeDefinition, propertyDefinition);
                getter = true;
            }
            if (!_log)
                return;

            var prop = $"{typeDefinition.FullName}.{propertyDefinition.Name}";
            if (getter && setter)
            {
                _moduleWeaver.WriteWarning($"Add getter and setter check on {prop}");
                return;
            }
            if (getter)
            {
                _moduleWeaver.WriteWarning($"Add getter check on {prop}");
                return;
            }
            if (setter)
            {
                _moduleWeaver.WriteWarning($"Add setter check on {prop}");
            }
        }

        private void AddGetDecorator(TypeDefinition typeDefinition, PropertyDefinition property)
        {
            var getMethod = property.GetMethod;

            var newGetMethod = new MethodDefinition(
                getMethod.Name,
                getMethod.Attributes,
                getMethod.ReturnType
            );

            getMethod.Name += "_Unchecked";

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
                setMethod.Name,
                setMethod.Attributes,
                setMethod.ReturnType
            );

            setMethod.Name += "_Unchecked";
            var valueParameter = setMethod.Parameters[0];

            newSetMethod.Parameters.Add(
                new ParameterDefinition(
                    "uncheckedEnumValue",
                    ParameterAttributes.None,
                    valueParameter.ParameterType
                )
            );

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

        private static bool CanOverrideBehavior(PropertyDefinition propertyDefinition)
        {
            var propertyType = propertyDefinition.PropertyType.Resolve();
            if (propertyType is null)
                return false;

            if (
                propertyDefinition.PropertyType is GenericInstanceType instance
                && propertyType.FullName == "System.Nullable`1"
            )
            {
                propertyType = instance.GenericArguments[0].Resolve();
            }

            if (!propertyType.IsEnum)
                return false;

            if (
                propertyType.CustomAttributes.Any(ca =>
                    ca.AttributeType.FullName == typeof(FlagsAttribute).FullName
                )
            )
                return false;

            return true;
        }
    }
}
