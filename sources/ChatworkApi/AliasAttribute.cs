namespace ChatworkApi;

using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class AliasAttribute : Attribute
{
    public string Value { get; }

    public AliasAttribute(string value)
    {
        Value = value;
    }
}