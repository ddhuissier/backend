using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OthersAPI.Settings
{
    public class MongoDBSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString { 
            get {
                return $"mongodb://{Host}:{Port}";
            } }
    }
}
