using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        //Define que esta prop é ID do objeto
        [BsonId]
        //Define o nome do campo MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("name")]
        public string? Name { get; set; }
        
        [BsonElement("price")]
        public decimal Price { get; set; }

        //Adiciona um dicionario para atributos adicionais 
        public Dictionary<string, string>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe Product, o atributo AdditionalAttributes já virá com um novo dicionario e portanto habilitado para adicionar + atributos.
        /// </summary>
        public Product()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
