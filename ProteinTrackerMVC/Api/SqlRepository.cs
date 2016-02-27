using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;

namespace ProteinTrackerMVC.Api
{
    public class SqlRepository :IRepository
    {
        string connection =  ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public SqlRepository()
        {
            
        }
        public long AddUser(string name, int goal)
        {
            User user = new User {Goal = goal,Id = 0,Name = name,Total = 0};
            
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from dbo_User"))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //count entities from db
                            int count =+ 1;
                            //insert id into insert statement below
                            user.Id = count;
                        }

                    }
                }
                using (
                    SqlCommand cmd =
                        new SqlCommand("insert into dbo_User(Name, Goal, Total) values(@Name, @Goal, @Total)"))
                {
                    cmd.Parameters.Add(new SqlParameter("@Name", user.Name));
                    cmd.Parameters.Add(new SqlParameter("@Goal", user.Goal));
                    cmd.Parameters.Add(new SqlParameter("@Total", user.Total));
                }
                return user.Id;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUsers(long userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}