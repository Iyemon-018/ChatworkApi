namespace ChatworkApi
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class ParameterValueAttribute : Attribute
    {
        public string Value { get; }

        public ParameterValueAttribute(string value)
        {
            Value = value;
        }
    }
}