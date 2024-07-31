using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection 
        /// </summary>
        private readonly IMongoCollection<Client> _client;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public ClientController(MongoDbService mongoDbService)
        {
            // Obtém a collection "clients"
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var client = await _client.Find(c => c.IdClient == id).FirstOrDefaultAsync();
                if (client == null)
                {
                    return NotFound("Cliente não encontrado!");
                }
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Client updatedClient)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(c => c.IdClient, id);
                var client = await _client.Find(filter).FirstOrDefaultAsync();
                if (client == null)
                {
                    return NotFound("Cliente não encontrado!");
                }

                client.UserId = updatedClient.UserId;
                client.Cpf = updatedClient.Cpf;
                client.Phone = updatedClient.Phone;
                client.Address = updatedClient.Address;
                client.AdditionalAttributes = updatedClient.AdditionalAttributes;

                var result = await _client.ReplaceOneAsync(filter, client);

                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(c => c.IdClient, id);
                var result = await _client.DeleteOneAsync(filter);

                return Ok("Cliente deletado com sucesso!");
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

