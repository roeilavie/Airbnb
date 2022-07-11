using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class PropertySearch
    {
        int bedrooms;
        string check_in;
        string check_out;
        float center_distance;
        float max_price;
        float min_price;
        string location;
        float review_scores_rating;
        public PropertySearch() { }

        public PropertySearch(int bedrooms, string check_in, string check_out, float center_distance, float max_price, float min_price, string location, float review_scores_rating)
        {
            this.bedrooms = bedrooms;
            this.check_in = check_in;
            this.check_out = check_out;
            this.center_distance = center_distance;
            this.max_price = max_price;
            this.min_price = min_price;
            this.location = location;
            this.review_scores_rating = review_scores_rating;
        }

        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public string Check_in { get => check_in; set => check_in = value; }
        public string Check_out { get => check_out; set => check_out = value; }
        public float Center_distance { get => center_distance; set => center_distance = value; }
        public float Max_price { get => max_price; set => max_price = value; }
        public float Min_price { get => min_price; set => min_price = value; }
        public string Location { get => location; set => location = value; }
        public float Review_scores_rating { get => review_scores_rating; set => review_scores_rating = value; }

        public List<Property> Read()
        {
            DataServices ds = new DataServices();
            return ds.ReadSearchResults(this);
        }

        public List<Property> ReadProp()
        {
            DataServices ds = new DataServices();
            return ds.ReadLeanSearchResults(this);
        }
    }
}