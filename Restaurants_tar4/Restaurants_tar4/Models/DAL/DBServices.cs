using Restaurants_tar4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using tar2.Models;

namespace tar1.Models.DAL
{
    public class DBServices
    {
        //static List<Businesses> favourites;
        public SqlDataAdapter da;
        public DataTable dt;

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
                //else if (pr == 0 && hList != null)
                //{
                //    String selectSTR = "";
                //    foreach (var h in hList)
                //    {
                //        selectSTR += "select * from [RestaurantsB_2021] inner join " +
                //       "[HighlightInRestB_2021] on[RestaurantsB_2021].id =[HighlightInRestB_2021].resId " +
                //       "where[HighlightInRestB_2021].highlight ="+ h +"and[RestaurantsB_2021].cusiId =" + cusineId + " ";
                //    }
                   
                //}
                // String selectSTR = "SELECT * FROM RestaurantsB_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
                while (dr.Read())
                { 
                    Businesses favourite = new Businesses();
                    favourite.Id = Convert.ToInt32(dr["id"]);
                    favourite.Image = (string)dr["image"];
                    favourite.Name = (string)dr["name"];
                    favourite.Reating = (float)Convert.ToDouble(dr["reating"]);
                    favourite.Category = (string)dr["category"];
                    favourite.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    favourite.Phone = (string)dr["phone"];
                    favourite.Address = (string)dr["address"];
                    favourite.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    favourite.Url= (string)dr["url"];
                    int id = Convert.ToInt32(dr["id"]);
                    favourite.Highlights = getRestHighlights(id);
                    rList.Add(favourite);
                }
                return rList;
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
                    Businesses favourite = new Businesses();
                    favourite.Id = Convert.ToInt32(dr["id"]);
                    favourite.Image = (string)dr["image"];
                    favourite.Name = (string)dr["name"];
                    favourite.Reating = (float)Convert.ToDouble(dr["reating"]);
                    favourite.Category = (string)dr["category"];
                    favourite.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    favourite.Phone = (string)dr["phone"];
                    favourite.Address = (string)dr["address"];
                    favourite.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    int id = Convert.ToInt32(dr["id"]);
                    favourite.Highlights = getRestHighlights(id);
                    rList.Add(favourite);
                }
                return rList;
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
                    con.Close();
                }
            }
        }

        public List<string> getRestHighlights(int id)
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
                // write to log
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
                        c.PriceRange = Convert.ToInt32(dr["price_range"]);
                        c.Chlist =getCustomerHighlights(c.Id);
                        return c;
                    }
                }
                return null;
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
                // write to log
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



        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        public int Insert(Businesses favourite)
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
            string cStr = BuildInsertCommand(favourite); // helper method to build the insert string
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

        private string BuildInsertCommand(Businesses favourite)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            //sb.AppendFormat("Values('{0}', '{1}')", student.Name, student.Age);
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}')", favourite.Id, favourite.Image, favourite.Name.Replace("'","''"), favourite.Reating, favourite.Category, favourite.PriceRange, favourite.Phone, favourite.Address,favourite.CuisineId, favourite.Url);
            //sb.AppendFormat("Values('{0}', '{1}')", student.Name, student.Age);

            String prefix = "INSERT INTO [RestaurantsB_2021]" + "([id], [image], [name], [reating], [category], [priceRange], [phone], [address],[cusiId],[url])";
            command = prefix + sb.ToString();
            return command;
        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
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
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            string cStr = BuildInsertCommand(cust); // helper method to build the insert string
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

        private string BuildInsertCommand(Customer cust)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", cust.Name, cust.Lastname, cust.Email, cust.Phone, cust.Password,cust.Img, cust.PriceRange);

            String prefix = "INSERT INTO [CustomersB_2021]" + "([name], [fname], [email], [phone], [password],[image],[price_range])";
            command = prefix + sb.ToString();
            return command;
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
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", camp.ResID, camp.ResName, camp.Budget, camp.Clicks, camp.Views, camp.Status);

            String prefix = "INSERT INTO [CampaignsB_2021]" + "([resid],[resName],[budget],[clicks],[views],[status])";
            command = prefix + sb.ToString();
            return command;
        }

        public int InsertCustomerHighlights(Customer cust)
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
            string cStr = BuildInsertCommand1(cust); // helper method to build the insert string
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

        private string BuildInsertCommand1(Customer cust)
        {
            String command = "";
            //StringBuilder sb = new StringBuilder();
            //// use a string builder to create the dynamic string

            //sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6})", cust.Name, cust.Lastname, cust.Email, cust.Phone, cust.Password, cust.Img, cust.PriceRange);

            //String prefix = "INSERT INTO [CustomersB_2021]" + "([name], [fname], [email], [phone], [password],[image],[price_range])";
            //command = prefix + sb.ToString();

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
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            string cStr = BuildInsertCommand2(cust); // helper method to build the insert string
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
        public int DeleteCamp(int id)
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
            string cStr = BuildDeleteCommand(id); // helper method to build the insert string
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
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            string cStr = BuildUpdateCommand(mode,id); // helper method to build the insert string
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

        public int InsertHighlight(Businesses highlight)
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
            string cStr = BuildInsertCommand1(highlight); // helper method to build the insert string
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

        private string BuildInsertCommand1(Businesses highlight)
        {
            String command="";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            //sb.AppendFormat("Values('{0}','{1}')", cust.Name, cust.Lastname, cust.Email, cust.Phone, cust.Password, cust.Img);

            //String prefix = "INSERT INTO [CustomersB_2021]" + "([name], [fname], [email], [phone], [password],[image])";
            //command = prefix + sb.ToString();
            string [] hArr = {"Wifi", "Serves Alcohol", "Breakfast", "Dinner", "Lunch", "Vegetarian Friendly", "Takeaway Available", "Credit Card", "Delivery", "Cash" };
            
            foreach (var h in highlight.Highlights)
            {
                if (hArr.Contains(h))
                {
                    String prefix = "INSERT INTO HighlightInRestB_2021" + "([resId], [highlight]) values(" + highlight.Id + ", '" + h + "') ";
                    command += prefix;
                }
                
            }
            return command;
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
                // write to log
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

                 selectSTR = "select TOP 3 [id],[image],[name],[reating],[category],[priceRange],[phone],[address],[cusiId],[url]" +
                " from [dbo].[RestaurantsB_2021] r inner join [dbo].[CampaignsB_2021] c" +
                " on c.resid=r.id where r.cusiId="+cusineId +"and c.status=1 ORDER BY c.budget DESC";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Businesses favourite = new Businesses();
                    favourite.Id = Convert.ToInt32(dr["id"]);
                    favourite.Image = (string)dr["image"];
                    favourite.Name = (string)dr["name"];
                    favourite.Reating = (float)Convert.ToDouble(dr["reating"]);
                    favourite.Category = (string)dr["category"];
                    favourite.PriceRange = Convert.ToInt32(dr["priceRange"]);
                    favourite.Phone = (string)dr["phone"];
                    favourite.Address = (string)dr["address"];
                    favourite.CuisineId = Convert.ToInt32(dr["cusiId"]);
                    favourite.Url = (string)dr["url"];
                    int id = Convert.ToInt32(dr["id"]);
                    favourite.Highlights = getRestHighlights(id);
                    rList.Add(favourite);
                }
                return rList;
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
                        Businesses favourite = new Businesses();
                        favourite.Id = Convert.ToInt32(dr["id"]);
                        favourite.Image = (string)dr["image"];
                        favourite.Name = (string)dr["name"];
                        favourite.Reating = (float)Convert.ToDouble(dr["reating"]);
                        favourite.Category = (string)dr["category"];
                        favourite.PriceRange = Convert.ToInt32(dr["priceRange"]);
                        favourite.Phone = (string)dr["phone"];
                        favourite.Address = (string)dr["address"];
                        favourite.CuisineId = Convert.ToInt32(dr["cusiId"]);
                        favourite.Url = (string)dr["url"];
                        int id = Convert.ToInt32(dr["id"]);
                        favourite.Highlights = getRestHighlights(id);
                        rList.Add(favourite);
                    }
                    return rList;
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

