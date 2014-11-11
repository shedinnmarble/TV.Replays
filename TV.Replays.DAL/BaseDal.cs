using MongoDB;
using MongoDB.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Linq;

namespace TV.Replays.DAL
{
    public abstract class BaseDal<T> where T : class,new()
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["mongodb_live"].ConnectionString;
        private static readonly string Database = "ReplaysTv";

        public string CollectionName { get; set; }

        public abstract void SetCollectionName();

        public BaseDal()
        {
            SetCollectionName();
        }

        public MongoConfiguration Configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();

                config.Mapping(mapping =>
                    {
                        mapping.DefaultProfile(profile =>
                        {
                            profile.SubClassesAre(t => t.IsSubclassOf(typeof(T)));
                        });
                        mapping.Map<T>();
                    });
                config.ConnectionString(ConnectionString);
                return config.BuildConfiguration();
            }
        }

        public virtual void Insert(T t)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var db = mongo.GetDatabase(Database);
                    var collection = db.GetCollection<T>(CollectionName);
                    collection.Insert(t, true);
                }

                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var database = mongo.GetDatabase(Database);
                    var collection = database.GetCollection<T>(CollectionName);
                    collection.Remove<T>(predicate);
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public virtual void Update(T t, Expression<Func<T, bool>> predicate)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var database = mongo.GetDatabase(Database);
                    var collection = database.GetCollection<T>(CollectionName);
                    collection.Update<T>(t, predicate);
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public virtual T Single(Expression<Func<T, bool>> predicate)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var database = mongo.GetDatabase(Database);
                    var collection = database.GetCollection<T>(CollectionName);
                    return collection.Linq().FirstOrDefault(predicate);
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public virtual List<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var database = mongo.GetDatabase(Database);
                    var collection = database.GetCollection<T>(CollectionName);
                    return collection.Linq().Where(predicate).ToList();
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public List<T> FindAll<O>(int pageIndex, int pageSize, out int count,
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, O>> selector,
            bool isDesc)
        {
            using (Mongo mongo = new Mongo(Configuration))
            {
                try
                {
                    mongo.Connect();
                    var database = mongo.GetDatabase(Database);
                    var collection = database.GetCollection<T>(CollectionName);
                    count = collection.Linq().Where(predicate).Count();
                    if (isDesc)
                        return collection
                            .Linq()
                            .Where(predicate)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(selector)
                            .ToList();
                    else
                        return collection
                            .Linq()
                            .Where(predicate)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .OrderBy(selector)
                            .ToList();
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
    }
}
