using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tar1.Models.DAL;

namespace Restaurants_tar4.Models
{
    public class Campaign
    {
        int resID;
        string resName;
        float budget;
        int clicks;
        int views;
        int status;
        float balance;

        public Campaign(int resID, string resName, float budget, int clicks, int views, int status, float balance)
        {
            ResID = resID;
            ResName = resName;
            Budget = budget;
            Clicks = clicks;
            Views = views;
            Status = status;
            Balance = balance;
        }

        public int ResID { get => resID; set => resID = value; }
        public string ResName { get => resName; set => resName = value; }
        public float Budget { get => budget; set => budget = value; }
        public int Clicks { get => clicks; set => clicks = value; }
        public int Views { get => views; set => views = value; }
        public int Status { get => status; set => status = value; }
        public float Balance { get => balance; set => balance = value; }

        public Campaign()
        {

        }

        public List<Campaign> Read()
        {
            DBServices dbs = new DBServices();
            List<Campaign> c = dbs.getCampaigns();
            return c;
        }

        public List<Campaign> Update()
        {
            GetNewBalance(this);
            DBServices dbs = new DBServices();
            dbs.UpdateCampaign(this);
            return dbs.getCampaigns();
        }
        public List<Campaign> Insert()
        {
            DBServices dbs = new DBServices();
            dbs.CreateCampaign(this);
            return dbs.getCampaigns();
        }
        public void GetNewBalance(Campaign camp) {
            camp.Balance = camp.Budget - (float)(camp.Clicks * 0.5 + camp.Views * 0.1);       
        }
        public List<Campaign> DeleteCamp(int id)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteCamp(id);
            return dbs.getCampaigns();
        }
    }
}