using System.Data;
using System.Data.SqlClient;
using ToDoListWithoutEF.Models;

namespace ToDoListWithoutEF.DataAccessLayer
{
    public class AuthorDAL
    {
        private IConfiguration configuration;
        private SqlConnection con;
        private string connectionString;

        public AuthorDAL(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.configuration = configuration;
            con = new SqlConnection(connectionString);
        }

        public List<Author> GetAllAuthors() {

            List<Author> res = new List<Author>();

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_GetAllAuthors";
            cmd.CommandType = CommandType.StoredProcedure;
            /*
            SqlParameter returnValue = cmd.Parameters.Add("@stt", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            */

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                res.Add(new Author {
                    AuthorId = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1]),
                    Age = Convert.ToInt32(row[2]),
                    Note = Convert.ToString(row[3])
                });
            }
            
            cmd.ExecuteNonQuery();
            con.Close();

            return res;
        }

        public Author GetById(int id)
        {
            List<Author> res = new List<Author>();
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_GetAuthorById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
            {
                foreach (DataRow row in  dt.Rows) {
                    res.Add(new Author
                    {
                        AuthorId = Convert.ToInt32(row[0]),
                        Name = Convert.ToString(row[1]),
                        Age = Convert.ToInt32(row[2]),
                        Note = Convert.ToString(row[3])
                    });
                }
            }

            return res[0];
        }
        public bool CreateAuthor(string Name, int Age, string? Note)
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_CreateAuthor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorName", Name);
            cmd.Parameters.AddWithValue("@AuthorAge", Age);
            if(Note == null)
            {
                Note = "";
            }
            cmd.Parameters.AddWithValue("@AuthorNote", Note);
            SqlParameter returnValue = cmd.Parameters.Add("@stt", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();

            return ((int) returnValue.Value > 0);
        }

        public List<Job> RemoveAuthor(int id)
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_RemoveAuthor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorId", id);

            DataTable tb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);

            List<Job> res = new List<Job>();

            if (tb == null)
            {
               
            }
            else{
                foreach(DataRow row in tb.Rows) {
                    res.Add(new Job
                    {
                        Id = Convert.ToInt32(row[0]),
                        Title = Convert.ToString(row[1]),
                        Description = Convert.ToString(row[2]),
                        IsCheck = Convert.ToBoolean(row[3]),
                        Target = Convert.ToString(row[4]),
                        Level = Convert.ToInt32(row[5]),
                        AuthorId = Convert.ToInt32(row[6])
                    }
                    );
                }
            }
            con.Close();
            return res;

        }

        public bool SubmitRemoveAuthor(int id)
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_SubmitRemoveAuthor";
            cmd.Parameters.AddWithValue("@AuthorId", id);
            SqlParameter returnVal = cmd.Parameters.Add("@stt", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            con.Close();

            return ((int) returnVal.Value >0);
        }

    }
}
