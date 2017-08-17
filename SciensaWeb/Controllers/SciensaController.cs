using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SciensaWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Votes")]
    public class SciensaController : Controller
    {
        private readonly HttpClient httpClient;
        string serviceProxyUrl = "http://localhost:19081/DesafioSciensa/SciensaData/api/SciensaData";
        string partitionKind = "Int64Range";
        string partitionKey = "0";

        public SciensaController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<KeyValuePair<int, object>> votes;

            HttpResponseMessage response = await this.httpClient.GetAsync($"{serviceProxyUrl}?PartitionKind={partitionKind}&PartitionKey={partitionKey}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            votes = JsonConvert.DeserializeObject<List<KeyValuePair<int, object>>>(await response.Content.ReadAsStringAsync());

            return Json(votes);
        }

        [HttpGet("{NomeCliente}")]
        public async Task<IActionResult> Get(string NomeCliente)
        {
            IEnumerable<KeyValuePair<int, object>> contas;

            HttpResponseMessage response = await this.httpClient.GetAsync($"{serviceProxyUrl}/{NomeCliente}?PartitionKind={partitionKind}&PartitionKey={partitionKey}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            contas = JsonConvert.DeserializeObject<List<KeyValuePair<int, object>>>(await response.Content.ReadAsStringAsync());

            return Json(contas);
        }

        [HttpPut("{nome}/{cpf}/{endereco}")]
        public async Task<IActionResult> Put(string nome, string cpf, string endereco)
        {
            string payload = $"{{ 'nome' : '{nome}' }}{{ 'cpf': '{cpf}' }}{{ 'endereco': '{endereco}' }}";
            StringContent putContent = new StringContent(payload, Encoding.UTF8, "application/json");
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string proxyUrl = $"{serviceProxyUrl}/{nome}/{cpf}/{endereco}?PartitionKind={partitionKind}&PartitionKey={partitionKey}";

            HttpResponseMessage response = await this.httpClient.PutAsync(proxyUrl, putContent);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }

        [HttpPut("{NomeCliente}/{NumeroConta}/{TipoConta}/{SaldoConta}")]
        public async Task<IActionResult> Put(string NomeCliente, string NumeroConta, string TipoConta, string SaldoConta)
        {
            string payload = $"{{ 'NomeCliente' : '{NomeCliente}' }}{{ 'NumeroConta': '{NumeroConta}' }}{{ 'TipoConta': '{TipoConta}' }}{{ 'SaldoConta': '{SaldoConta}' }}";
            StringContent putContent = new StringContent(payload, Encoding.UTF8, "application/json");
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string proxyUrl = $"{serviceProxyUrl}/{NomeCliente}/{NumeroConta}/{TipoConta}/{SaldoConta}?PartitionKind={partitionKind}&PartitionKey={partitionKey}";

            HttpResponseMessage response = await this.httpClient.PutAsync(proxyUrl, putContent);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
