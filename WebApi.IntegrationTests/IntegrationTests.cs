using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi.Contract.V1;
using WebApi.Models;

namespace WebApi.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient httpClient;
        //    protected IntegrationTests()
        //    {
        //        var appFactory = new WebApplicationFactory<Startup>()
        //             .WithWebHostBuilder(builder =>
        //             {
        //                 builder.ConfigureServices(services =>
        //                 {
        //                     services.RemoveAll(typeof(AppDbContext));
        //                     services.AddDbContext<AppDbContext>(options =>
        //                     {
        //                         options.UseInMemoryDatabase("testDb");
        //                     });
        //                 });
        //             });
        //        httpClient = appFactory.CreateClient();
        //    }

        //    protected async Task AuthenticateAsync()
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //            "bearer", await GetJwtAsync());
        //    }
        //    protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        //    {
        //        var response = await httpClient.PostAsJsonAsync(ApiRoutes.Posts.Create, request);
        //        return await response.Content.ReadAsAsync<PostResponse>();
        //    }
        //    private async Task<string> GetJwtAsync()
        //    {
        //        var response = await httpClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
        //        {
        //            Email = "test@integration.com",
        //            Password = "SomePass1234!"
        //        });

        //        var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
        //        return registrationResponse.Token;
        //    }
    }
}
