/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

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

    
    // checks pupil and teacher
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getIsLoginOk(@QueryParam("username") String username, @QueryParam("password") String password) {
        String retVal = "false";
        Database db = Database.newInstance();
        
        retVal = db.getIsLoginOkPupil(username, password);
        
        if (retVal.endsWith("false"))
        {
            retVal = db.getIsLoginOkTeacher(username, password);
        }
        
        return retVal;
    }
}
