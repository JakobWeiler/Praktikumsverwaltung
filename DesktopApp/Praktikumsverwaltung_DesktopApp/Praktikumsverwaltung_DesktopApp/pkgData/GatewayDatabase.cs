using MongoDB.Bson;         // !!!
using MongoDB.Driver;       // !!!
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{    
    class GatewayDatabase
    {
        private static GatewayDatabase instance = null;

        private static MongoUrl mongoUrl = null;
        private IMongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        // Singleton
        public static GatewayDatabase newInstance()
        {
            if (instance == null)
            {
                instance = new GatewayDatabase();
                mongoUrl = new MongoUrl("mongodb://212.152.179.118");     // aphrodite. intern: 192.168.196.38       extern: 212.152.179.118
            }
            return instance;
        }

        public bool ConnectMongoDB()
        {
            bool successful = false;
            try
            {
                client = new MongoClient(mongoUrl);
                database = client.GetDatabase("5BHIFS_BSD_Praktikumsverwaltung");            // database of mongoDb
                successful = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ConnectMongoDB: " + ex.Message);
            }

            return successful;     
        }

        private void CheckConnectionMongoDB()
        {
            try
            {
                collection = database.GetCollection<BsonDocument>("Company");              //collection name

                var filter = new BsonDocument();
                var cursor = collection.Find(filter, null);
                Console.WriteLine("Anzahl der Elemente von Company: {0}", cursor.Count());

            }
            catch (Exception ex)
            {
                throw new Exception("Error in CheckConnectionMongoDB: " + ex.Message);
            }
        }

        public bool AddPupil(Pupil pupil)
        {
            bool successful = false;
            try
            {
                collection = database.GetCollection<BsonDocument>("Pupil");              //collection name

                var document = new BsonDocument { new BsonElement("username", pupil.Username), new BsonElement("password", pupil.Password), new BsonElement("firstName", pupil.Firstname), new BsonElement("lastName", pupil.Lastname), new BsonElement("email", pupil.Email), new BsonElement("isActive", true) };
                collection.InsertOneAsync(document).Wait();
                successful = true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddPupil: " + ex.Message);
            }

            return successful;
        }

        public bool CheckPupilLogin(string usernamePupil, string passwordPupil)
        {
            bool successful = false;
            try
            {
                collection = database.GetCollection<BsonDocument>("Pupil");              //collection name
                
                // Filter for username and password (they must be found "together" in the MongoDB)
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("username", usernamePupil) & builder.Eq("password", passwordPupil);
                var cursor = collection.Find(filter, null);
                
                if (cursor.Count() == 1)
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CheckPupilLogin: " + ex.Message);
            }

            return successful;

        }

        public List<string> GetAllEntries()
        {
            List<string> listMongoDBEntries = new List<string>();
            try
            {
                collection = database.GetCollection<BsonDocument>("Entry");              //collection name

                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("allowedTeacher", true) & builder.Eq("allowedAV", true);
                
                // Loop to convert from var to datatable in order to display data
                foreach (var post in collection.Find(filter).ToListAsync().Result)
                {
                    Console.WriteLine("Elemente: {0}", post.ToString());
                    listMongoDBEntries.Add(post.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllEntries: " + ex.Message);
            }

            return listMongoDBEntries;
        }        
    }
}
