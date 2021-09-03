using MongoDB.Bson;
using MongoDB.Driver;
using OthersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace OthersAPI.Reporitories
{
    public class WeatherPreferenceMongoDbRepository : IWeatherPrefRepository
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoCollection<WeatherPreference> prefs;
        private readonly FilterDefinitionBuilder<WeatherPreference> filterBuilder = Builders<WeatherPreference>.Filter;
        private const string dbName = "weather";
        private const string itemsName = "weatherpreferences";

        public WeatherPreferenceMongoDbRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            IMongoDatabase db = mongoClient.GetDatabase(dbName);
            prefs = db.GetCollection<WeatherPreference>(itemsName);
        }

        public void Create(WeatherPreference pref)
        {
            prefs.InsertOne(pref);
        }

        public void Delete(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            prefs.DeleteOne(filter);
        }

        public IEnumerable<WeatherPreference> Get()
        {
            return prefs.Find(new BsonDocument()).ToList();
        }

        public WeatherPreference Get(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return prefs.Find(filter).SingleOrDefault();
        }

        public void Update(Guid id,WeatherPreference pref)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            prefs.ReplaceOne(filter, pref);
        }
    }
}
