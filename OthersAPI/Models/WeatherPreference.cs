using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OthersAPI.Models
{
    public record WeatherPreference
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public DateTimeOffset CreateDate { get; init; }
    }
}
