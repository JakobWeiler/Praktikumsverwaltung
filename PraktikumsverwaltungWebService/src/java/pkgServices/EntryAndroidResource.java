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
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.core.MediaType;
import pkgData.Database;
import pkgData.Entry;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("EntryAndroid")
public class EntryAndroidResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of EntryAndroidResource
     */
    public EntryAndroidResource() {
    }

    /**
     * Retrieves representation of an instance of pkgServices.EntryAndroidResource
     * @return an instance of java.lang.String
     */
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
    
    @PUT
    @Consumes(MediaType.APPLICATION_XML)
    public void putXml(String content) {
    }
}
