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
        public List<Campaign> Get()
        {
            Campaign camp = new Campaign();
            return camp.Read(); 
        }


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
        public HttpResponseMessage Post([FromBody] Campaign camp)
        {
            try
            {
                List<Campaign> campList1 = camp.Insert();
                return Request.CreateResponse(HttpStatusCode.OK, campList1);
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }

        [HttpDelete]
        [Route("api/Campaigns/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Campaign camp = new Campaign();
                List<Campaign> campList = camp.DeleteCamp(id);
                return Request.CreateResponse(HttpStatusCode.OK, campList);
            }

            catch
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }
        [HttpPut]
        [Route("api/Campaigns/{mode}/{id}")]
        public HttpResponseMessage Put(string mode,int id)
        {
            try
            {
                Campaign camp = new Campaign();
                List<Campaign> campList = camp.UpdateViewCamp(mode,id);
                return Request.CreateResponse(HttpStatusCode.OK, campList);
            }

            catch
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }
    }
}