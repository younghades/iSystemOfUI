using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iSystemOfUI.Models.SOModel;

namespace iSystemOfUI.Controllers.API
{
    public class SOController : ApiController
    {
        // GET: api/SO
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/SO/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SO
        public string New(SOModel value)
        {
            return value.New();
        }

        // PUT: api/SO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SO/5
        public void Delete(int id)
        {
        }
    }
}
