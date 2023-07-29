using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class AdminModels
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("FirstName")]
        public string? FirstName { get; set; }
        [BsonElement("LastName")]
        public string? LastName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string? Password { get; set; }
        [BsonElement("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("City")]
        public string? City { get; set; }
        [BsonElement("Interested")]
        public string?[] Interested { get; set; }
        [BsonElement("dateofBirth")]
        public string? dateofBirth { get; set; }
    }
}
