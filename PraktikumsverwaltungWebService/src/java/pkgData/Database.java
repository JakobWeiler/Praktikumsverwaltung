/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import com.google.gson.Gson;
import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import static com.mongodb.client.model.Filters.eq;
import java.util.ArrayList;
import org.bson.Document;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
public class Database {
        private MongoClient client;
        private MongoDatabase mongoDb;
        private static Database db;
        private String connStr;
        private String dbName;
 
        private Database() {
         connStr = "mongodb://192.168.196.38:27017";  //intern
         //connStr = "mongodb://212.152.179.118:27017";   //extern
         dbName = "5BHIFS_BSD_Praktikumsverwaltung";
        }
        
        public static Database newInstance() {
            if(db==null){
                db = new Database();
            }
            return db;
        }
          
        private MongoDatabase connect() {
         client = new MongoClient(new MongoClientURI(connStr));   
         return client.getDatabase(dbName);
        }
        
        public ArrayList<Company> getAllCompanies() throws Exception {
            ArrayList<Company> allCompanies = new ArrayList<>();
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            for(Document d : collection.find()){
                Company c = gson.fromJson(d.toJson(), Company.class);
                c.setId(d.getObjectId("_id"));
                allCompanies.add(c);
            }
            return allCompanies;
        }
        
        public Company getCompanyById(ObjectId id) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            return gson.fromJson(collection.find(eq("_id", id)).first().toJson(), Company.class);
        }
        
        public Company addCompany(Company c) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            collection.insertOne(Document.parse(gson.toJson(c, Company.class)));
            
            //collection.find().sort()
            return null;
        }
}