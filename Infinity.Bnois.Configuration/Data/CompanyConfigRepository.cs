

using Infinity.Bnois.Data.Core;

namespace Infinity.Bnois.Configuration.Data
{
    public abstract class CompanyConfigRepository<T> : Repository<ConfigurationDbContext, T>, ICompanyConfigRepository<T>
        where T : class
    {
        public CompanyConfigRepository(ConfigurationDbContext context) : base(context)
        {
        }
    }
}
