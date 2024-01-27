using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ExceptionHelper
{
    [Serializable]
    public class InfinityInvalidDataException : InfinityException
    {
        public InfinityInvalidDataException(string message)
            : base(message, (int)HttpStatusCode.BadRequest)
        {
        }

        public InfinityInvalidDataException(string format, params object[] args) : 
            this(string.Format(format, args))
        {

        }
    }
}
