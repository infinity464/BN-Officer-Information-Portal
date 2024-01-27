using System.Collections.ObjectModel;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription'
    public class ComplexTypeModelDescription : ModelDescription
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription.ComplexTypeModelDescription()'
        public ComplexTypeModelDescription()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription.ComplexTypeModelDescription()'
        {
            Properties = new Collection<ParameterDescription>();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription.Properties'
        public Collection<ParameterDescription> Properties { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ComplexTypeModelDescription.Properties'
    }
}