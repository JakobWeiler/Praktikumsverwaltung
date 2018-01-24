/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import com.google.gson.Gson;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Consumes;
import javax.ws.rs.Produces;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import pkgData.Database;
import pkgData.Person;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Login")
public class LoginResource {

    @Context
    private UriInfo context;

    
    public LoginResource() {
    }

    
    // checks pupil and teacher login
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getIsLoginOk(@QueryParam("username") String username, @QueryParam("password") String password) {
        Person p = null;
        
        try {
            Database db = Database.newInstance();
        
            p = db.getIsLoginOkPupil(username, password);

            if (p == null)
            {
                p = db.getIsLoginOkTeacher(username, password);
            }
        }
        catch (Exception ex) {
            System.out.println("error in login: " + ex.getMessage());
        }
        
        return new Gson().toJson(p, Person.class);
    }
}
