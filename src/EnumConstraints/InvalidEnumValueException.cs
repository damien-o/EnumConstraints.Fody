using System;

namespace EnumConstraints
{
    /// <summary>
    /// Custom exception class to handle cases where an invalid enum value is encountered
    /// </summary>
    public class InvalidEnumValueException : Exception
    {
        /// <summary>
        /// Constructor to initialize the exception with the invalid enum value and its type
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <param name="type">Enum type</param>
        public InvalidEnumValueException(Enum? value, Type type)
        {
            EnumType = type;
            EnumValue = value;
        }

        /// <summary>
        /// Enum value
        /// </summary>
        public Enum? EnumValue { get; }

        /// <summary>
        /// Enum type
        /// </summary>
        public Type EnumType { get; }

        /// <summary>
        /// Checks if an enum value is invalid and throws an exception if it is
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <exception cref="InvalidEnumValueException"></exception>
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
