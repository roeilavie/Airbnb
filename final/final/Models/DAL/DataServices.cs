using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace final.Models.DAL
{
    public class DataServices
    {
        //read properties from databases - after advanced search
        public List<Property> ReadProperties()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadProperties");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Property> properties = new List<Property>();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                float price = Convert.ToSingle(dr["price"]);
                string property_type = dr["property_type"].ToString();
                string picture_url = dr["picture_url"].ToString();
                string name = dr["name"].ToString();
                string host_name = dr["host_name"].ToString();
                int total_cancel = Convert.ToInt32(dr["total_cancel"]);
                int total_rent_days = Convert.ToInt32(dr["total_days"]);
                properties.Add(new Property(id, price, property_type, picture_url, name, host_name, total_cancel, total_rent_days));
            }
            con.Close();
            return properties;
        }

        //read hosts information from the database for admin
        public List<Host> ReadHosts()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadHosts");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Host> hosts = new List<Host>();

            while (dr.Read())
            {
                int host_id = Convert.ToInt32(dr["host_id"]);
                DateTime host_date = Convert.ToDateTime(dr["host_since"].ToString());
                string host_since = host_date.ToShortDateString();
                string host_name = dr["host_name"].ToString();
                string host_neighbourhood = dr["host_neighbourhood"].ToString();
                string host_picture_url = dr["host_picture_url"].ToString();
                float total_income = Convert.ToSingle(dr["total income"]);
                int total_cancel = Convert.ToInt32(dr["total cancel"]);
                int num_of_properties = Convert.ToInt32(dr["num of properties"]);

                hosts.Add(new Host(host_id, host_since, host_name, host_neighbourhood, host_picture_url, total_income, total_cancel, num_of_properties));
            }

            con.Close();
            return hosts;
        }

        //read all users from database for admin
        public List<User> ReadAllUsers()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadAllUsers");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> users = new List<User>();

            while (dr.Read())
            {
                if (dr["userName"].ToString() == "admin")
                    continue;
                int id = Convert.ToInt32(dr["id"]);
                string email = dr["email"].ToString();
                string fullName = dr["fullName"].ToString();
                string userName = dr["userName"].ToString();
                string password = dr["password"].ToString();
                DateTime sign_up = Convert.ToDateTime(dr["sign_up_date"].ToString());
                string sign_up_date = sign_up.ToShortDateString();
                int total_reservation = Convert.ToInt32(dr["total_reservation"]);
                float total_income = Convert.ToSingle(dr["total_income"]);
                int total_cancels = Convert.ToInt32(dr["total_cancels"]);

                users.Add(new User(id, email, fullName, userName, password, sign_up_date, total_reservation, total_income, total_cancels));
            }

            con.Close();
            return users;
        }

        //update user and property after cancellation
        public int updateCancel(float user_total_income, int user_total_reservation, int user_id, int user_total_cancellations, int property_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@total_income", user_total_income);
            command.Parameters.AddWithValue("@total_reservation", user_total_reservation);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@user_total_cancellations", user_total_cancellations);

            // updating canecletion field in property table - 0 failed
            int canceled = UpdateCancellation(property_id);
            if (canceled != 1)
            {
                //exiting function with error
                return 0;
            }
            command.CommandText = "spUpdateUserAfterCancelation";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            int numaffected;
            // Execute
            try
            {
                numaffected = command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                con.Close();
                return 0;
            }
            // Close Connection
            con.Close();
            return numaffected;
        }

        //update user income and total reservations after booking reservation
        public int updateUser(float user_total_income, int user_total_reservation, int user_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@total_income", user_total_income);
            command.Parameters.AddWithValue("@total_reservation", user_total_reservation);
            command.Parameters.AddWithValue("@user_id", user_id);

            command.CommandText = "spUpdateUser";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            int numaffected;
            // Execute
            try
            {
                numaffected = command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                con.Close();
                return 0;
            }
            // Close Connection
            con.Close();
            return numaffected;

        }

        //update property cancels field - only at property table
        public int UpdateCancellation(int property_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@property_id", property_id);

            command.CommandText = "spUpdateCancel";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            int numaffected;
            // Execute
            try
            {
                numaffected = command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                con.Close();
                return 0;
            }
            // Close Connection
            con.Close();
            return numaffected;
        }

        // Insert reservation when booking
        public int InsertReservation(Reservation resv)
        {
            SqlConnection con = Connect();
            // Create Command
            SqlCommand command = CreateInsertReservationCommand(con, resv);
            // Execute
            int numaffected = command.ExecuteNonQuery();
            // Close Connection
            con.Close();
            return numaffected;
        }

        //delete reservation - inserting 'f' in the is_deleted field, and null in the dates fields
        public int Delete_reservation(int reservation_number)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@reservation_number", reservation_number);
            command.CommandText = "spDeleteReservation";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            int numaffected;
            // Execute
            try
            {
                numaffected = command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                con.Close();
                return 0;
            }
            // Close Connection
            con.Close();
            return numaffected;
        }

        //get all reservations of a user - my orders
        public List<Reservation> readReservations(int user_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@userId", user_id);
            command.CommandText = "spReadReservations";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Reservation> reservations = new List<Reservation>();
            if (!dr.HasRows)
            {
                con.Close();
                return null;
            }
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string host_name = dr["host_name"].ToString();
                int price = Convert.ToInt32(dr["price"]);
                string location = dr["location"].ToString();
                int reserv_number = Convert.ToInt32(dr["reservation_number"]);
                int property_id = Convert.ToInt32(dr["property_id"]);
                int userid = Convert.ToInt32(dr["userId"]);

                DateTime check_in = Convert.ToDateTime(dr["check_in"].ToString());
                DateTime check_out = Convert.ToDateTime(dr["check_out"].ToString());
                string check_in_str = check_in.ToShortDateString();
                string check_out_str = check_out.ToShortDateString();

                reservations.Add(new Reservation(name, host_name, price, location, reserv_number, property_id, userid, check_in_str, check_out_str));

            }
            con.Close();
            return reservations;

        }

        //Insert new user to the datebase - register
        public User InsertUser(User user)
        {
            SqlConnection con = Connect();
            // Create Command
            SqlCommand command = CreateInsertUserCommand(con, user);
            // Execute
            int numaffected = command.ExecuteNonQuery();
            // Close Connection
            con.Close();

            if (numaffected != 1)
            {
                return null;
            }
            else
            {
                User u = GetUser(user.Email, user.Password);
                return u;
            }
        }

        // get user - login
        public User GetUser(string email, string password)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetUserCommand(con, email, password);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            dr.Read();
            if (!dr.HasRows)
            {
                con.Close();
                return null;
            }
            string password_ = dr["password"].ToString();
            int data_id = Convert.ToInt32(dr["id"]);
            string username = dr["username"].ToString();
            string email_ = dr["email"].ToString();
            string fullname = dr["fullname"].ToString();
            string sign_up_date = dr["sign_up_date"].ToString();
            int total_reservation = Convert.ToInt32(dr["total_reservation"]);
            int total_income = Convert.ToInt32(dr["total_income"]);
            int total_cancels = Convert.ToInt32(dr["total_cancels"]);

            User user = new User(data_id, email_, fullname, username, password_, sign_up_date, total_reservation, total_income, total_cancels);

            con.Close();
            return user;

        }

        //read the specific property - propertyPage
        public Property ReadProperty(int property_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadProperty", property_id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            dr.Read();

            int id = Convert.ToInt32(dr["id"]);
            string listing_url = dr["listing_url"].ToString();
            string name = dr["name"].ToString();
            string description = dr["description"].ToString();
            string neighborhood_overview = dr["neighborhood_overview"].ToString();
            string picture_url = dr["picture_url"].ToString();
            int host_id = Convert.ToInt32(dr["host_id"]);
            string host_url = dr["host_url"].ToString();
            string host_name = dr["host_name"].ToString();
            string host_since = dr["host_since"].ToString();
            string host_location = dr["host_location"].ToString();
            string host_about = dr["host_about"].ToString();
            string host_response_time = dr["host_response_time"].ToString();

            string host_response_rate = dr["host_response_rate"].ToString();
            string host_acceptance_rate = dr["host_acceptance_rate"].ToString();
            int host_is_superhost = Convert.ToInt32(dr["host_is_superhost"]);
            string host_picture_url = dr["host_picture_url"].ToString();
            string host_neighbourhood = dr["host_neighbourhood"].ToString();
            string neighbourhood = dr["neighbourhood"].ToString();
            string host_has_profile_pic = dr["host_has_profile_pic"].ToString();
            float latitude = Convert.ToSingle(dr["latitude"]);
            float longitude = Convert.ToSingle(dr["longitude"]);
            string property_type = dr["property_type"].ToString();
            string room_type = dr["room_type"].ToString();

            int accommodates = Convert.ToInt32(dr["accommodates"]);
            string bathrooms_text = dr["bathrooms_text"].ToString();
            int bedrooms = Convert.ToInt32(dr["bedrooms"]);
            int beds = Convert.ToInt32(dr["beds"]);
            string arr = dr["amenities"].ToString();
            JArray jarray = JArray.Parse(arr);
            List<string> amenities = new List<string>();
            foreach (string item in jarray)
            {
                amenities.Add(item.ToString());
            }

            float price = Convert.ToSingle(dr["price"]);
            int minimum_nights = Convert.ToInt32(dr["minimum_nights"]);
            int maximum_nights = Convert.ToInt32(dr["maximum_nights"]);
            float review_scores_rating = Convert.ToSingle(dr["review_scores_rating"]);
            float review_scores_cleanliness = Convert.ToSingle(dr["review_scores_cleanliness"]);
            float review_scores_checkin = Convert.ToSingle(dr["review_scores_checkin"]);
            float review_scores_communication = Convert.ToSingle(dr["review_scores_communication"]);
            float review_scores_location = Convert.ToSingle(dr["review_scores_location"]);
            string coords = dr["coords"].ToString();
            float center_distance = Convert.ToSingle(dr["center_distance"]);
            string location = dr["location"].ToString();

            con.Close();
            return new Property(id, listing_url, name, description, neighborhood_overview, picture_url, host_id, host_url, host_name, host_since, host_location, host_about, host_response_time, host_response_rate, host_acceptance_rate, host_is_superhost, host_picture_url, host_neighbourhood, host_has_profile_pic, neighbourhood, latitude, longitude, property_type, room_type, accommodates, bathrooms_text, bedrooms, beds, amenities, price, minimum_nights, maximum_nights, review_scores_rating, review_scores_cleanliness, review_scores_checkin, review_scores_communication, review_scores_location, coords, center_distance, location);
        }

        //read the reviews of a given property - propertyPage
        public List<Review> ReadReviews(int property_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadReviews", property_id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Review> reviews = new List<Review>();

            while (dr.Read())
            {
                int listing_id = Convert.ToInt32(dr["listing_id"]);
                long id = Convert.ToInt64(dr["id"]);
                string date = dr["date"].ToString();
                int reviewer_id = Convert.ToInt32(dr["reviewer_id"]);
                string reviewer_name = dr["reviewer_name"].ToString();
                string comments = dr["comments"].ToString();
                reviews.Add(new Review(listing_id, id, date, reviewer_id, reviewer_name, comments));
            }

            con.Close();
            return reviews;

        }

        //read top rating properties from the database
        public List<Property> ReadTopRating()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spTopRatingProperties");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Property> properties = new List<Property>();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string picture_url = dr["picture_url"].ToString();
                string location = dr["location"].ToString();
                int accommodates = Convert.ToInt32(dr["accommodates"]);
                int bedrooms = Convert.ToInt32(dr["bedrooms"]);
                float price = Convert.ToSingle(dr["price"]);
                float latitude = Convert.ToSingle(dr["latitude"]);
                float longitude = Convert.ToSingle(dr["longitude"]);
                string neighbourhood = dr["neighbourhood"].ToString();
                properties.Add(new Property(id, picture_url, location, accommodates, bedrooms, price, latitude, longitude, neighbourhood));
            }
            con.Close();
            return properties;
        }

        //read possible destinations from the database
        public List<string> ReadDestinations()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spReadDestinations");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<string> destinations = new List<string>();

            while (dr.Read())
            {
                string location = dr["location"].ToString();
                if (!destinations.Contains(location))
                    destinations.Add(location);
            }

            con.Close();
            return destinations;
        }

        //read rows from sql - related to lean/advanced search
        public List<Property> readFromSql(SqlDataReader dr, List<Property> properties)
        {
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string name = dr["name"].ToString();
                string property_type = dr["property_type"].ToString();
                string picture_url = dr["picture_url"].ToString();
                float center_distance = Convert.ToSingle(dr["center_distance"]);
                float price = Convert.ToSingle(dr["price"]);
                string location = dr["location"].ToString();
                float review_scores_rating = Convert.ToSingle(dr["review_scores_rating"]);
                int bedrooms = Convert.ToInt32(dr["bedrooms"]);
                int accommodates = Convert.ToInt32(dr["accommodates"]);
                string bathrooms_text = dr["bathrooms_text"].ToString();
                float latitude = Convert.ToSingle(dr["latitude"]);
                float longitude = Convert.ToSingle(dr["longitude"]);
                properties.Add(new Property(id, name, property_type, picture_url, center_distance, price, location, review_scores_rating, bedrooms, accommodates, bathrooms_text, latitude, longitude));
            }
            return properties;
        }

        //read all possible search results - advanced
        public List<Property> ReadSearchResults(PropertySearch prop)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommand(con, "spSearchResults", prop);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Property> properties = new List<Property>();
            properties = readFromSql(dr, properties);
            con.Close();
            return properties;
        }

        //read all possible search results from lean search
        public List<Property> ReadLeanSearchResults(PropertySearch prop)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateReadCommandForLeanSearch(con, "spLeanSearchResults", prop);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Property> properties = new List<Property>();
            properties = readFromSql(dr, properties);
            con.Close();
            return properties;
        }

        //sql command - advanced search property
        private SqlCommand CreateReadCommand(SqlConnection con, string text, PropertySearch prop)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@bedrooms", prop.Bedrooms);
            command.Parameters.AddWithValue("@max_price", prop.Max_price);
            command.Parameters.AddWithValue("@min_price", prop.Min_price);
            command.Parameters.AddWithValue("@center_distance", prop.Center_distance);
            command.Parameters.AddWithValue("@location", prop.Location);
            command.Parameters.AddWithValue("@review_scores_rating", prop.Review_scores_rating);
            command.Parameters.AddWithValue("@check_in", prop.Check_in);
            command.Parameters.AddWithValue("@check_out", prop.Check_out);
            command.CommandText = text;
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        //sql command - lean search property
        private SqlCommand CreateReadCommandForLeanSearch(SqlConnection con, string text, PropertySearch prop)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@location", prop.Location);
            command.Parameters.AddWithValue("@check_in", prop.Check_in);
            command.Parameters.AddWithValue("@check_out", prop.Check_out);
            command.CommandText = text;
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        //sql command read property - propertyPage, readReviews
        private SqlCommand CreateReadCommand(SqlConnection con, string text, int property_id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@property_id", property_id);
            command.CommandText = text;
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        //sql command - read hosts, read users, read destinations, read properties
        private SqlCommand CreateReadCommand(SqlConnection con, string text)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = text;
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // read command for User - login
        private SqlCommand CreateGetUserCommand(SqlConnection con, string email, string password)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.CommandText = "spGetUser";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // insert command for User
        private SqlCommand CreateInsertUserCommand(SqlConnection con, User user)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@username", user.UserName);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@fullname", user.FullName);
            //date needs to be in sql format
            string date = DateTime.Now.ToShortDateString();
            string[] arr = date.Split('/');
            date = arr[2] + "-" + arr[1] + "-" + arr[0];
            command.Parameters.AddWithValue("@sign_up_date", date);
            command.CommandText = "spInsertUser";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // insert command for reservation
        private SqlCommand CreateInsertReservationCommand(SqlConnection con, Reservation resv)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@property_id", resv.Property_id);
            command.Parameters.AddWithValue("@user_id", resv.User_id);
            command.Parameters.AddWithValue("@check_in", resv.Check_in);
            command.Parameters.AddWithValue("@check_out", resv.Check_out);
            command.Parameters.AddWithValue("@total_amount", resv.Total_amount);
            command.CommandText = "spInsertReservation";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // connect 
        private SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;
        }

    }
}