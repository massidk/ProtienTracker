using System.Collections.Generic;

namespace ProteinTrackerMVC.Api
{
    public class UsersResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}