using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tar1.Models.DAL;

namespace tar2.Models
{
    public class Customer
    {
        int id;
        string name;
        string lastname;
        string email;
        string phone;
        string password;
        string img;
        int priceRange;
        List<string> chlist;

  

        public Customer() { }

        public Customer(int id,string name, string lastname, string email, string phone, string password, string img, int priceRange, List<string> chlist)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Email = email;
            Phone = phone;
            Password = password;
            Img = img;
            PriceRange = priceRange;
            Chlist = chlist;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
        public string Img { get => img; set => img = value; }
        public int PriceRange { get => priceRange; set => priceRange = value; }
        public List<string> Chlist { get => chlist; set => chlist = value; }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertCustomer(this);
            dbs.InsertCustomerHighlights(this);
        }

        public Customer Read(string email, string pass) {
            DBServices dbs = new DBServices();
            return dbs.checkCustomer(email, pass);
        }

        public void Update()
        {
            DBServices dbs = new DBServices();          
            dbs.UpdateCustomerHighlights(this);
        }
    }
}