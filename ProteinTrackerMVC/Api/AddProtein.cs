using ServiceStack;
using ServiceStack.ServiceHost;

namespace ProteinTrackerMVC.Api
{
    [Route("/users/{userid}", "POST")]
    public class AddProtein
    {
        public long UserId { get; set; }
        public int Amount { get; set; }
    }
}