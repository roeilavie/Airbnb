using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class User
    {
        int id;
        string email;
        string fullName;
        string userName;
        string password;
        string sign_up_date;
        int total_reservation;
        float total_income;
        int total_cancels;


        public User()
        { }

        public User(int id, string email, string fullName, string userName, string password)
        {
            this.Id = id;
            this.Email = email;
            this.FullName = fullName;
            this.UserName = userName;
            this.Password = password;
        }

        public User(int id, string email, string fullName, string userName, string password, string sign_up_date, int total_reservation, float total_income, int total_cancels)
        {
            this.id = id;
            this.email = email;
            this.fullName = fullName;
            this.userName = userName;
            this.password = password;
            this.sign_up_date = sign_up_date;
            this.Total_reservation = total_reservation;
            this.Total_income = total_income;
            this.Total_cancels = total_cancels;
        }

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Sign_up_date { get => sign_up_date; set => sign_up_date = value; }
        public int Total_reservation { get => total_reservation; set => total_reservation = value; }
        public float Total_income { get => total_income; set => total_income = value; }
        public int Total_cancels { get => total_cancels; set => total_cancels = value; }

        public User Insert()
        {
            DataServices ds = new DataServices();
            return ds.InsertUser(this);
        }

        public User Read(string email, string password)
        {
            DataServices dataservices = new DataServices();
            User user = dataservices.GetUser(email, password);
            return user;
        }

        public int update_cancel(float user_total_income, int user_total_reservation, int user_id,int property_id, int user_total_cancellations)
        {
            DataServices ds = new DataServices();
            return ds.updateCancel(user_total_income, user_total_reservation, user_id, property_id, user_total_cancellations);
        }

        public int update(float user_total_income, int user_total_reservation, int user_id)
        {
            DataServices ds = new DataServices();
            return ds.updateUser(user_total_income, user_total_reservation, user_id);
        }

        public List<User> ReadAllUsers()
        {
            DataServices ds = new DataServices();
            return ds.ReadAllUsers();
        }
    }
}