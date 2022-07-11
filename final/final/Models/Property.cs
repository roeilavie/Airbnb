using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class Property
    {
        string listing_url;
        string description;
        string neighborhood_overview;
        int host_id;
        string host_url;

        int id;
        float price;
        string property_type;
        string picture_url;
        string name;
        string host_name;
        int total_cancels;
        int total_rent_days;

        string host_since;
        string host_location;
        string host_about;
        string host_response_time;
        string host_response_rate;
        string host_acceptance_rate;
        int host_is_superhost;
        string host_picture_url;
        string host_neighbourhood;
        string host_has_profile_pic;
        string neighbourhood;

        float latitude;
        float longitude;
        string room_type;
        int accommodates;
        string bathrooms_text;
        int bedrooms;
        int beds;
        List<string> amenities;
        int minimum_nights;
        int maximum_nights;

        float review_scores_rating;
        float review_scores_cleanliness;
        float review_scores_checkin;
        float review_scores_communication;
        float review_scores_location;

        string coords;
        float center_distance;
        string location;


        public Property(int id, string name, string property_type, string picture_url, float center_distance, float price, string location, float review_scores_rating, int bedrooms, int accommodates, string bathrooms_text)
        {
            this.id = id;
            this.name = name;
            this.property_type = property_type;
            this.picture_url = picture_url;
            this.center_distance = center_distance;
            this.price = price;
            this.location = location;
            this.review_scores_rating = review_scores_rating;
            this.bedrooms = bedrooms;
            this.accommodates = accommodates;
            this.bathrooms_text = bathrooms_text;
        }
        public Property(int id, string name, string property_type, string picture_url, float center_distance, float price, string location, float review_scores_rating, int bedrooms, int accommodates, string bathrooms_text, float latitude, float longitude)
        {
            this.id = id;
            this.name = name;
            this.property_type = property_type;
            this.picture_url = picture_url;
            this.center_distance = center_distance;
            this.price = price;
            this.location = location;
            this.review_scores_rating = review_scores_rating;
            this.bedrooms = bedrooms;
            this.accommodates = accommodates;
            this.bathrooms_text = bathrooms_text;
            this.latitude = latitude;
            this.longitude = longitude;
        }
        public Property() { }
        public Property(int id, string picture_url, string location,int accommodates, int bedrooms, float price)
        {
            this.id = id;
            this.picture_url = picture_url;
            this.location = location;
            this.accommodates = accommodates;
            this.bedrooms = bedrooms;
            this.price = price;
        }
        public Property(int id, string picture_url, string location, int accommodates, int bedrooms, float price, float latitude, float longitude, string neighbourhood)
        {
            this.id = id;
            this.picture_url = picture_url;
            this.location = location;
            this.accommodates = accommodates;
            this.bedrooms = bedrooms;
            this.price = price;
            this.latitude = latitude;
            this.longitude = longitude;
            this.neighbourhood = neighbourhood;

        }
        public Property(int id, string listing_url, string name, string description, string neighborhood_overview, string picture_url, int host_id, string host_url, string host_name, string host_since, string host_location, string host_about, string host_response_time, string host_response_rate, string host_acceptance_rate, int host_is_superhost, string host_picture_url, string host_neighbourhood, string host_has_profile_pic, string neighbourhood, float latitude, float longitude, string property_type, string room_type, int accommodates, string bathrooms_text, int bedrooms, int beds, List<string> amenities, float price, int minimum_nights, int maximum_nights, float review_scores_rating, float review_scores_cleanliness, float review_scores_checkin, float review_scores_communication, float review_scores_location, string coords, float center_distance, string location)
        {
            Id = id;
            Listing_url = listing_url;
            Name = name;
            Description = description;
            Neighborhood_overview = neighborhood_overview;
            Picture_url = picture_url;
            Host_id = host_id;
            Host_url = host_url;
            Host_name = host_name;
            Host_since = host_since;
            Host_location = host_location;
            Host_about = host_about;
            Host_response_time = host_response_time;
            Host_response_rate = host_response_rate;
            Host_acceptance_rate = host_acceptance_rate;
            Host_is_superhost = host_is_superhost;
            Host_picture_url = host_picture_url;
            Host_neighbourhood = host_neighbourhood;
            Host_has_profile_pic = host_has_profile_pic;
            Neighbourhood = neighbourhood;
            Latitude = latitude;
            Longitude = longitude;
            Property_type = property_type;
            Room_type = room_type;
            Accommodates = accommodates;
            Bathrooms_text = bathrooms_text;
            Bedrooms = bedrooms;
            Beds = beds;
            Amenities = amenities;
            Price = price;
            Minimum_nights = minimum_nights;
            Maximum_nights = maximum_nights;
            Review_scores_rating = review_scores_rating;
            Review_scores_cleanliness = review_scores_cleanliness;
            Review_scores_checkin = review_scores_checkin;
            Review_scores_communication = review_scores_communication;
            Review_scores_location = review_scores_location;
            Coords = coords;
            Center_distance = center_distance;
            Location = location;
        }
        public Property(int id, float price, string property_type, string picture_url, string name, string host_name, int total_cancels, int total_rent_days)
        {
            this.id = id;
            this.price = price;
            this.property_type = property_type;
            this.picture_url = picture_url;
            this.name = name;
            this.host_name = host_name;
            this.total_cancels = total_cancels;
            this.total_rent_days = total_rent_days;
        }

        public int Id { get => id; set => id = value; }
        public string Listing_url { get => listing_url; set => listing_url = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Neighborhood_overview { get => neighborhood_overview; set => neighborhood_overview = value; }
        public string Picture_url { get => picture_url; set => picture_url = value; }
        public int Host_id { get => host_id; set => host_id = value; }
        public string Host_url { get => host_url; set => host_url = value; }
        public string Host_name { get => host_name; set => host_name = value; }
        public string Host_since { get => host_since; set => host_since = value; }
        public string Host_location { get => host_location; set => host_location = value; }
        public string Host_about { get => host_about; set => host_about = value; }
        public string Host_response_time { get => host_response_time; set => host_response_time = value; }
        public string Host_response_rate { get => host_response_rate; set => host_response_rate = value; }
        public string Host_acceptance_rate { get => host_acceptance_rate; set => host_acceptance_rate = value; }
        public int Host_is_superhost { get => host_is_superhost; set => host_is_superhost = value; }
        public string Host_picture_url { get => host_picture_url; set => host_picture_url = value; }
        public string Host_neighbourhood { get => host_neighbourhood; set => host_neighbourhood = value; }
        public string Host_has_profile_pic { get => host_has_profile_pic; set => host_has_profile_pic = value; }
        public string Neighbourhood { get => neighbourhood; set => neighbourhood = value; }
        public float Latitude { get => latitude; set => latitude = value; }
        public float Longitude { get => longitude; set => longitude = value; }
        public string Property_type { get => property_type; set => property_type = value; }
        public string Room_type { get => room_type; set => room_type = value; }
        public int Accommodates { get => accommodates; set => accommodates = value; }
        public string Bathrooms_text { get => bathrooms_text; set => bathrooms_text = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public int Beds { get => beds; set => beds = value; }
        public List<string> Amenities { get => amenities; set => amenities = value; }
        public float Price { get => price; set => price = value; }
        public int Minimum_nights { get => minimum_nights; set => minimum_nights = value; }
        public int Maximum_nights { get => maximum_nights; set => maximum_nights = value; }
        public float Review_scores_rating { get => review_scores_rating; set => review_scores_rating = value; }
        public float Review_scores_cleanliness { get => review_scores_cleanliness; set => review_scores_cleanliness = value; }
        public float Review_scores_checkin { get => review_scores_checkin; set => review_scores_checkin = value; }
        public float Review_scores_communication { get => review_scores_communication; set => review_scores_communication = value; }
        public float Review_scores_location { get => review_scores_location; set => review_scores_location = value; }
        public string Coords { get => coords; set => coords = value; }
        public float Center_distance { get => center_distance; set => center_distance = value; }
        public string Location { get => location; set => location = value; }
        public int Total_cancels { get => total_cancels; set => total_cancels = value; }
        public int Total_rent_days { get => total_rent_days; set => total_rent_days = value; }

        //read top rating properties
        public List<Property> ReadTopRating()
        {
            DataServices ds = new DataServices();
            return ds.ReadTopRating();
        }

        //read the possible destinations
        public List<string> ReadDestinations()
        {
            DataServices ds = new DataServices();
            return ds.ReadDestinations();
        }

        public Property ReadProperty(int property_id)
        {
            DataServices ds = new DataServices();
            return ds.ReadProperty(property_id);
        }

        public List<Property> ReadProperties()
        {
            DataServices ds = new DataServices();
            return ds.ReadProperties();
        }
    }
}