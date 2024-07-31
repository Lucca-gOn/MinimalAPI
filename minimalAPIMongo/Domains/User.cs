using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class User
    {
        [BsonId]
        [BsonElement("_idUser"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdUser { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        public Dictionary<string, string>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe Product, o atributo AdditionalAttributes já virá com um novo dicionario e portanto habilitado para adicionar + atributos.
        /// </summary>
        public User()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
