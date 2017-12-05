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
import java.util.ArrayList;
import org.bson.Document;

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
         connStr = "mongodb://192.168.196.38:27017";
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
                allCompanies.add(gson.fromJson(d.toJson(), Company.class));
            }
            return allCompanies;
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
        
        public void addPupil(Pupil p) {
            ArrayList<Pupil> listPupil = new ArrayList<>();
            MongoDatabase mongoDb = connect();
            listPupil.add(p);
        }
}