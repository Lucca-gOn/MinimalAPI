using MongoDB.Driver;
using System.Data.Common;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena a configuração da aplicação.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referência ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a config da aplicação como parâmetro
        /// </summary>
        /// <param name="configuration">objeto configuration</param>
        public MongoDbService(IConfiguration configuration)
        {
            //Atribui a configuração recebida em _configuration
            _configuration = configuration;

            //Obtem a string de conexão atraves do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");

            //Cria um objeto MongoUrl que recebe como parâmetro a string de conexão
            var mongoUrl = MongoUrl.Create(connectionString);

            //Cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            //Obtem a referencia ao bd com o nome especificado na string de conexão.
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase GetDatabase => _database;
    }
}
