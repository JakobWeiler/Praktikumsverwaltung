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
import pkgData.Teacher;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Teacher")
public class TeacherResource {

    @Context
    private UriInfo context;

    
    public TeacherResource() {
    }
    
    @GET
    @Produces({MediaType.APPLICATION_JSON})
    public ArrayList<Teacher> getAllActivePupils() {
        ArrayList<Teacher> listPupils;
        
        try {
            Database db = Database.newInstance();
            listPupils = db.getAllActiveTeachers();
        }
        catch (Exception ex) {
            listPupils = new ArrayList<>();
            listPupils.add(new Teacher("", ex.getMessage(), "", "", "", "", false, false));
        }
        
        return listPupils;
    }
    
}
