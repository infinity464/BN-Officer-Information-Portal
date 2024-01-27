using System;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Use this attribute to change the name of the <see cref="ModelDescription"/> generated for a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelNameAttribute.ModelNameAttribute(string)'
        public ModelNameAttribute(string name)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelNameAttribute.ModelNameAttribute(string)'
        {
            Name = name;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelNameAttribute.Name'
        public string Name { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelNameAttribute.Name'
    }
}