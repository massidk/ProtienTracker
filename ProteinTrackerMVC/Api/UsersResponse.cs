using System.Collections.Generic;

namespace ProteinTracker.Api
{
    public class UsersResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}