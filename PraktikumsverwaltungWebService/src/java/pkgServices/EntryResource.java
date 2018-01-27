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
import javax.ws.rs.DELETE;
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
    public ArrayList<Entry> getAllEntries() {
        ArrayList<Entry> listEntries;
        try {
            Database db = Database.newInstance();
            listEntries = db.getAllEntries();
        }
        catch (Exception ex) {
            listEntries = new ArrayList<>();
            listEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, false, "", "", ""));
        }
        
        return listEntries;
    }
    
    @GET
    @Path("{entryId}")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<Entry> getAllOwnEntries(@PathParam("entryId") String id) {
        ArrayList<Entry> listEntries = null;
         
        try{
            Database db = Database.newInstance();
            listEntries = db.getAllOwnEntries(id);
        }     
	catch(Exception ex){ 
                listEntries = new ArrayList<>();
                listEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, false, "", "", ""));      
            }
        return listEntries;
    }
    
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    public String addEntry(String jsonStringEntry) throws Exception {
        String retValue ="ok";
        Database db = Database.newInstance();
        try{
            db.addEntry(jsonStringEntry);                //gson.fromJson(jsonStringEntry, Entry.class
        }catch(Exception e) {
            retValue = e.getMessage();
        }
        
        return retValue;
    }
    
    @PUT
    @Consumes(MediaType.APPLICATION_JSON)
    public String updateEntry(String editedEntry) throws Exception {
        String retValue = "ok";
        
        try {
            Database db = Database.newInstance();
            db.updateEntry(editedEntry);
        }
        catch (Exception ex) {
            retValue = ex.getMessage();
        }
        
        return retValue;
    }
    
    @DELETE            // !!! DELETE
    @Consumes(MediaType.APPLICATION_JSON)
    public String deleteEntry (@QueryParam("entryId") String id) throws Exception {           // STATT path nimmt man hier QueryParam. Und IMMER String für id nehmen, weil sonst bei Error zu viele Exceptions?
        Database db = Database.newInstance();
        String retValue = "ok";
        
        try {            
            db.deleteEntry(id);            
        }
        catch (Exception ex) {            
            // !!! the following exception cannot never be reached from client (@delete - bug)
            // @delete method always returns "200 OK" message
            retValue = "error: " + ex.getMessage();
        }
        return retValue;
    }
}
