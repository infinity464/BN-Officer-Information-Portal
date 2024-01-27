using System.Collections.Generic;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ILanguageService'
    public interface ILanguageService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ILanguageService'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ILanguageService.GetLanguages()'
        List<SelectModel> GetLanguages();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ILanguageService.GetLanguages()'
    }
}
