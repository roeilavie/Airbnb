using final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api/Users")]
        public List<User> Get()
        {
            User u = new User();
            return u.ReadAllUsers();
        }

        [HttpGet]
        [Route("api/Users/{email}/{password}")]
        // GET api/<controller>/5
        public User Get(string email, string password)
        {
            User u = new User();
            User user = u.Read(email, password);
            return user;
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Users")]
        public User Post(User u)
        {
            User user = u.Insert();
            return user;
        }



        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Users/{user_total_income}/{user_total_reservation}/{user_id}")]
        public int Put_(float user_total_income, int user_total_reservation, int user_id)
        {
            User u = new User();
            return u.update(user_total_income, user_total_reservation, user_id);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Users/{user_total_income}/{user_total_reservation}/{user_id}/{user_total_cancellations}/{property_id}")]
        public int Put(float user_total_income, int user_total_reservation, int user_id, int user_total_cancellations,int property_id)
        {
            User u = new User();
            return u.update_cancel(user_total_income, user_total_reservation, user_id, user_total_cancellations, property_id);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}