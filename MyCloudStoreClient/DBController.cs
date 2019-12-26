using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MyCloudStoreClient
{
    class DBController
    {
        public static bool storeFileInfo(string fileName, byte[] key, long size)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=fileInfo;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into dbo.localFiles (fileN,fileK,fileS) VALUES (@fileName, @fileKey, @fileSize)";
                    command.Parameters.AddWithValue("@fileName", fileName);
                    command.Parameters.AddWithValue("@fileKey", key);
                    command.Parameters.AddWithValue("@fileSize", size);

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

        public static bool readFileInfo(string fileName, ref byte[] key, ref long size)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=fileInfo;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.localFiles where fileN=@fileName";
                    command.Parameters.AddWithValue("@fileName", fileName);

                    try
                    {
                        connection.Open();
                        IList<string> ret = new List<string>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                key = (byte[])reader["fileK"];
                                size = Int64.Parse(reader["fileS"].ToString());
                               
                            }
                        }
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

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static void deleteFileLocal(string fileName)
        {
            string cs = "Data Source=DESKTOP-F558RH3\\SQLEXPRESS;Initial Catalog=fileInfo;Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from dbo.localFiles where fileN=@fileName";
                    command.Parameters.AddWithValue("@fileName", fileName);
                  

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
       
    }
}
