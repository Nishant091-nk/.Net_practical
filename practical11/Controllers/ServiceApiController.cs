using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SmartCampusPortal.Controllers
{
    public class ServiceApiController : ApiController
    {
        ServiceRequestDAL dal = new ServiceRequestDAL();

        // GET: api/ServiceApi
        [HttpGet]
        public IHttpActionResult GetRequests()
        {
            return Ok(dal.GetAll());
        }

        // POST: api/ServiceApi
        [HttpPost]
        public IHttpActionResult CreateRequest(
            ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dal.Insert(request);

            return Ok("Service request created successfully");
        }

        // PUT: api/ServiceApi/UpdateStatus/1
        [HttpPut]
        [Route("api/ServiceApi/UpdateStatus/{id:int}")]
        public IHttpActionResult UpdateStatus(
            int id, [FromBody] string status)
        {
            dal.UpdateStatus(id, status);

            return Ok("Status updated successfully");
        }
    }
}