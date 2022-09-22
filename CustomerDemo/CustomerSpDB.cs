using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace CustomerDemo
{
    internal class CustomerSpDB
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        public CustomerSpDB()
        {
            string conStr = "server=.;database=SoleraEmployees;user id=sa;pwd=sa";
            con = new SqlConnection(conStr);
        }
        public void AddCustomer(Customer c)
        {
            string insStr = $"AddCustomer";
            cmd = new SqlCommand(insStr,con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
        public int GenerateID()
        {
            string cmdStr = "select max(cid)from Customer";

            cmd = new SqlCommand(cmdStr, con);
            int genId = 0;
            try
            {
                con.Open();
                object data = cmd.ExecuteScalar();
                if (data.ToString().Equals(""))
                {
                    genId = 1;
                }
                else
                {
                    genId = Convert.ToInt32(data) + 1;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return genId;
        }
       
        public string UpdateCustomer(int cid, Customer c)
        {

            string updStr = $"UpdateCustomer";
            cmd = new SqlCommand(updStr, con);
            cmd.Parameters.Add(new SqlParameter("@cid", c.CID));
            cmd.Parameters.Add(new SqlParameter("@cname", c.CNAME));
            cmd.Parameters.Add(new SqlParameter("@gender", c.CGENDER));
            cmd.Parameters.Add(new SqlParameter("@address", c.ADDRESS));
            cmd.Parameters.Add(new SqlParameter("@mobile", c.MOBILE));
            SqlParameter sp=new SqlParameter("@sts",SqlDbType.VarChar,100);
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            string returnData = "";


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnData = cmd.Parameters[5].Value.ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

            return returnData;


        }
        public bool DeleteCustomer(int cid)
        {
            string dltStr = $"Delete Customer";
            cmd = new SqlCommand(dltStr, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid", cid));
            SqlParameter sp = new SqlParameter("@sts", SqlDbType.VarChar, 100);
            sp.Direction=ParameterDirection.Output;
            
               cmd. Parameters.Add(sp);



            try
            {
                con.Open();
                int rEffected = cmd.ExecuteNonQuery();
                if (rEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }




        }
        public Customer FindCustomer(int cid)
        {
            string sltStr = $"FindCustomer";
            cmd = new SqlCommand(sltStr, con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cid",cid));
            SqlDataReader dr = null;
            Customer cr = null;



            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    cr = new Customer
                    {
                        CID = dr.GetInt32(0),
                        CNAME = dr.GetString(1),
                        CGENDER = dr.GetString(2),
                        ADDRESS = dr.GetString(3),
                        MOBILE = dr.GetString(4)
                    };
                    return cr;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return cr;



        }
        public List<Customer> GetCustomers()
        {
            List<Customer> lcst = new List<Customer>();
            string sltStr = $" CustomerSummary";
            cmd = new SqlCommand(sltStr, con);
            SqlDataReader dr = null;
            Customer cr = null;



            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cr = new Customer
                        {
                            CID = dr.GetInt32(0),
                            CNAME = dr.GetString(1),
                            CGENDER = dr.GetString(2),
                            ADDRESS = dr.GetString(3),
                            MOBILE = dr.GetString(4)
                        };
                        lcst.Add(cr);
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return lcst;



        }




    }

}
