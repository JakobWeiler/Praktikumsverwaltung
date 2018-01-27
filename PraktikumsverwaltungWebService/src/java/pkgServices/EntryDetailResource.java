/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;
import pkgData.Database;
import pkgData.Entry;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("EntryDetail")
public class EntryDetailResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of EntryDetailResource
     */
    public EntryDetailResource() {
    }
    
    @GET
    @Path("{entryId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Entry getEntry(@PathParam("entryId") String idOfEntry) {
        Entry entry = null;
         
        try{
            Database db = Database.newInstance();
            entry = db.getEntry(idOfEntry);
        }     
	catch(Exception ex) {
            entry = new Entry("", null, null, 0.0, ex.getMessage(), "", false, false, false, "", "", "");      
        }
        return entry;
    }    
}
