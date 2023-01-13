using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ToDoListWithoutEF.Models;
namespace ToDoListWithoutEF.DataAccessLayer
{
    public class JobDAL
    {
        private IConfiguration configuration;
        private SqlConnection con ;
        private string connectionstring;
        public JobDAL(IConfiguration _configuration) {
            connectionstring= _configuration.GetConnectionString("DefaultConnection");
            configuration = _configuration;
            con = new SqlConnection(connectionstring);
        }

        public List<Job> SearchJob(int Type,string val)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                List<Job> res = new List<Job>();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_GetAllJobsContains";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@JobString", val);
                
                
                DataTable tb = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tb);
                con.Close();

                foreach (DataRow row in tb.Rows)
                {
                    res.Add(new Job
                    {
                        Id = Convert.ToInt32(row[0]),
                        Title = Convert.ToString(row[1]),
                        Description = Convert.ToString(row[2]),
                        IsCheck = Convert.ToBoolean(row[3]),
                        Target = Convert.ToString(row[4]),
                        Level = Convert.ToInt32(row[5]),
                        AuthorName = Convert.ToString(row[6]),
                        AuthorId = Convert.ToInt32(row[7])
                    });
                }

                return res;
            }
            catch (Exception)
            {
                throw new Exception("error");
            }
        }
        public List<Job> GetAllJobs()
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand();
                List<Job> res = new List<Job>();
                cmd.Connection = con;
                cmd.CommandText = "sp_GetAllJobs";
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                foreach (DataRow row in dt.Rows)
                {
                    res.Add(new Job
                    {
                        Id = Convert.ToInt32(row[0]),
                        Title = Convert.ToString(row[1]),
                        Description = Convert.ToString(row[2]),
                        IsCheck = Convert.ToBoolean(row[3]),
                        Target = Convert.ToString(row[4]),
                        Level = Convert.ToInt32(row[5]),
                        AuthorName = Convert.ToString(row[6]),
                        AuthorId = Convert.ToInt32(row[7])
                    });
                }
                return res;
            }
            catch (Exception)
            {
                throw new Exception("error");
            }
            
        }

        public bool CreateJob(string Title , string Description, string Target, int Level, int AuthorId)
        {
            try {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_CreateJob";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JobTitle", Title);
                if (Description == null) {
                    Description = "";
                }
                if (Target == null)
                {
                    Target = "";
                }
                cmd.Parameters.AddWithValue("@JobDescription", Description);
                cmd.Parameters.AddWithValue("@JobTarget", Target);
                cmd.Parameters.AddWithValue("@JobLevel", Level);
                cmd.Parameters.AddWithValue("@JobAuthorId", AuthorId);
                SqlParameter returnParameter = cmd.Parameters.Add("@stt", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                int res = (int)returnParameter.Value;
                con.Close();
                return (res>0);
            }
            catch (Exception e)
            {
                return false;
            }
            

        }

        
        public Job GetJobById(int id)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_GetJobById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            adapter.Fill(dt);
            List<Job> res = new List<Job>();
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new Job
                {
                    Id = Convert.ToInt32(row[0]),
                    Title = Convert.ToString(row[1]),
                    Description = Convert.ToString(row[2]),
                    IsCheck = Convert.ToBoolean(row[3]),
                    Target = Convert.ToString(row[4]),
                    Level = Convert.ToInt32(row[5]),
                    AuthorName = Convert.ToString(row[6]),
                    AuthorId = Convert.ToInt32(row[7])
                });
            }
            return res[0];


        }

        public int EditJob(Job job)
        {
            if (con.State == ConnectionState.Closed) con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_NEditJob";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobId", job.Id);
            cmd.Parameters.AddWithValue("@JobTitle", job.Title);
            if (job.Description == null) job.Description = "";
            cmd.Parameters.AddWithValue("@JobDescription", job.Description);
            cmd.Parameters.AddWithValue("@JobCheck", job.IsCheck);
            cmd.Parameters.AddWithValue("@JobAuthorId", job.AuthorId);
            cmd.Parameters.AddWithValue("@JobTarget", job.Target);
            cmd.Parameters.AddWithValue("@JobLevel", job.Level);
            SqlParameter returnParameter = cmd.Parameters.Add("@stt", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int res = (int)returnParameter.Value;
            con.Close();
            return res ;

            /*

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return (i>0);

            */
        }

        public bool RemoveJob(int id)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                    
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_RemoveJob";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JobId", id);

                int i = cmd.ExecuteNonQuery();

                con.Close();
                return (i == 0) ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

    }

}
