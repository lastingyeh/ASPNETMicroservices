using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using Microsoft.Extensions.Logging;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogService> _logger;
        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            _logger.LogInformation("Getting Catalog Products from url: {url} and custom property : {customProperty}", _httpClient.BaseAddress, 6);

            var response = await _httpClient.GetAsync("/Catalog");

            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _httpClient.GetAsync($"/Catalog/{id}");

            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _httpClient.GetAsync($"/Catalog/GetProductByCategory/{category}");

            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var response = await _httpClient.PostAsJson($"/Catalog", model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling api.");

            return await response.ReadContentAs<CatalogModel>();
        }
    }
}