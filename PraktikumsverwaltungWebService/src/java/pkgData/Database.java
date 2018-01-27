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
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
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
        
        private void disconnect() {
            client.close();
        }
        
        public ArrayList<Company> getAllCompanies() throws Exception {
            ArrayList<Company> allCompanies = new ArrayList<>();
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            for(Document d : collection.find()){
                Company c = gson.fromJson(d.toJson(), Company.class);
                c.setId(d.getObjectId("_id").toString());
                allCompanies.add(c);
            }
            disconnect();
            return allCompanies;
        }
        
        public Company getCompanyById(ObjectId id) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            disconnect();
            return gson.fromJson(collection.find(eq("_id", id)).first().toJson(), Company.class);
        }
        
        public Company addCompany(Company c) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Company");
            
            collection.insertOne(Document.parse(gson.toJson(c, Company.class)));
            
            disconnect();
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
                Pupil p = new Pupil();//gson.fromJson(d.toJson(), Pupil.class); //funktioniert nicht mehr wegen ids als string
                p.setUsername(d.getString("username"));
                p.setPassword(d.getString("password"));
                p.setFirstName(d.getString("firstName"));
                p.setLastname(d.getString("lastName"));
                p.setEmail(d.getString("email"));
                p.setIsActive(d.getBoolean("isActive"));
                p.setId(d.getObjectId("_id").toString());            // to make the id's "visible"
                p.setIdDepartment(d.getObjectId("idDepartment").toString());
                p.setIdClass(d.getObjectId("idClass").toString());
                listPupil.add(p);
            }
            disconnect();
            return listPupil;
        }
        
        // checks if login of pupil is ok
        public Pupil getIsLoginOkPupil(String username, String password) throws Exception {
            Pupil p = null;
            mongoDb = connect();
            Gson gson = new Gson();
            MongoCollection<Document> collection = mongoDb.getCollection("Pupil");
            
            BasicDBObject query = new BasicDBObject();
            query.put("username", username);
            query.put("password", password);            
            
            for(Document d : collection.find(query)) {
                p = new Pupil();
                p.setUsername(d.getString("username"));
                p.setPassword(d.getString("password"));
                p.setFirstName(d.getString("firstName"));
                p.setLastname(d.getString("lastName"));
                p.setEmail(d.getString("email"));
                p.setIsActive(d.getBoolean("isActive"));
                p.setId(d.getObjectId("_id").toString());
                p.setIdDepartment(d.getObjectId("idDepartment").toString());
                p.setIdClass(d.getObjectId("idClass").toString());
            }
            disconnect();
            return p;
        }
        
        // checks if login of teacher is ok
        public Teacher getIsLoginOkTeacher(String username, String password) throws Exception {
            Teacher t = null;
            mongoDb = connect();
            Gson gson = new Gson();
            MongoCollection<Document> collection = mongoDb.getCollection("Teacher");
            
            BasicDBObject query = new BasicDBObject();
            query.put("username", username);
            query.put("password", password);            
            
            for(Document d : collection.find(query)) {
                t = gson.fromJson(d.toJson(), Teacher.class);
                t.setId(d.getObjectId("_id").toString());
            }
            disconnect();
            return t;
        }
        
