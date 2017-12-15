/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import com.google.gson.Gson;
import com.mongodb.BasicDBObject;
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
         connStr = "mongodb://192.168.142.144:27017";  //intern
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
            
            return gson.fromJson(collection.find().sort(new BasicDBObject("_id", -1)).first().toJson(), Company.class);
        }
        
        public ArrayList<Pupil> getListPupil() { 
            ArrayList<Pupil> listPupil = new ArrayList<>();
            mongoDb = connect();
            Gson gson = new Gson();            
            
            MongoCollection<Document> collection = mongoDb.getCollection("Pupil");
            for(Document d : collection.find()) {
               listPupil.add(gson.fromJson(d.toJson(), Pupil.class));
            }
            return listPupil;
        }
        
        // checks if login of pupil is ok
        public String getIsLoginOkPupil(String username, String password) {
            String retVal = "false";
            mongoDb = connect();
            Gson gson = new Gson();
            MongoCollection<Document> collection = mongoDb.getCollection("Pupil");
            
            BasicDBObject query = new BasicDBObject();
            query.put("username", username);
            query.put("password", password);
            
            
            for(Document d : collection.find(query)) {
                retVal = "true";
            }
            return retVal;
        }
        
        // checks if login of teacher is ok
        public String getIsLoginOkTeacher(String username, String password) {
            String retVal = "false";
            mongoDb = connect();
            Gson gson = new Gson();
            MongoCollection<Document> collection = mongoDb.getCollection("Teacher");
            
            BasicDBObject query = new BasicDBObject();
            query.put("username", username);
            query.put("password", password);
            
            
            for(Document d : collection.find(query)) {
                retVal = "true";
            }
            return retVal;
        }
        
        public void addPupil(Pupil p) {
            ArrayList<Pupil> listPupil = new ArrayList<>();
            MongoDatabase mongoDb = connect();
            listPupil.add(p);
        }       
        
}