using final.Models;
using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final.Controllers
{
    public class PropertiesController : ApiController
    {
        // GET api/<controller>
        //properties in the index page
        public List<Property> Get()
        {
            Property prop = new Property();
            return prop.ReadTopRating();
        }

        [HttpGet]
        [Route("api/Properties/Properties")]
        // GET api/<controller>/5
        //properties for the admin
        public List<Property> Properties()
        {
            Property prop = new Property();
            return prop.ReadProperties();
        }


        [HttpGet]
        [Route("api/Properties/{property_id}")]
        //propertyPage
        public Property Get(int property_id)
        {
            Property prop = new Property();
            return prop.ReadProperty(property_id);
        }

        [HttpGet]
        [Route("api/Properties/Destinations")]
        //get all destinations
        // GET api/<controller>/5
        public List<string> Destinations()
        {
            Property prop = new Property();
            return prop.ReadDestinations();
        }


        [HttpPost]
        [Route("api/Properties/{search_property}")]
        // POST api/<controller>
        public List<Property> Post(PropertySearch search_property)
        {
            List<Property> properties = search_property.Read();
            return properties;
        }

        //from lean search
        [HttpPost]
        [Route("api/Properties/leanSearch/{search_property}")]
        // POST api/<controller>
        public List<Property> leanSearch(PropertySearch search_property)
        {
            List<Property> properties = search_property.ReadProp();
            return properties;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}