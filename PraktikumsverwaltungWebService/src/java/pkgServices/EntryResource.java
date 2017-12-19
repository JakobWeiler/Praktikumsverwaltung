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
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
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
    public ArrayList<Entry> getJson() {
        ArrayList listEntries;
        try {
            Database db = Database.newInstance();
            listEntries = db.getAllEntries();
        }
        catch (Exception ex) {
            listEntries = new ArrayList<>();
            listEntries.add(new Entry(new ObjectId(), null, null, 0.0, ex.getMessage(), "", false, false, new ObjectId(), new ObjectId(), new ObjectId()));
        }
        
        return listEntries;
    }
}
