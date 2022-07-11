using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class Review
    {
        int listing_id;
        long id;
        string date;
        int reviewer_id;
        string reviewer_name;
        string comments;

        public Review() { }

        public Review(int listing_id, long id, string date, int reviewer_id, string reviewer_name, string comments)
        {
            this.Listing_id = listing_id;
            this.Id = id;
            this.Date = date;
            this.Reviewer_id = reviewer_id;
            this.Reviewer_name = reviewer_name;
            this.Comments = comments;
        }

        public int Listing_id { get => listing_id; set => listing_id = value; }
        public long Id { get => id; set => id = value; }
        public string Date { get => date; set => date = value; }
        public int Reviewer_id { get => reviewer_id; set => reviewer_id = value; }
        public string Reviewer_name { get => reviewer_name; set => reviewer_name = value; }
        public string Comments { get => comments; set => comments = value; }

        public List<Review> ReadReviews(int property_id)
        {
            DataServices ds = new DataServices();
            return ds.ReadReviews(property_id);
        }
    }
}