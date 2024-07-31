using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        [BsonId]
        [BsonElement("_idClient"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdClient { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? UserId { get; set; }

        [BsonElement("cpf")]
        public string? Cpf { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        public Dictionary<string, string>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe Product, o atributo AdditionalAttributes já virá com um novo dicionario e portanto habilitado para adicionar + atributos.
        /// </summary>
        public Client()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
