using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Infinity.Bnois.Api.Web.Controllers
{
    //[Authorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController'
    public class ValuesController : ApiController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController'
    {
        // GET api/values
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Get()'
        public IEnumerable<string> Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Get()'
        {
#pragma warning disable CS0219 // The variable 'total' is assigned but its value is never used
            int total = 0;
#pragma warning restore CS0219 // The variable 'total' is assigned but its value is never used
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Get(int)'
        public string Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Get(int)'
        {
            return "value";
        }

        // POST api/values
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Post(string)'
        public void Post([FromBody]string value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Post(string)'
        {
        }

        // PUT api/values/5
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Put(int, string)'
        public void Put(int id, [FromBody]string value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Put(int, string)'
        {
        }

        // DELETE api/values/5
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Delete(int)'
        public void Delete(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ValuesController.Delete(int)'
        {
        }
    }
}
