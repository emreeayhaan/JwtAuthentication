using MongoDB.Driver;
using System.Linq.Expressions;

namespace DataAccess
{
    public class MongoDB<T>
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDB()
        {
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URI"));
            _mongoCollection = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME")).GetCollection<T>(typeof(T).Name);
        }

        public List<T> GetAll() => _mongoCollection.Find(_ => true).ToList();
        public List<T> GetAll(Expression<Func<T, bool>> filter) => _mongoCollection.Find(filter).ToList();
        public T Get(Expression<Func<T, bool>> filter) => _mongoCollection.Find(filter).FirstOrDefault();
        public void Add(T model) => _mongoCollection.InsertOne(model);
        public void Update(Expression<Func<T, bool>> filter, T model) => _mongoCollection.FindOneAndReplace(filter, model);
        public void Delete(Expression<Func<T, bool>> filter) => _mongoCollection.FindOneAndDelete(filter);
    }
}
