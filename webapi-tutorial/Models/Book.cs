using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebapiTutorial.Models {
  public class Book {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { set; get; }

    [BsonElement("Name")]
    public string BookName { set; get; }
    
    [BsonElement("Price")]
    public decimal Price { set; get; }

    [BsonElement("Category")]
    public string Category { set; get; }

    [BsonElement("Author")]
    public string Author { set; get; }
  }
}