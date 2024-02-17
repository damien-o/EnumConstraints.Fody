using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumConstraints
{
    public class InvalidEnumValueException : Exception
    {
        public InvalidEnumValueException(Enum? value, Type enumType)
        {
            EnumType = enumType;
            EnumValue = value;
        }

        public Type EnumType { get; }
        public Enum? EnumValue { get; }

        public static void ThrowIfInvalid(Enum? value)
        {
            if (value is null)
                return;
            if (Enum.IsDefined(value.GetType(), value))
                return;
            throw new InvalidEnumValueException(value, value.GetType());
        }
    }
}
