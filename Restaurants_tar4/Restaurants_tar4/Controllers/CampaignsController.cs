using Restaurants_tar4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restaurants_tar4.Controllers
{
    public class CampaignsController : ApiController
    {
        // GET api/<controller>
        public List<Campaign> Get()
        {
            Campaign camp = new Campaign();
            return camp.Read(); 
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Campaign camp)
        {
            try
            {
                List<Campaign> campList = camp.Update();
                return Request.CreateResponse(HttpStatusCode.OK, campList);
            }

            catch
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}