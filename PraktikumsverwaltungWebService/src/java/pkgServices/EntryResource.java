/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import java.util.ArrayList;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Consumes;
import javax.ws.rs.Produces;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import org.bson.types.ObjectId;
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
    public ArrayList<Entry> getAllEntries() {
        ArrayList<Entry> listEntries;
        try {
            Database db = Database.newInstance();
            listEntries = db.getAllEntries();
        }
        catch (Exception ex) {
            listEntries = new ArrayList<>();
            listEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, "", "", ""));
        }
        
        return listEntries;
    }
    
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    public String addPupil(Entry entry) throws Exception {
        String retValue ="ok";
        Database db = Database.newInstance();
        
        try{
            db.addEntry(entry);
        }catch(Exception e) {
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
