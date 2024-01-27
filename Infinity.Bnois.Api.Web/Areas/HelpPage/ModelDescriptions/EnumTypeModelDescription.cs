using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription'
    public class EnumTypeModelDescription : ModelDescription
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription.EnumTypeModelDescription()'
        public EnumTypeModelDescription()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription.EnumTypeModelDescription()'
        {
            Values = new Collection<EnumValueDescription>();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription.Values'
        public Collection<EnumValueDescription> Values { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EnumTypeModelDescription.Values'
    }
}