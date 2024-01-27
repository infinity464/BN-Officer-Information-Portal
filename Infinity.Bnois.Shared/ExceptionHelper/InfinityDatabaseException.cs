using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ExceptionHelper
{
    [Serializable]
    public class InfinityDatabaseException : InfinityException
    {
        public InfinityDatabaseException(string message)
            : base(message, (int)HttpStatusCode.InternalServerError)
        {
        }

        public InfinityDatabaseException(string message, Exception innerException)
            : base(message, (int)HttpStatusCode.InternalServerError, innerException)
        {
        }

        public InfinityDatabaseException(string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.InternalServerError)
        {
        }

        public InfinityDatabaseException(Exception innerException, string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.InternalServerError, innerException)
        {
        }
    }
}
