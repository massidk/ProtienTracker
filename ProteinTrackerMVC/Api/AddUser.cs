using ServiceStack;
using ServiceStack.ServiceHost;

namespace ProteinTrackerMVC.Api
{
    [Route("/users", "POST")]
    public class AddUser
    {
        public string Name { get; set; }
        public int Goal { get; set; }
    }
}