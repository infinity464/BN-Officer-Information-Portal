using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ExceptionHelper
{
    [Serializable]
    public class InfinityException : Exception
    {
        public InfinityException(string message, int errorCode)
            : base(message)
        {
            HResult = errorCode;
        }

        public InfinityException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            HResult = errorCode;
        }
    }
}
