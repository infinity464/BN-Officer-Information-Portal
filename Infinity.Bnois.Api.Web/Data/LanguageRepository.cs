using Infinity.Bnois.Api.Web.Models;
using System.Collections.Generic;
using System.Linq;


namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository'
    public class LanguageRepository : ILanguageRepository
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository'
    {
        private readonly InfinityIdentityDbContext context;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository.LanguageRepository(InfinityIdentityDbContext)'
        public LanguageRepository(InfinityIdentityDbContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository.LanguageRepository(InfinityIdentityDbContext)'
        {
            this.context = context;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository.All()'
        public List<Language> All()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LanguageRepository.All()'
        {
            return context.Languages.ToList();
        }
    }
}