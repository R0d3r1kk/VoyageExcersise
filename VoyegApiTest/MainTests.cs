using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VoyageExcercise;
using VoyageExcercise.Controllers;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Helpers;
using VoyageExcercise.Interfaces;
using Xunit;

namespace VoyegApiTest
{
    public class MainTests : IClassFixture<WebApiTesterFactory>
    {
        private CrudController _crud;
        private IServices _services;

        //testerdataseeder
        /*public MainTests():base(
            new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlite("Filename=voyagedb.db")
            .Options)
        {
            _services = new ServicesHelper();

        }*/

        private readonly WebApiTesterFactory _factory;

        public MainTests(WebApiTesterFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TransactionsShouldGetCreated()
        {
            var httpClient = _factory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { page = 1, pagesize = 10 })),
                Method = HttpMethod.Post,
                RequestUri = new Uri(@"https://localhost:5005/api/crud/getAllTransactions"),
            };
            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Transactions>>(json);
            Assert.IsType<List<Transactions>>(list);
            Assert.Empty(list);

            
            //response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public async Task PaymentsShouldGetCreated()
        {

            var httpClient = _factory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { page = 1, pagesize = 10 })),
                Method = HttpMethod.Post,
                RequestUri = new Uri(@"https://localhost:5005/api/crud/getAllPayments"),
            };
            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Payments>>(json);
            Assert.IsType<List<Payments>>(list);
            Assert.Empty(list);

        }

        [Fact]
        public async Task ServicesShouldGetCreated()
        {

            var httpClient = _factory.CreateClient();
            var request = new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { page = 1, pagesize = 10 })),
                Method = HttpMethod.Post,
                RequestUri = new Uri(@"https://localhost:5005/api/crud/getAllServices"),
            };
            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<CServices>>(json);
            Assert.IsType<List<CServices>>(list);
            Assert.Empty(list);
        }

    }

  
}