//        public void addPupil(Pupil p) throws Exception {
//            ArrayList<Pupil> listPupil = new ArrayList<>();
////            MongoDatabase mongoDb = connect();
//            listPupil.add(p);
//        }
        
        public ArrayList<Teacher> getAllActiveTeachers() throws Exception {
            ArrayList<Teacher> listTeacher = new ArrayList<>();
            mongoDb = connect();
            Gson gson = new Gson();
            
            BasicDBObject query = new BasicDBObject();
            query.put("isActive", true);
            
            MongoCollection<Document> collection = mongoDb.getCollection("Teacher");
            for(Document d : collection.find(query)) {
                Teacher t = gson.fromJson(d.toJson(), Teacher.class);
                t.setId(d.getObjectId("_id").toString());            // to make the id "visible"
                listTeacher.add(t);
            }
            disconnect();
            return listTeacher;
        }
        
        // returns all accepted Entries (accepted by kv and av)
        public ArrayList<Entry> getAllEntries() throws Exception {
            ArrayList<Entry> listEntry = new ArrayList<>();
            mongoDb = connect();

            // Delivers only Entries which are accepted by KV and AV
            BasicDBObject query = new BasicDBObject();
            query.put("allowedTeacher", true);
            query.put("allowedAV", true);
            
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            for(Document d : collection.find(query)) {
                Entry e = new Entry(); //gson.fromJson(d.toJson(), Entry.class);
                e.setStartDate(d.getDate("startDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setEndDate(d.getDate("endDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setSalary(d.getDouble("salary"));
                e.setTitle(d.getString("title"));
                e.setDescription(d.getString("description"));
                e.setAllowedTeacher(d.getBoolean("allowedTeacher"));
                e.setAllowedAV(d.getBoolean("allowedAV"));
                e.setSeenByAdmin(d.getBoolean("seenByAdmin"));
                e.setId(d.getObjectId("_id").toString());            // to make the id's "visible"                
                e.setIdPupil(d.getObjectId("idPupil").toString());
                e.setIdCompany(d.getObjectId("idCompany").toString());
                e.setIdClass(d.getObjectId("idClass").toString());
                listEntry.add(e);
            }
            disconnect();
            return listEntry;
        }
        
        public ArrayList<Entry> getAllOwnEntries(String id) throws Exception {
            ArrayList<Entry> listEntry = new ArrayList<>();
            mongoDb = connect();

            // Delivers only Entries which are accepted by KV and AV
            BasicDBObject query = new BasicDBObject();
            query.put("idPupil", new ObjectId(id));
            
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            for(Document d : collection.find(query)) {
                Entry e = new Entry();
                e.setStartDate(d.getDate("startDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setEndDate(d.getDate("endDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setSalary(d.getDouble("salary"));
                e.setTitle(d.getString("title"));
                e.setDescription(d.getString("description"));
                e.setAllowedTeacher(d.getBoolean("allowedTeacher"));
                e.setAllowedAV(d.getBoolean("allowedAV"));
                e.setSeenByAdmin(d.getBoolean("seenByAdmin"));
                e.setId(d.getObjectId("_id").toString());            // to make the id's "visible"
                e.setIdPupil(d.getObjectId("idPupil").toString());
                e.setIdCompany(d.getObjectId("idCompany").toString());
                e.setIdClass(d.getObjectId("idClass").toString());
                listEntry.add(e);
            }
            disconnect();
            return listEntry;
        }
        
        public void addEntry(String jsonStringEntry) throws Exception {
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            
            Document doc = Document.parse(jsonStringEntry);            
            collection.insertOne(doc);
            
            disconnect();
        }
        
        // Replaces the whole "old" document with the new document because we don't know what exactly changed. Don't forget the query which ensures
        // that the "old" Entry is selected and replaced (_id of the entry remains equal)
        // !!!!!!! Make sure that the salary is always like a float, e.g. 1300.0 and NOT only 1300 => otherwise exception
        public void updateEntry(String jsonStringEditedEntry) throws Exception {
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            
            Document doc = Document.parse(jsonStringEditedEntry);
            
            BasicDBObject query = new BasicDBObject();
            query.put("_id", doc.getObjectId("_id"));
            
            collection.replaceOne(query, doc);
            
            disconnect();
        }
        
        public ArrayList<Entry> getAllUnacceptedAndUnseenEntries() {
            mongoDb = connect();
            ArrayList<Entry> listUnacceptedEntries = new ArrayList<>();
            Gson gson = new Gson();
            
            BasicDBObject query = new BasicDBObject();
            query.put("allowedTeacher", false);
            query.put("allowedAV", false);
            query.put("seenByAdmin", false);        // Needed, because without it would also load the entries which the admin declined but pupil hasn't updated yet
            
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            
            for(Document d : collection.find(query)){
                Entry e = new Entry();
                e.setStartDate(d.getDate("startDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setEndDate(d.getDate("endDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                e.setSalary(d.getDouble("salary"));
                e.setTitle(d.getString("title"));
                e.setDescription(d.getString("description"));
                e.setAllowedTeacher(d.getBoolean("allowedTeacher"));
                e.setAllowedAV(d.getBoolean("allowedAV"));
                e.setId(d.getObjectId("_id").toString());            // to make the id's "visible"
                e.setIdPupil(d.getObjectId("idPupil").toString());
                e.setIdCompany(d.getObjectId("idCompany").toString());
                e.setIdClass(d.getObjectId("idClass").toString());
                listUnacceptedEntries.add(e);
            }
            disconnect();
            return listUnacceptedEntries;
        }
        
        public Entry getEntry(String idOfEntry) throws Exception {
            Entry entry = null;
            mongoDb = connect();

            // Delivers only Entries which are accepted by KV and AV
            BasicDBObject query = new BasicDBObject();
            query.put("_id", new ObjectId(idOfEntry));
            
            MongoCollection<Document> collection = mongoDb.getCollection("Entry");
            for(Document d : collection.find(query)) {
                entry.setStartDate(d.getDate("startDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                entry.setEndDate(d.getDate("endDate").toInstant().atZone(ZoneId.systemDefault()).toLocalDate());
                entry.setSalary(d.getDouble("salary"));
                entry.setTitle(d.getString("title"));
                entry.setDescription(d.getString("description"));
                entry.setAllowedTeacher(d.getBoolean("allowedTeacher"));
                entry.setAllowedAV(d.getBoolean("allowedAV"));
                entry.setSeenByAdmin(d.getBoolean("seenByAdmin"));
                entry.setId(d.getObjectId("_id").toString());            // to make the id's "visible"                
                entry.setIdPupil(d.getObjectId("idPupil").toString());
                entry.setIdCompany(d.getObjectId("idCompany").toString());
                entry.setIdClass(d.getObjectId("idClass").toString());
            }
            disconnect();
            return entry;
        }
        
        public ArrayList<Department> getAllDepartments() throws Exception {
            ArrayList<Department> allDepartments = new ArrayList<>();
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            
            for(Document d : collection.find()){
                Department dep = gson.fromJson(d.toJson(), Department.class);
                dep.setId(d.getObjectId("_id").toString());
                allDepartments.add(dep);
            }
            disconnect();
            return allDepartments;
        }
        
        public Department getDepartmentById(ObjectId id) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            Department dep = gson.fromJson(collection.find(eq("_id", id)).first().toJson(), Department.class);
            
            disconnect();
            return dep;
        }
        
        public Department addDepartment(Department d) throws Exception {
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Department");
            
            collection.insertOne(Document.parse(gson.toJson(d, Department.class)));
            Department dep = gson.fromJson(collection.find().sort(new BasicDBObject("_id", -1)).first().toJson(), Department.class);
            
            disconnect();
            return dep;
        }        
        
        public ArrayList<Class> getAllClasses() throws Exception {
            ArrayList<Class> allClasses = new ArrayList<>();
            Gson gson = new Gson();
            mongoDb = connect();
            MongoCollection<Document> collection = mongoDb.getCollection("Class");
            
            for(Document d : collection.find()){
                Class c = new Class();
                c.setId(d.getObjectId("_id").toString());
                c.setDescription(d.getString("description"));
                c.setIdKV(d.getObjectId("idKV").toString());
                allClasses.add(c);
            }
            disconnect();
            return allClasses;
        }
}
