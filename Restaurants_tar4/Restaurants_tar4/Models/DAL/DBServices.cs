using Restaurants_tar4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using tar2.Models;

namespace tar1.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {
        }

        public List<Businesses> getBusinesses(int cusineId, int pr)
        {
            SqlConnection con = null;
            List<Businesses> rList = new List<Businesses>();
            try
            {
                String selectSTR = "";
                con = connect("DBConnectionString");
                if (pr == 0)
                {
                    selectSTR = "select * from [RestaurantsB_2021] where cusiId=" + cusineId+ " ORDER BY [reating] DESC";
                }
                else
                {
                    selectSTR = "select * from [RestaurantsB_2021] where cusiId=" + cusineId + " and priceRange=" + pr + " ORDER BY [reating] DESC";
                }

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
                while (dr.Read())
                { 
                    Businesses restaurant = new Businesses();
                    restaurant.Id = Convert.ToInt32(dr["id"]);
                    restaurant.Image = (string)dr["image"];
                    restaurant.Name = (string)dr["name"];
                    restaurant.Reating = (float)Convert.ToDouble(dr["reating"]);
                    restaurant.Category = (string)dr["category"];
                    restaurant.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    restaurant.Phone = (string)dr["phone"];
                    restaurant.Address = (string)dr["address"];
                    restaurant.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    restaurant.Url= (string)dr["url"];
                    restaurant.Highlights = getRestHighlights(restaurant.Id);
                    rList.Add(restaurant);
                }
                return rList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                { 
                con.Close();
                }
            }
        }

        public List<Businesses> getAllBusinesses()
        {
            SqlConnection con = null;
            List<Businesses> rList = new List<Businesses>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from[RestaurantsB_2021] order by name";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Businesses restaurant = new Businesses();
                    restaurant.Id = Convert.ToInt32(dr["id"]);
                    restaurant.Image = (string)dr["image"];
                    restaurant.Name = (string)dr["name"];
                    restaurant.Reating = (float)Convert.ToDouble(dr["reating"]);
                    restaurant.Category = (string)dr["category"];
                    restaurant.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    restaurant.Phone = (string)dr["phone"];
                    restaurant.Address = (string)dr["address"];
                    restaurant.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    restaurant.Highlights = getRestHighlights(restaurant.Id);
                    rList.Add(restaurant);
                }
                return rList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<string> getRestHighlights(int id) // get restaurant's highlights
        {
            SqlConnection con = null;
            List<string> rhList = new List<string>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from [HighlightInRestB_2021] where resId = " + id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string h = (string)dr["highlight"];
                    rhList.Add(h);
                }
                return rhList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public Customer checkCustomer(string email, string pass)
        {
            SqlConnection con = null;
            Customer c = new Customer();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM CustomersB_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    if (email == (string)dr["email"] && pass == (string)dr["password"])
                    {
                        c.Id = Convert.ToInt32(dr["id"]);
                        c.Name = (string)dr["name"];
                        c.Lastname = (string)dr["fname"];
                        c.Email = (string)dr["email"];
                        c.Phone = (string)dr["phone"];
                        c.Password = (string)dr["password"];
                        c.Img = (string)dr["image"];
                        c.PriceRange = Convert.ToInt32(dr["price_range"]);
                        c.Chlist =getCustomerHighlights(c.Id);
                        return c;
                    }  
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public bool checkEmailCustomer(string email)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM CustomersB_2021 where [email]='"+ email+"'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<string> getCustomerHighlights(int id)
        {
            SqlConnection con = null;
            List<string> hList = new List<string>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select * from [CustHighlightsB_2021] where custID = " + id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string h = (string)dr["highlight"];
                    hList.Add(h);
                }
                return hList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = CommandSTR; // can be Select, Insert, Update, Delete
            /*cmd.CommandTimeout = 10;*/ // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }

        public int InsertCustomer(Customer cust)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            string cStr = BuildInsertCommand(cust);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand(Customer cust)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", cust.Name, cust.Lastname, cust.Email, cust.Phone, cust.Password,cust.Img, cust.PriceRange);

            String prefix = "INSERT INTO [CustomersB_2021]" + "([name], [fname], [email], [phone], [password],[image],[price_range])";
            command = prefix + sb.ToString();
            return command;
        }

        public int GetLastCustomerID()
        {
            SqlConnection con = null;
            int commandLastID;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT TOP 1 * FROM [dbo].[CustomersB_2021] ORDER BY [id] DESC";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Read(); // read single row
                commandLastID = Convert.ToInt32(dr["id"]);
                return commandLastID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

    public int CreateCampaign(Campaign camp)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            string cStr = BuildInsertCommand1(camp); // helper method to build the insert string
            cmd = CreateCommand(cStr, con); // create the command
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand1(Campaign camp)
        {
            String command;
            String prefix = "";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", camp.ResID, camp.ResName.Replace("'", "''"), camp.Budget, camp.Clicks, camp.Views, 1);

                prefix = "DELETE FROM [CampaignsB_2021] WHERE resid =" + camp.ResID +
                "INSERT INTO [CampaignsB_2021]" + "([resid],[resName],[budget],[clicks],[views],[status])";

            command = prefix + sb.ToString();
            return command;
        }

        public int InsertCustomerHighlights(Customer cust)
        {
            SqlConnection con;
            SqlCommand cmd;
            
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildInsertCommand1(cust);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand1(Customer cust)
        {
            String command = "";
            foreach (var ch in cust.Chlist)
            { 
                    String prefix = "INSERT INTO [CustHighlightsB_2021] ([custID], [highlight]) values(IDENT_CURRENT ('CustomersB_2021'),'" + ch + "') ";
                    command += prefix;
            }
            return command;
        }


        public int UpdateCustomerHighlights(Customer cust)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildInsertCommand2(cust);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand2(Customer cust)
        {
            String command = "";
            String prefix = "DELETE FROM [CustHighlightsB_2021] WHERE custID = " + cust.Id +
                " update [CustomersB_2021] set price_range="+cust.PriceRange+" where id=" + cust.Id;
            foreach (var ch in cust.Chlist)
            {
                
                prefix += "INSERT INTO [CustHighlightsB_2021] ([custID], [highlight]) values("+cust.Id+",'" + ch + "') ";
                command += prefix;
            }
            return command;
        }


        public int DeleteCustomerHighlights(Customer cust)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildDeleteCustomerHighloghtsCommand(cust);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildDeleteCustomerHighloghtsCommand(Customer cust)
        {
            String command = "DELETE FROM [CustHighlightsB_2021] WHERE custID = " + cust.Id +
                " update [CustomersB_2021] set price_range=" + cust.PriceRange + " where id=" + cust.Id;
            return command;
        }

        public int DeleteCamp(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildDeleteCommand(id);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildDeleteCommand(int id)
        {
            String prefix = "UPDATE [CampaignsB_2021] SET [status]=0 WHERE resid =" + id;
            return prefix;
        }

        public int UpdateViewCamp(string mode, int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildUpdateCommand(mode,id);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private string BuildUpdateCommand(string mode, int id)
        {
            String prefix = "";
            if (mode == "view")
            {
                prefix = "UPDATE [CampaignsB_2021] SET [views]=[views]+1 WHERE resid =" + id;
                return prefix;
            }
            else
            {
                prefix = "UPDATE [CampaignsB_2021] SET [clicks]=[clicks]+1 WHERE resid =" + id;
                return prefix;
            }

        }

        public List<Campaign> getCampaigns()
        {
            SqlConnection con = null;
            List<Campaign> campList = new List<Campaign>();
            try
            {

                con = connect("DBConnectionString");

                string selectSTR = "select * from [CampaignsB_2021] where [status]=1";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Campaign camp = new Campaign();
                    camp.ResID = Convert.ToInt32(dr["resid"]);
                    camp.ResName = (string)dr["resName"];
                    camp.Budget = (float)Convert.ToDouble(dr["budget"]);
                    camp.Clicks = Convert.ToInt32(dr["clicks"]);
                    camp.Views = Convert.ToInt32(dr["views"]);
                    camp.Balance = camp.Budget - (float)(camp.Clicks * 0.5 + camp.Views * 0.1);
                    camp.Status = (camp.Balance < 0.5) ? DeleteCamp(camp.ResID) : Convert.ToInt32(dr["status"]);
                    campList.Add(camp);
                }
                return campList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Businesses> getRestCampaigns(int cusineId)
        {
            SqlConnection con = null;
            List<Businesses> rList = new List<Businesses>();
            try
            {
                con = connect("DBConnectionString");
                string selectSTR = "";

                // get top 3 specific cusine's campaigns with the highest budgets
                 selectSTR = "select TOP 3 [id],[image],[name],[reating],[category],[priceRange],[phone],[address],[cusiId],[url]" +
                " from [dbo].[RestaurantsB_2021] r inner join [dbo].[CampaignsB_2021] c" +
                " on c.resid=r.id where r.cusiId=" + cusineId +"and c.status=1 ORDER BY c.budget DESC";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Businesses camp = new Businesses();
                    camp.Id = Convert.ToInt32(dr["id"]);
                    camp.Image = (string)dr["image"];
                    camp.Name = (string)dr["name"];
                    camp.Reating = (float)Convert.ToDouble(dr["reating"]);
                    camp.Category = (string)dr["category"];
                    camp.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    camp.Phone = (string)dr["phone"];
                    camp.Address = (string)dr["address"];
                    camp.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    camp.Url = (string)dr["url"];
                    camp.Highlights = getRestHighlights(camp.Id);
                    rList.Add(camp);
                }
                return rList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Businesses> getRestCampaigns1()
        {
            SqlConnection con = null;
            List<Businesses> rList = new List<Businesses>();
            try
            {
                con = connect("DBConnectionString");
                string selectSTR = "";

                selectSTR = "select TOP 3 [id],[image],[name],[reating],[category],[priceRange],[phone],[address],[cusiId],[url]" +
               " from [dbo].[RestaurantsB_2021] r inner join [dbo].[CampaignsB_2021] c" +
               " on c.resid=r.id where c.status=1 ORDER BY c.budget DESC";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        Businesses camp = new Businesses();
                        camp.Id = Convert.ToInt32(dr["id"]);
                        camp.Image = (string)dr["image"];
                        camp.Name = (string)dr["name"];
                        camp.Reating = (float)Convert.ToDouble(dr["reating"]);
                        camp.Category = (string)dr["category"];
                        camp.PriceRange = Convert.ToInt32(dr["priceRange"]);
                        camp.Phone = (string)dr["phone"];
                        camp.Address = (string)dr["address"];
                        camp.CuisineId = Convert.ToInt32(dr["cusiId"]);
                        camp.Url = (string)dr["url"];
                        camp.Highlights = getRestHighlights(camp.Id);
                        rList.Add(camp);
                    }
                    return rList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int UpdateCampaign(Campaign camp)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string cStr = BuildInsertCommand3(camp);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private string BuildInsertCommand3(Campaign camp)
        {
            String prefix = "UPDATE [CampaignsB_2021] SET budget ="+ camp.Budget + "WHERE resid =" + camp.ResID;
            return prefix;
        }
    }

}

