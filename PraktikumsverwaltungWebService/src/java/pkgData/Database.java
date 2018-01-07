/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.mongodb.BasicDBObject;
import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import static com.mongodb.client.model.Filters.eq;
import java.lang.reflect.Type;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import org.bson.Document;
import org.bson.types.ObjectId;
import pkgMisc.LocalDateDeserializer;
import pkgMisc.LocalDateSerializer;

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
         //connStr = "mongodb://192.168.142.144:27017";  //intern
         connStr = "mongodb://212.152.179.118:27017";   //extern
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
        
        public ArrayList<Pupil> getAllActivePupils() throws Exception { 
            ArrayList<Pupil> listPupil = new ArrayList<>();
            mongoDb = connect();
            Gson gson = new Gson();
            
            BasicDBObject query = new BasicDBObject();
            query.put("isActive", true);
            
            MongoCollection<Document> collection = mongoDb.getCollection("Pupil");
            for(Document d : collection.find(query)) {                
                //listPupil.add(gson.fromJson(d.toJson(), Pupil.class));
                Pupil p = gson.fromJson(d.toJson(), Pupil.class);
                p.setId(d.getObjectId("_id"));            // to make the id's "visible"
                p.setIdDepartment(d.getObjectId("idDepartment"));
                p.setIdClass(d.getObjectId("idClass"));
                listPupil.add(p);
            }
            return listPupil;
        }
        
        // checks if login of pupil is ok
        public String getIsLoginOkPupil(String username, String password) throws Exception {
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
        public String getIsLoginOkTeacher(String username, String password) throws Exception {
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
        
        public void addPupil(Pupil p) throws Exception {
            ArrayList<Pupil> listPupil = new ArrayList<>();
            MongoDatabase mongoDb = connect();
            listPupil.add(p);
        }
        
        public ArrayList<Teacher> getAllActiveTeachers() throws Exception {
            ArrayList<Teacher> listTeacher = new ArrayList<>();
            mongoDb = connect();
            Gson gson = new Gson();
            
            BasicDBObject query = new BasicDBObject();
            query.put("isActive", true);
            
            MongoCollection<Document> collection = mongoDb.getCollection("Teacher");
            for(Document d : collection.find(query)) {
                Teacher t = gson.fromJson(d.toJson(), Teacher.class);
                t.setId(d.getObjectId("_id"));            // to make the id "visible"
                listTeacher.add(t);
            }
            return listTeacher;
        }
        
        // returns all accepted Entries (accepted by kv and av)
        public ArrayList<Entry> getAllEntries() throws Exception {
            ArrayList<Entry> listEntry = new ArrayList<>();
            mongoDb = connect();
            Gson gson = new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ").create();

//            GsonBuilder gsonBuilder = new GsonBuilder();
//            gsonBuilder.registerTypeAdapter(LocalDate.class, new LocalDateSerializer());
//            gsonBuilder.registerTypeAdapter(LocalDate.class, new LocalDateDeserializer());
//            Gson gson = gsonBuilder.create();


            

            // Delivers only Entries which are accepted by KV and AV
            BasicDBObject query = new BasicDBObject();
            query.put("allowedTeacher", true);
            query.put("allowedAV", true);
            
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            for(Document d : collection.find(query)) {
                System.out.println(d.toJson());
                Entry e = gson.fromJson(d.toJson(), Entry.class);
                e.setId(d.getObjectId("_id"));            // to make the id's "visible"
                e.setIdPupil(d.getObjectId("idPupil"));
                e.setIdCompany(d.getObjectId("idCompany"));
                e.setIdClass(d.getObjectId("idClass"));
                System.out.println(e.getId());
                listEntry.add(e);
            }
            return listEntry;
        }
        
        public void addEntry(Entry e) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            
            collection.insertOne(Document.parse(gson.toJson(e, Entry.class)));
        }
        
        public ArrayList<Department> getAllDepartments() throws Exception {
            ArrayList<Department> allDepartments = new ArrayList<>();
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            
            for(Document d : collection.find()){
                Department dep = gson.fromJson(d.toJson(), Department.class);
                dep.setId(d.getObjectId("_id"));
                allDepartments.add(dep);
            }
            return allDepartments;
        }
        
        public Department getDepartmentById(ObjectId id) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            
            return gson.fromJson(collection.find(eq("_id", id)).first().toJson(), Department.class);
        }
        
        public Department addDepartment(Department d) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            
            collection.insertOne(Document.parse(gson.toJson(d, Department.class)));
            
            return gson.fromJson(collection.find().sort(new BasicDBObject("_id", -1)).first().toJson(), Department.class);
        }        
}
