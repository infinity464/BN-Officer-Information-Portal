using System;
using System.Reflection;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage.ModelDescriptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider'
    public interface IModelDocumentationProvider
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider.GetDocumentation(MemberInfo)'
        string GetDocumentation(MemberInfo member);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider.GetDocumentation(MemberInfo)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider.GetDocumentation(Type)'
        string GetDocumentation(Type type);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IModelDocumentationProvider.GetDocumentation(Type)'
    }
}