using final.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final.Models
{
    public class Host
    {
        int host_id;
        string host_since;
        string host_name;
        string host_neighbourhood;
        string host_picture_url;
        float total_income;
        int total_cancel;
        int num_of_properties;

        public Host() { }

        public Host(int host_id, string host_since, string host_name, string host_neighbourhood, string host_picture_url, float total_income, int total_cancel, int num_of_properties)
        {
            this.host_id = host_id;
            this.host_since = host_since;
            this.host_name = host_name;
            this.host_neighbourhood = host_neighbourhood;
            this.host_picture_url = host_picture_url;
            this.total_income = total_income;
            this.total_cancel = total_cancel;
            this.num_of_properties = num_of_properties;
        }

        public int Host_id { get => host_id; set => host_id = value; }
        public string Host_since { get => host_since; set => host_since = value; }
        public string Host_name { get => host_name; set => host_name = value; }
        public string Host_neighbourhood { get => host_neighbourhood; set => host_neighbourhood = value; }
        public string Host_picture_url { get => host_picture_url; set => host_picture_url = value; }
        public float Total_income { get => total_income; set => total_income = value; }
        public int Total_cancel { get => total_cancel; set => total_cancel = value; }
        public int Num_of_properties { get => num_of_properties; set => num_of_properties = value; }

        public List<Host> ReadHosts()
        {
            DataServices ds = new DataServices();
            List<Host> hosts = ds.ReadHosts();
            return hosts;
        }
    }
}