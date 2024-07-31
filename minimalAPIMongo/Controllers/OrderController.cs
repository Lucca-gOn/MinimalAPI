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
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection 
        /// </summary>
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public OrderController(MongoDbService mongoDbService)
        {
            // Obtém a collection "orders"
            _order = mongoDbService.GetDatabase.GetCollection<Order>("orders");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");

        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {
                await _order.InsertOneAsync(order);
                return Ok(order);
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
                var order = await _order.Find(o => o.IdOrder == id).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Pedido não encontrado!");
                }
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Order updatedOrder)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(o => o.IdOrder, id);
                var order = await _order.Find(filter).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Pedido não encontrado!");
                }

                order.Date = updatedOrder.Date;
                order.Status = updatedOrder.Status;
                order.Products = updatedOrder.Products;
                order.ClientId = updatedOrder.ClientId;

                var result = await _order.ReplaceOneAsync(filter, order);
                
                return Ok(order);

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
                var filter = Builders<Order>.Filter.Eq(o => o.IdOrder, id);
                var result = await _order.DeleteOneAsync(filter);

                return Ok("Pedido deletado com sucesso!");
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

