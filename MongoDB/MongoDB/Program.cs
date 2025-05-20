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
    //Console.WriteLine(document);
    string doc = document.ToString();
    dynamic person = JsonConvert.DeserializeObject(doc);
    Console.WriteLine("\n------");
    foreach (var person1 in person)
    {
        foreach (var propertyInfo in person1.GetType().GetProperties())
        {
            if (propertyInfo.Name == "Name" || propertyInfo.Name == "Value")
            {
                string n = propertyInfo.Name;
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(person1);
                Console.WriteLine($"{propertyName}: {propertyValue}");
            }
        }
    }

    foreach (var kvp in document)
    {
        Console.WriteLine(kvp.Name + ": " + kvp.Value);
    }
}
//Add new entry
BsonDocument doc1 = new BsonDocument
{
    { "_id", ObjectId.GenerateNewId() },
    { "name", "max" },
};
collection.InsertOne(doc1);
//Update entry
var filter = Builders<BsonDocument>.Filter.Eq("name", "max");
var update = Builders<BsonDocument>.Update.Set("name", "flo");
var result = await collection.UpdateOneAsync(filter, update);
Console.WriteLine($"Matched: {result.MatchedCount}, Modified: {result.ModifiedCount}");
//delete entry
var filter1 = Builders<BsonDocument>.Filter.Eq("name", "max");
var result1 = await collection.DeleteOneAsync(filter);
Console.WriteLine($"Deleted Count: {result1.DeletedCount}");
