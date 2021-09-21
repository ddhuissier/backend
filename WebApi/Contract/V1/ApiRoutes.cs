using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Contract.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "V1";
        public const string Base = Root+"/"+Version;
        public static class Products {
            public const string GetProducts = Base+"/GetProducts";
            public const string GetProduct = Base + "/GetProduct/{id}";
            public const string PostProduct = Base + "/PostProduct";
            public const string PutProduct = Base + "/PutProduct/{id}";
            public const string DeleteProduct = Base + "/DeleteProduct/{id}";
        }
    }
}
