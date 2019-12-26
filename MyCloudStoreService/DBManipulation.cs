using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MyCloudStoreService
{
    public class DBManipulation
    {
        public static bool authenticateUser(string userName, string password)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=usersDB;Integrated Security=True";

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("Select * from dbo.Users where USERNAME=@username and PSW=@password", con);
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            con.Close();
            int count = ds.Tables[0].Rows.Count;
            //If count is equal to 1, than show frmMain form
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool storeFile(string fileName, byte[] fileContent, string hashValue)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=filesDB;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into dbo.FILES (fName,fContent,fHashValue) VALUES (@fileName, @fileContent, @hashValue)";
                    command.Parameters.AddWithValue("@fileName", fileName);
                    command.Parameters.AddWithValue("@fileContent", fileContent);
                    command.Parameters.AddWithValue("@hashValue", hashValue);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException)
                    {
                        return false;
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        public static byte[] downloadFile(string fileName, string hashValue)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=filesDB;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select fContent from dbo.FILES where fName=@fileName and fHashValue=@hashValue";
                    command.Parameters.AddWithValue("@fileName", fileName);
                    command.Parameters.AddWithValue("@hashValue", hashValue);

                    try
                    {
                        connection.Open();
                        byte[] retBytes = (byte[])command.ExecuteScalar();
                        return retBytes;
                    }
                    catch (SqlException)
                    {
                        // error here
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static IList<string> getNameOfFiles(string hashValue)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=filesDB;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select fName from dbo.FILES where fHashValue=@hashValue";
                    command.Parameters.AddWithValue("@hashValue", hashValue);

                    try
                    {
                        connection.Open();
                        IList<string> ret = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ret.Add(reader["fName"].ToString());
                            }
                        }
                        return ret;
                    }
                    catch (SqlException)
                    {
                        // error here
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static byte[][] BufferSplit(byte[] buffer, int blockSize)
        {
            byte[][] blocks = new byte[(buffer.Length + blockSize - 1) / blockSize][];

            for (int i = 0, j = 0; i < blocks.Length; i++, j += blockSize)
            {
                blocks[i] = new byte[Math.Min(blockSize, buffer.Length - j)];
                Array.Copy(buffer, j, blocks[i], 0, blocks[i].Length);
            }

            return blocks;
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static double[] availableSpace(string userName)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=usersDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.Users where USERNAME=@userName";
                    command.Parameters.AddWithValue("@userName", userName);

                    try
                    {
                        connection.Open();
                        double[] d = new double[2];
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                d[0] = (double)reader["currData"];
                                d[1] = (double)reader["maxData"];
                            }
                        }

                        return d;

                    }
                    catch (SqlException)
                    {
                        // error here
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

        public static bool incData(string userName, double size)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=usersDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update dbo.Users set currData+=@size where USERNAME=@userName";
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@size", size);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                        
                    }
                    catch (SqlException)
                    {
                        // error here
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static void deleteFile(string hashValue, string fileName)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=filesDB;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from dbo.FILES where fName=@fileName and fHashValue=@hashValue";
                    command.Parameters.AddWithValue("@fileName", fileName);
                    command.Parameters.AddWithValue("@hashValue", hashValue);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        
                    }
                    catch (SqlException)
                    {
                       
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static void decData(string userName, double size)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=usersDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update dbo.Users set currData-=@size where USERNAME=@userName";
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@size", size);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                      

                    }
                    catch (SqlException)
                    {
                        // error here
                      
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

    }
}