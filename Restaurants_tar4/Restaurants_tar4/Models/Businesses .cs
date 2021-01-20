using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tar1.Models.DAL;

namespace tar1.Models
{
    public class Businesses
    {
        string image;
        int id;
        string name;
        float reating;
        string category;
        int priceRange;
        string phone;
        string address;
        int cuisineId;
        string url;
        List<string> highlights;

        public Businesses() { }

        public Businesses(string image, int id, string name, float reating, string category, int priceRange, string phone, string address, int cuisineId, string url, List<string> highlights)
        {
            Image = image;
            Id = id;
            Name = name;
            Reating = reating;
            Category = category;
            PriceRange = priceRange;
            Phone = phone;
            Address = address;
            CuisineId = cuisineId;
            Url = url;
            Highlights = highlights;
        }

        public string Image { get => image; set => image = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Reating { get => reating; set => reating = value; }
        public string Category { get => category; set => category = value; }
        public int PriceRange { get => priceRange; set => priceRange = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public int CuisineId { get => cuisineId; set => cuisineId = value; }
        public string Url { get => url; set => url = value; }
        public List<string> Highlights { get => highlights; set => highlights = value; }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.Insert(this);
           
        }

        public void InsertHighlight()
        {
            DBServices dbs = new DBServices();
            dbs.InsertHighlight(this);
        }

        public List<Businesses> ReadByCamp(int cusineId)
        {
            DBServices dbs = new DBServices();
            List<Businesses> restc = dbs.getRestCampaigns(cusineId);
            if (restc.Count == 0)
            {
                restc = dbs.getRestCampaigns1();
            }
            return restc;
        }

        public List<Businesses> ReadAll()
        {
            DBServices dbs = new DBServices();
            List<Businesses> b = dbs.getAllBusinesses();
            return b;
        }

        public List<Businesses> Read(int cusineId, int pr)
        {
            DBServices dbs = new DBServices();
            List<Businesses> b = dbs.getBusinesses(cusineId, pr);
            return b;
        }
    }
}