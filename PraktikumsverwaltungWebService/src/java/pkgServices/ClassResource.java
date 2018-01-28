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
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;
import pkgData.Database;
import pkgData.Class;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Class")
public class ClassResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of ClassResource
     */
    public ClassResource() {
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<Class> getClasses() {
        ArrayList<Class> allClasses;
        Database db = Database.newInstance();
        
        try {
            allClasses = db.getAllClasses();
        } catch (Exception ex) {
            allClasses = new ArrayList<>();
            allClasses.add(new Class(ex.getMessage(), "", ""));
        }
        return allClasses;
    }
    
    @GET
    @Path("{classId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Class getCompanyById(@PathParam("classId") String id) {
        Class company = null;
         
        try{
            company = Database.newInstance().getClassById(id);
        }     
	catch(Exception ex){ 
                company = new Class(ex.getMessage(), "", "");       
            }
        return company;
    }
}
