using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;

namespace ProteinTrackerMVC.Api
{
    public class RedisRepository : IRepository
    {
        IRedisClientsManager RedisManager { get; set; }

        public RedisRepository(IRedisClientsManager redisManager)
        {
            RedisManager = redisManager;
        }

        public long AddUser(string name, int goal)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                var user = new User() {Name = name, Goal = goal, Id = redisUsers.GetNextSequence()};
                redisUsers.Store(user);
                return user.Id;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                return redisUsers.GetAll();
            }
        }

        public User GetUsers(long userId)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                return redisUsers.GetById(userId);
            }
        }

        public void UpdateUser(User user)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                redisUsers.Store(user);
            }
        }
    }
}