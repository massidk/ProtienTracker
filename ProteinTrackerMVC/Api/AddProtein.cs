using ServiceStack;
using ServiceStack.ServiceHost;

namespace ProteinTracker.Api
{
    [Route("/users/{userid}", "POST")]
    public class AddProtein
    {
        public long UserId { get; set; }
        public int Amount { get; set; }
    }
}