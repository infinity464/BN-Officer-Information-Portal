
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Core
{
    public class ResponseMessage<T> : ErrorMessage
    {
        public int Total { get; set; }
        public T Result { get; set; }
        public RoleFeature Permission { get; set; }
        public ResponseMessage()
        {
            if (Permission == null)
            {
                Permission = new RoleFeature();
            }
        }
    }
}
