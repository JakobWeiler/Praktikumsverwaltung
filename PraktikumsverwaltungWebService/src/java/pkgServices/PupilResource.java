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
import javax.ws.rs.core.MediaType;
import pkgData.Database;
import pkgData.Pupil;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Pupil")
public class PupilResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of PupilResource
     */
    public PupilResource() {
    }

    /**
     * Retrieves representation of an instance of pkgServices.PupilResource
     * @return an instance of java.lang.String
     */
    @GET
    @Produces({MediaType.APPLICATION_JSON, MediaType.TEXT_XML, MediaType.APPLICATION_XML})
    public ArrayList<Pupil> getPupils() {
        Database db = Database.newInstance();
        return db.getListPupil();
    }

    @POST
    @Consumes({MediaType.TEXT_HTML, MediaType.TEXT_XML, MediaType.APPLICATION_JSON})
    public String addPupil(Pupil pupil) throws Exception {
        String retValue ="ok";
        Database db = Database.newInstance();
        
        try{
            db.addPupil(pupil);
        }catch(Exception e) {
            retValue = e.getMessage();
        }
        return retValue;
    }
    
    @PUT
    @Consumes(MediaType.APPLICATION_XML)
    public void putXml(String content) {
    }
}