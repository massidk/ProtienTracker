using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using ProteinTrackerMVC.Api;

namespace ProteinTrackerMVC.Api
{
    public class RepositoryFactory
    { 

        public IRepository GetRepository(string inc)
        {
            string asm = inc;
            //if(asm == "Redis")
                //return new RedisRepository();

            if(asm == "Sql")
                    return new SqlRepository();
            return null;
        }
    }
}