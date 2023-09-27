using _3rdPartyApiCall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace _3rdPartyApiCall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("GetCatFactData")]
        public IActionResult GetAllData()
        {
            CatFactApiModel ApiModel = new CatFactApiModel();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //the data of this api is continuosly changing...
            string apiURL = "https://catfact.ninja/fact";

            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage response = client.GetAsync(apiURL).Result;

            if(response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ApiModel = JsonConvert.DeserializeObject<CatFactApiModel>(response.Content.ReadAsStringAsync().Result);
            }

            return Ok(ApiModel);
        }


        [HttpGet]
        [Route("GetAllProductData")]
        public IActionResult GetAllProductData()
        {
            ProductsApiModel ApiModel = new ProductsApiModel();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //the data of this api is continuosly changing...
            string apiURL = "https://dummyjson.com/products";

            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage response = client.GetAsync(apiURL).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                ApiModel = JsonConvert.DeserializeObject<ProductsApiModel>(response.Content.ReadAsStringAsync().Result);
            }

            return Ok(ApiModel);
        }
    }
}
