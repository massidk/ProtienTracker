using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using ProteinTracker.Api;
using ServiceStack.Redis;

namespace ProteinTracker.Api
{
    public class RepositoryFactory
    {
        IRedisClientsManager RedisManager { get; set; }
        public IRepository GetRepository(string inc)
        {
            string asm = inc;
            if(asm == "Redis")
                return new RedisRepository(RedisManager);

            if(asm == "Sql")
                    return new SqlRepository();
            
            return null;
        }
    }
}