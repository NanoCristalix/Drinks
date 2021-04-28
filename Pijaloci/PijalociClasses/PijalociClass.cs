using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pijaloci.PijalociClasses
{
    class PijalociClass
    {
        //get i set properties
        public int DrinkID { get; set; }
        public string Price { get; set; }
        public string DrinkName { get; set; }
        public string TableNo { get; set; }

        static string mykonekcija = ConfigurationManager.ConnectionStrings["Konekcija"].ConnectionString; 

       
        //Selektiranje na datata od Database   
        public DataTable Select()
        {
            //Cekor1: Database Connection
            SqlConnection conn = new SqlConnection(mykonekcija);
            DataTable dt = new DataTable();

            try
            {

                //Cekor2: Writing SQL query
                string sql = "SELECT *FROM tbl_pijaloci";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);


            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;


        }

        //Inserting Data into Database
        public bool Insert (PijalociClass c)
        {
            //Create a default return type and settings its value to false
            bool isSuccess = false;

            //Cekor1: Connect DataBase
            SqlConnection conn = new SqlConnection(mykonekcija);
            try
            {
                //Cekor2:Create Sql Query to insert data

                string sql = " INSERT INTO tbl_pijaloci (Price, DrinkName, TableNo) VALUES (@Price, @DrinkName, @TableNo)";

                //creating sql command  using sql and conn

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Parameters to add Data

                cmd.Parameters.AddWithValue("@Price", c.Price);
                cmd.Parameters.AddWithValue("@DrinkName", c.DrinkName);
                cmd.Parameters.AddWithValue("TableNo", c.TableNo);

                //Connection Opeh Here

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                
                //If the query runs succesfully then the value of rows will be greater then zero  else its value will be 0
                if(rows>0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //Method to update data in database from our application

        public bool Update(PijalociClass c)
        {
            //Create a default return type and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(mykonekcija);
            try
            {
                //SQL to update data in our database 
                string sql = "UPDATE tbl_pijaloci SET Price=@Price, DrinkName=@DrinkName, TableNo=@TableNo WHERE DrinkID=@DrinkID";

                //Creating SQL command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Parameters to add value
                cmd.Parameters.AddWithValue("@Price", c.Price);
                cmd.Parameters.AddWithValue("@DrinkName", c.DrinkName);
                cmd.Parameters.AddWithValue("@TableNo", c.TableNo);
                cmd.Parameters.AddWithValue("DrinkID", c.DrinkID);

                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query runs succesfully then the value of  rows will be greater then null else its value will be zero
                if(rows>0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }

            }

            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return isSuccess;

        }

        //Method to delete data from database
        public bool Delete(PijalociClass c)
        {
            //create default return value and set its value to false
            bool isSuccess = false;

            //Create sql connection
            SqlConnection conn = new SqlConnection(mykonekcija);
            try
            {
                //SQl to detele data
                string sql = "DELETE FROM tbl_pijaloci WHERE DrinkID=@DrinkID";

                //Creating sql command 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DrinkID", c.DrinkID);

                //Open connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfully then the value of  rows will be greater then null else its value will be zero
                if(rows>0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //close connection
                conn.Close();
            }
            return isSuccess;

        }





    }
}
