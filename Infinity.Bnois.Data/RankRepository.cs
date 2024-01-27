using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Data
{
    public class RankRepository : BnoisRepository<Rank>, IRankRepository
    {
        public RankRepository(BnoisDbContext context) : base(context)
        {
        }
    }
}
