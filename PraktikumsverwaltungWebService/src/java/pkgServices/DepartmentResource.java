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
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;
import org.bson.types.ObjectId;
import pkgData.Database;
import pkgData.Department;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Department")
public class DepartmentResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of DepartmentResource
     */
    public DepartmentResource() {
    }

    @GET
    @Produces({MediaType.APPLICATION_JSON, MediaType.TEXT_XML, MediaType.APPLICATION_XML})
    public ArrayList<Department> getDepartments() {
        ArrayList<Department> allDepartments;
        Database db = Database.newInstance();
        
        try {
            allDepartments = db.getAllDepartments();
        } catch (Exception ex) {
            ex.printStackTrace();
            allDepartments = new ArrayList<>();
            allDepartments.add(new Department("",ex.getMessage(), ""));
        }
        return allDepartments;
    }
    
    @GET
    @Path("{departmentid}")
    @Produces({MediaType.APPLICATION_JSON, MediaType.TEXT_XML, MediaType.APPLICATION_XML})
    public Department getDepartmentById(@PathParam("departmentid") String id) {
        Department department = null;
         
        try{
            department = Database.newInstance().getDepartmentById(new ObjectId(id));
        }     
	catch(Exception ex){ 
                ex.printStackTrace();
                department = new Department("",ex.getMessage(), "");       
            }
        return department;
    }
    
    @POST
    @Consumes({MediaType.TEXT_HTML, MediaType.APPLICATION_JSON, MediaType.TEXT_XML})
    public Department newDepartment(Department department) throws Exception {
        Department retDepartment;
        
        try {
            retDepartment = Database.newInstance().addDepartment(department);
        } catch (Exception ex) {
            retDepartment = new Department("", ex.getMessage(), "");
        }
        
        return retDepartment;
    }
}
