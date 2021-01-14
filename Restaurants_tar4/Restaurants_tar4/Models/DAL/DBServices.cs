﻿using System;
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

        public List<Businesses> getBusinesses()
        {
            SqlConnection con = null;
            List<Businesses> fList = new List<Businesses>();
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM RestaurantsB_2021";
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
                    fList.Add(favourite);
                }
                return fList;
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
                        c.Name = (string)dr["name"];
                        c.Lastname = (string)dr["fname"];
                        c.Email = (string)dr["email"];
                        c.Phone = (string)dr["phone"];
                        c.Password = (string)dr["password"];
                        c.PriceRange = Convert.ToInt32(dr["price_range"]);
                        int id = Convert.ToInt32(dr["id"]);
                        c.Chlist =getCustomerHighlights(id);
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

    }
}
