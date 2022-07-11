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
    public class ReservationsController : ApiController
    {

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Reservations/{user_id}")]
        public List<Reservation> Get(int user_id)
        {
            Reservation r = new Reservation();
            return r.Read(user_id);
        }

        // POST api/<controller>
        public int Post(Reservation resv)
        {
            int status = resv.InsertReservation();
            return status;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Reservations/{reserv_number}/{user_id}")]
        public List<Reservation> Delete(int reserv_number, int user_id)
        {
            //delete
            Reservation r = new Reservation();
            int numaffected = r.Delete(reserv_number); //0 if nothing was deleted
            return r.Read(user_id);
        }
    }
}