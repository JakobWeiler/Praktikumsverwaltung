/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import com.google.gson.Gson;
import java.util.ArrayList;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Consumes;
import javax.ws.rs.Produces;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import org.bson.types.ObjectId;
import pkgData.Company;
import pkgData.Database;
import pkgData.Entry;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Entry")
public class EntryResource {

    @Context
    private UriInfo context;

    
    public EntryResource() {
    }

    
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getAllEntries() {
        ArrayList<Entry> listEntries;
        try {
            Database db = Database.newInstance();
            listEntries = db.getAllEntries();
        }
        catch (Exception ex) {
            listEntries = new ArrayList<>();
            listEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, "", "", ""));
        }
        
        return new Gson().toJson(listEntries);
    }
    
    @GET
    @Path("{entryId}")
    @Produces(MediaType.APPLICATION_JSON)
    public String getAllOwnEntries(@PathParam("entryId") String id) {
        ArrayList<Entry> listEntries = null;
         
        try{
            Database db = Database.newInstance();
            listEntries = db.getAllOwnEntries(id);
        }     
	catch(Exception ex){ 
                listEntries = new ArrayList<>();
                listEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, "", "", ""));      
            }
        return new Gson().toJson(listEntries);
    }
    
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    public String addEntry(String jsonStringEntry) throws Exception {
        String retValue ="ok";
        Database db = Database.newInstance();
        Gson gson = new Gson();
        try{
            System.out.println("++++++++++++++ vor addEntry " +jsonStringEntry);
//            System.out.println("++++++++++++++ vor addEntry " +gson.fromJson(jsonStringEntry, Entry.class));
            db.addEntry(jsonStringEntry);                //gson.fromJson(jsonStringEntry, Entry.class
        }catch(Exception e) {
            System.out.println("+++++++ error: " +e.getMessage());
            retValue = e.getMessage();
        }
        
        return retValue;
    }
    
//    @GET
//    @Produces(MediaType.APPLICATION_JSON)
//    public ArrayList<Entry> getKvEntries(@QueryParam("id_kv") String id_kv) {
//        ArrayList<Entry> listEntries;
//        try {
//            Database db = Database.newInstance();
//            listEntries = db.getKvEntries(id_kv);
//        }
//        catch (Exception ex) {
//            listEntries = new ArrayList<>();
//            listEntries.add(new Entry(new ObjectId(), null, null, 0.0, ex.getMessage(), "", false, false, new ObjectId(), new ObjectId(), new ObjectId()));
//        }
//        
//        return listEntries;
//    }
    
//    @GET
//    @Produces(MediaType.APPLICATION_JSON)
//    public ArrayList<Entry> getAvEntries(@QueryParam("id_av") String id_av) {
//        ArrayList<Entry> listEntries;
//        try {
//            Database db = Database.newInstance();
//            listEntries = db.getAvEntries(id_av);
//        }
//        catch (Exception ex) {
//            listEntries = new ArrayList<>();
//            listEntries.add(new Entry(new ObjectId(), null, null, 0.0, ex.getMessage(), "", false, false, new ObjectId(), new ObjectId(), new ObjectId()));
//        }
//        
//        return listEntries;
//    }
}
