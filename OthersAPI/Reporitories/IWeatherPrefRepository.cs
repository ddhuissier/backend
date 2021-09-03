using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OthersAPI.Models;

namespace WebApi.Repositories
{
    public interface IWeatherPrefRepository
    {
        IEnumerable<WeatherPreference> Get();
        WeatherPreference Get(Guid id);
        void Create(WeatherPreference pref);
        void Update(Guid id,WeatherPreference pref);
        void Delete(Guid id);
    }
}
