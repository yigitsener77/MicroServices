using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RESTful_Mongo.Data;

namespace RESTful_Mongo.Services
{
    public abstract class BaseService<T> where T : Base
    {
        protected readonly IMongoCollection<T> _collection;
        public BaseService(IOptions<DatabaseSettings> options, string collectionName)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            _collection = database.GetCollection<T>(options.Value.Collections[collectionName]);
        }

        //Abstract CRUD Operations:
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
        public virtual async Task<IEnumerable<T>> GetManyAsync(FilterDefinition<T> filter) => await _collection.Find(filter).ToListAsync();
        public virtual async Task<T?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public virtual async Task CreateAsync(T item) => await _collection.InsertOneAsync(item);
        public virtual async Task UpdateAsync(string id, T item) => await _collection.ReplaceOneAsync(x => x.Id == id, item);
        public virtual async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
