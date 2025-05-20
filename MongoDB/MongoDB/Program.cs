// See https://aka.ms/new-console-template for more information

using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
/*
Person person1 = new Person
{
    Id = 1,
    Name = "John Doe",
    Age = 30
};
Console.WriteLine();
foreach (var propertyInfo in typeof(Person).GetProperties())
{
    Console.WriteLine(propertyInfo.Name +";" + propertyInfo.GetValue(person1));
}
*/
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("bombababa");
var collection = database.GetCollection<BsonDocument>("Beispiel");

var list = await collection.Find(new BsonDocument()).ToListAsync();

foreach (var document in list)
{
    Console.WriteLine(document);
}