using System.Collections.Generic;
using System.Linq;
using Infinity.Bnois.Api.Web.Data;

namespace Infinity.Bnois.Api.Web.Services
{ 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageService'
    public class LanguageService : ILanguageService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageService'
    {
        private readonly ILanguageRepository languageRepository;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageService.LanguageService(ILanguageRepository)'
        public LanguageService(ILanguageRepository languageRepository)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageService.LanguageService(ILanguageRepository)'
        {
            this.languageRepository = languageRepository;
        }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageService.GetLanguages()'
        public List<SelectModel> GetLanguages()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageService.GetLanguages()'
        {
            return languageRepository.All().Select(x=> new SelectModel { Value = x.CultureCode, Text= x.DisplayName}).ToList();
        }
    }
}
