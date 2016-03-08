using System.Collections.Generic;

namespace ProteinTracker.Api
{
    public interface IRepository 
    {
        long AddUser(string name, int goal);
        IEnumerable<User> GetUsers();
        User GetUsers(long userId);
        void UpdateUser(User user);
    }
}