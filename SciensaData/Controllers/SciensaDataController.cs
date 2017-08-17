using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using System.Threading;
using Microsoft.ServiceFabric.Data.Collections;
using SciensaData.Models;

namespace SciensaData.Controllers
{
    [Route("api/[controller]")]
    public class SciensaDataController : Controller
    {
        private readonly IReliableStateManager stateManager;

        public SciensaDataController(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ct = new CancellationToken();

            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Cliente>>("SciensaBank_Clientes");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var list = await votesDictionary.CreateEnumerableAsync(tx);

                var enumerator = list.GetAsyncEnumerator();

                var result = new List<KeyValuePair<int, Cliente>>();

                while (await enumerator.MoveNextAsync(ct))
                {
                    result.Add(enumerator.Current);
                }

                return Json(result);
            }
        }
        
        [HttpPut("{nome}/{cpf}/{endereco}")]
        public async Task<IActionResult> Put(string nome, string cpf, string endereco)
        {
            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Cliente>>("SciensaBank_Clientes");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var cliente = new Cliente();
                cliente.Nome = nome;
                cliente.CPF = cpf;
                cliente.Endereco = endereco;

                var idx = await votesDictionary.GetCountAsync(tx);

                await votesDictionary.AddAsync(tx, (int)(idx + 1), cliente);
                await tx.CommitAsync();
            }

            return new OkResult();
        }

        [HttpPut("{NomeCliente}/{NumeroConta}/{TipoConta}/{SaldoConta}")]
        public async Task<IActionResult> Put(string NomeCliente, string NumeroConta, string TipoConta, string SaldoConta)
        {
            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Conta>>("SciensaBank_Contas");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var conta = new Conta();
                conta.NomeCliente = NomeCliente;
                conta.NumeroConta = NumeroConta;
                conta.TipoConta = TipoConta;
                conta.SaldoConta = SaldoConta;

                var idx = await votesDictionary.GetCountAsync(tx);

                await votesDictionary.AddAsync(tx, (int)(idx + 1), conta);
                await tx.CommitAsync();
            }

            return new OkResult();
        }

        [HttpGet("{NomeCliente}")]
        public async Task<IActionResult> Get(string NomeCliente)
        {
            var ct = new CancellationToken();

            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Conta>>("SciensaBank_Contas");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var list = await votesDictionary.CreateEnumerableAsync(tx);

                var enumerator = list.GetAsyncEnumerator();

                var result = new List<KeyValuePair<int, Conta>>();

                while (await enumerator.MoveNextAsync(ct))
                {
                    if(((Conta)enumerator.Current.Value).NomeCliente == NomeCliente)
                    {
                        result.Add(enumerator.Current);
                    }                    
                }

                return Json(result);
            }
        }
    }
}
