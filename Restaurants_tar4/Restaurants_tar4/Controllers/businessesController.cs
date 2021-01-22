using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tar1.Models;
using tar2.Models;

using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace tar1.Controllers
{
    public class businessesController : ApiController
    {
        public List<Businesses> Get()
        {
            Businesses restaurant = new Businesses();
            return restaurant.ReadAll();
        }
        [HttpGet]
        [Route("api/businesses/getRestCamp/{cusineId}")]
        public List<Businesses> GetByCamp(int cusineId)
        {
            Businesses restcamp = new Businesses();
            return restcamp.ReadByCamp(cusineId);
        }

        [HttpGet]
        [Route("api/businesses/{cusineId}/{pr}")]
        public List<Businesses> Get(int cusineId, int pr)
        {
            Businesses restaurant = new Businesses();
            return restaurant.Read(cusineId,pr);
        }

        [HttpGet]
        [Route("api/businesses/checkCust")]
        public HttpResponseMessage GetCustomer(string email, string pass) {
            Customer c = new Customer();
            c.Email = email;
            c.Password = pass;
            try {
                c = c.Read(email, pass);
                return Request.CreateResponse(HttpStatusCode.OK, c);
            }
            catch {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "checking error");
            }
        }

        [HttpPost]
        [Route("api/businesses/favorites")]
        public HttpResponseMessage Post([FromBody] Businesses favourite)//*****we can delete this???
        {
            try
            {
                favourite.Insert();
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }

            catch {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }

        [HttpPost]
        [Route("api/businesses/highlights")]
        public HttpResponseMessage Post1([FromBody] Businesses highlight)//*****we can delete this???
        {
            try
            {
                highlight.InsertHighlight();
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }

            catch{
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }

        [HttpPost]
        [Route("api/businesses/cusTomer")]
        public HttpResponseMessage Post([FromBody] Customer cust)
        {
            try
            {
               Customer c = cust.Insert();
                return Request.CreateResponse(HttpStatusCode.OK, c);
            }
            catch {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"connecting error");
           }            
        }


        [HttpPost]
        [Route("api/businesses/FileUpload")]
        public HttpResponseMessage Post()
        {
            string imageLink;
            var httpContext = HttpContext.Current;
            string imgpath = "";

            // Check for any uploaded file
            if (httpContext.Request.Files.Count > 0)
            {
                HttpPostedFile httpPostedFile = httpContext.Request.Files[0];
                // this is an example of how you can extract addional values from the Ajax call
                string name = httpContext.Request.Form["name"];

                if (httpPostedFile != null)
                {
                    // Construct file save path
                    //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                    string fname = httpPostedFile.FileName.Split('\\').Last();
                    string sfname = fname.Split('.').Last();
                    var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), name + "." + sfname);
                    // Save the uploaded file
                    httpPostedFile.SaveAs(fileSavePath);
                    imgpath = fileSavePath;
                    imageLink = $"uploadedFiles/{name}.{sfname}";
                }
            }
            // Return status code
            return Request.CreateResponse(HttpStatusCode.Created, imgpath);
        }

        public HttpResponseMessage Put([FromBody] Customer c)
        {
            try
            {
                c.Update();
                return Request.CreateResponse(HttpStatusCode.OK, c);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "connecting error");
            }
        }
    }
}