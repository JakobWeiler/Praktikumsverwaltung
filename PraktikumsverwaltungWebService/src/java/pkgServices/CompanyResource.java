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
import pkgData.Company;
import pkgData.Database;

/**
 * REST Web Service
 *
 * @author schueler
 */
@Path("Company")
public class CompanyResource {

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of CompanyResource
     */
    public CompanyResource() {
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<Company> getCompanies() {
        ArrayList<Company> allCompanies;
        Database db = Database.newInstance();
        
        try {
            allCompanies = db.getAllCompanies();
        } catch (Exception ex) {
            ex.printStackTrace();
            allCompanies = new ArrayList<>();
            allCompanies.add(new Company("", ex.getMessage(), "",0,""));
        }
        return allCompanies;
    }
    
    @GET
    @Path("{companyId}")
    @Produces({MediaType.APPLICATION_JSON, MediaType.TEXT_XML, MediaType.APPLICATION_XML})
    public Company getCompanyById(@PathParam("companyId") String id) {
        Company company = null;
         
        try{
            company = Database.newInstance().getCompanyById(new ObjectId(id));
        }     
	catch(Exception ex){ 
                ex.printStackTrace();
                company = new Company("",ex.getMessage(), "",0,"");       
            }
        return company;
    }
    
    @POST
    @Consumes({MediaType.TEXT_HTML, MediaType.APPLICATION_JSON, MediaType.TEXT_XML})
    public Company newCompany(Company company) throws Exception {
        Company retCompany;
        
        try {
            retCompany = Database.newInstance().addCompany(company);
        } catch (Exception ex) {
            retCompany = new Company("", ex.getMessage(), "", 0, "");
        }
        
        return retCompany;
    }
}