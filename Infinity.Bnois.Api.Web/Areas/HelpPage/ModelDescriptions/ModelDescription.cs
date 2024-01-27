using System;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.Documentation'
        public string Documentation { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.Documentation'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.ModelType'
        public Type ModelType { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.ModelType'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.Name'
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ModelDescription.Name'
    }
}