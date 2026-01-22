using Oracle.ManagedDataAccess.Client;
using oracle_api_demo.Models;

namespace oracle_api_demo.Data
{
    public class OracleRepository
    {
        private readonly IConfiguration _configuration; 

        public OracleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private OracleConnection GetConnection()
        {
            return new OracleConnection(
                _configuration.GetConnectionString("OracleConnection")
                );
        }   

        public void InsertUser(UserModel user)
        {
            using var conn = GetConnection();
            conn.Open();

            using var cmd = new OracleCommand(
            "INSERT INTO USERS_API (NAME, EMAIL) VALUES (:name, :email)", conn);

            cmd.Parameters.Add(new OracleParameter("name", user.Name));
            cmd.Parameters.Add(new OracleParameter("email", user.Email));

            cmd.ExecuteNonQuery();
        }   

        public List<UserModel> GetUsers()
        {
            var users = new List<UserModel>();

            using var conn = GetConnection();
            conn.Open();

            using var cmd = new OracleCommand(
                "SELECT ID, NAME, EMAIL FROM USERS_API", conn);

            using var reader = cmd.ExecuteReader(); 

            while (reader.Read())
            {
                users.Add(new UserModel
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }

            return users;
        }
    }
}
