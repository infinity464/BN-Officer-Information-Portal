using log4net;
using System.Web.Http.ExceptionHandling;

namespace Infinity.Bnois.Api.Core
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(string.Format("Unhandled exception thrown in {0} for request {1}: {2}",
                context.Request.Method, context.Request.RequestUri, context.Exception));
        }
    }
}