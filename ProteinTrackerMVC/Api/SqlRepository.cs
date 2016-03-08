using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using ServiceStack.Common.Utils;
using ServiceStack.Text;

namespace ProteinTracker.Api
{
    public class SqlRepository :IRepository 
    {
        private string conStr;
        private SqlConnection con;
        private SqlCommand cmd;
        private IEnumerable<User> userList;

        public SqlRepository()
        {
            conStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        }
        public long AddUser(string name, int goal)
        {
            //Oprettter nu bruger i db
            using (con = new SqlConnection(conStr))
            using (cmd = new SqlCommand("insert into dbo_User(Name, Goal, Total, Id) values (@name, @goal, @total, @id)",con))
            {
                con.Open();
                long id = 0;
                if (userList == null)
                    id = 1;
                else
                id = userList.LongCount()+1;
                User u = new User() { Name = name, Goal = goal, Id = id};

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@name";
                param.Value = name;
                param.SqlDbType = SqlDbType.Char;
                cmd.Parameters.Add(param);
                
                param = new SqlParameter();
                param.ParameterName = "@goal";
                param.Value = goal;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@total";
                param.Value = 0;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = id;
                param.SqlDbType = SqlDbType.BigInt;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                con.Close();
                //GetUsers();
                return u.Id;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            PopulateUsers();
            return userList;
        }

        private void PopulateUsers()
        {
            //Opdatere userList
            using (con = new SqlConnection(conStr))
            using (cmd = new SqlCommand("select * from dbo_User", con))
            {
                con.Open();
                List<User> list = new List<User>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User
                        {
                            Name = reader.GetString(0),
                            Goal = reader.GetInt32(1),
                            Total = reader.GetInt32(2),
                            Id = reader.GetInt64(3)
                        });
                    }
                }
                con.Close();
                userList = list;
            }
        }

        public User GetUsers(long userId) 
        {
            //Retunere bestemt user
            int i = 0;
            List<User> uList = userList.ToList();
            while (uList[i].Id != userId)
            {
                i++;
            }
            return uList[i];
        }

        public void UpdateUser(User user)
        {
            using (con = new SqlConnection(conStr))
            using (
                cmd =
                    new SqlCommand("update dbo_User set Name = @name, Goal = @goal, Total = @total where Id = @id", con)
                )
            {
                con.Open();
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@name";
                param.Value = user.Name;
                param.SqlDbType = SqlDbType.VarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@goal";
                param.Value = user.Goal;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@total";
                param.Value = user.Total;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = user.Id;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                con.Close();
                //GetUsers();
            }
        }
    }
}