using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription'
    public class ParameterDescription
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.ParameterDescription()'
        public ParameterDescription()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.ParameterDescription()'
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Annotations'
        public Collection<ParameterAnnotation> Annotations { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Annotations'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Documentation'
        public string Documentation { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Documentation'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Name'
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.Name'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.TypeDescription'
        public ModelDescription TypeDescription { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterDescription.TypeDescription'
    }
}