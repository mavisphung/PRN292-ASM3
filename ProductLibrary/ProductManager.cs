using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductLibrary
{
    public class ProductManager
    {
        string connectionString;

        public ProductManager()
        {
            connectionString = GetConnectionString();
        }

        public string GetConnectionString()
        {
            string str = "server=.;database=SaleDB;uid=sa;pwd=123";
            return str;
        }

        public DataTable GetProducts()
        {
            string sql = "SELECT * FROM Products";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtBook = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtBook);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return dtBook;
        }

        public bool AddNewProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Products " +
                                "VALUES(@id, @name, @price, @quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", p.ID);
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.UnitPrice);
                cmd.Parameters.AddWithValue("@quantity", p.Quantity);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int count = -1;
                try
                {
                    count = cmd.ExecuteNonQuery();
                } catch (Exception e)
                {
                    MessageBox.Show("ID is duplicated!");
                }
                return count > 0;
            }
        }

        public bool UpdateProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Products " +
                                "SET ProductName = @name, UnitPrice = @price, Quantity = @quantity " +
                                "WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", p.ID);
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.UnitPrice);
                cmd.Parameters.AddWithValue("@quantity", p.Quantity);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int count = -1;
                try
                {
                    count = cmd.ExecuteNonQuery();
                } catch (Exception e)
                {
                }
                return count > 0;
            }
        }

        public bool DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE Products WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int count = -1;
                try
                {
                    count = cmd.ExecuteNonQuery();
                } catch (Exception e)
                {
                }
                return count > 0;
            }
        }

        public Product FindProduct(int id)
        {
            Product result = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Products WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DbDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        result = new Product
                        {
                            ID = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            UnitPrice = dr.GetDecimal(2),
                            Quantity = dr.GetInt32(3),

                        };
                    }
                }
                return result;
            }
        }
    }
}
