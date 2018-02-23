/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import java.util.ArrayList;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
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
@Path("EntryAdmin")
public class EntryAdminResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of EntryAdminResource
     */
    public EntryAdminResource() {
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<Entry> getAllUnacceptedEntries() {
        ArrayList<Entry> listUnacceptedEntries;
        try {
            Database db = Database.newInstance();
            listUnacceptedEntries = db.getAllUnacceptedAndUnseenEntries();
        }
        catch (Exception ex) {
            listUnacceptedEntries = new ArrayList<>();
            listUnacceptedEntries.add(new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, false, "", "", "", ""));
        }
        
        return listUnacceptedEntries;
    }
}
