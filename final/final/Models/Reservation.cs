using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class Reservation
    {
        string name;
        string host_name;
        float price;
        string location;

        int reserv_number;
        int property_id;
        int user_id;
        string check_in;
        string check_out;
        float total_amount;

        public string Name { get => name; set => name = value; }
        public string Host_name { get => host_name; set => host_name = value; }
        public float Price { get => price; set => price = value; }
        public string Location { get => location; set => location = value; }
        public int Reserv_number { get => reserv_number; set => reserv_number = value; }
        public int Property_id { get => property_id; set => property_id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public string Check_in { get => check_in; set => check_in = value; }
        public string Check_out { get => check_out; set => check_out = value; }
        public float Total_amount { get => total_amount; set => total_amount = value; }

        public Reservation() { }

        public Reservation(int reserv_number, int property_id, int user_id, string check_in, string check_out) {
            this.Reserv_number = reserv_number;
            this.Property_id = property_id;
            this.User_id = user_id;
            this.Check_in = check_in;
            this.Check_out = check_out;
        }


        public Reservation(int property_id, int user_id, string check_in, string check_out,float total_amount)
        {
            this.Property_id = property_id;
            this.User_id = user_id;
            this.Check_in = check_in;
            this.Check_out = check_out;
            this.total_amount = total_amount;
        }


        public Reservation(string name, string host_name, float price, string location, int reserv_number, int property_id, int user_id, string check_in, string check_out)
        {
            this.name = name;
            this.host_name = host_name;
            this.price = price;
            this.location = location;
            this.reserv_number = reserv_number;
            this.property_id = property_id;
            this.user_id = user_id;
            this.check_in = check_in;
            this.check_out = check_out;
        }

        public Reservation(string name, string host_name, float price, string location, int reserv_number, int property_id, int user_id, string check_in, string check_out, float total_amount)
        {
            this.name = name;
            this.host_name = host_name;
            this.price = price;
            this.location = location;
            this.reserv_number = reserv_number;
            this.property_id = property_id;
            this.user_id = user_id;
            this.check_in = check_in;
            this.check_out = check_out;
            this.total_amount = total_amount;
        }



        //insert new reservation
        public int InsertReservation()
        {
            DataServices ds = new DataServices();
            return ds.InsertReservation(this);
        }

        public int Delete(int reserv_number)
        {
            DataServices ds = new DataServices();
            return ds.Delete_reservation(reserv_number);
        }

        public List<Reservation> Read(int user_id)
        {
            DataServices ds = new DataServices();
            return ds.readReservations(user_id);
        }


    }
}