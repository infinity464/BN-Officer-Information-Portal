using System;
using System.Net;

namespace Infinity.Bnois.ExceptionHelper
{
    [Serializable]
    public class InfinityArgumentMissingException : InfinityException
    {
        public InfinityArgumentMissingException(string message)
            : base(message, (int)HttpStatusCode.BadRequest)
        {
        }

        public InfinityArgumentMissingException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
