using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebapiTutorial.Models;

namespace WebapiTutorial.Services {
  public class BookService {
    private readonly IMongoCollection<Book> _books;
    
    public BookService(IConfiguration configuration) {
      var client = new MongoClient(configuration.GetConnectionString("BookstoreDb"));
      var database = client.GetDatabase("BookstoreDb");
      this._books = database.GetCollection<Book>("Books");
    }

    public Task<List<Book>> Get() {
      return this._books.Find(book => true).ToListAsync();
    }

    public Book Get(string id) {
      return this._books.Find(book => book.Id == id).FirstOrDefault();
    }

    public Book Create(Book newBook) {
      this._books.InsertOne(newBook);
      return newBook;
    }

    public void Update(string id, Book bookIn) {
      this._books.ReplaceOne(book => book.Id == id, bookIn);
    }

    public void Remove(string id) {
      this._books.DeleteOne(book => book.Id == id);
    }

    public void Remove(Book bookIn) {
      this._books.DeleteOne(book => book.Id == bookIn.Id);
    }
  }
}