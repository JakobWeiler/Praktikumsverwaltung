/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgMisc;

import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.WebResource;
import com.sun.jersey.api.client.config.ClientConfig;
import com.sun.jersey.api.client.config.DefaultClientConfig;
import java.net.URI;
import java.util.Map;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.MultivaluedHashMap;
import javax.ws.rs.core.MultivaluedMap;
import javax.ws.rs.core.UriBuilder;


/**
 *
 * @author schueler
 */
public class Controller {
    private String uri;
    private ClientConfig config;
    private Client client;
    private WebResource service;
    
    public Controller(String uri) throws Exception {
        setUri(uri);
    }
    
    public Controller() {
        
    }
        
    public String login(String username, String password) throws Exception {
        MultivaluedMap<String, String> queryParams = new MultivaluedHashMap<String,String>();
        queryParams.add("username", username);
        queryParams.add("password", password);
        String strFromServer = service.path("Login")
                .queryParams(queryParams)
                .accept(MediaType.APPLICATION_JSON).get(String.class);
        
        return strFromServer;
    }
    
     public String getEntries() throws Exception {
        String strFromServer = service.path("Entry")
                .accept(MediaType.APPLICATION_JSON).get(String.class);
        
        return strFromServer;
    }
    
    /*public String getLogBooksByDriverId(String driverId) throws Exception {
        String strFromServer = service.path("LogBook/" + driverId)
                .accept(MediaType.APPLICATION_XML).get(String.class);
        
        return strFromServer;
    }
    
    public String addDriver(Driver driver) throws Exception {
        String retValue;
        
        retValue = service.path("Driver/").type(MediaType.TEXT_XML).post(String.class, driver);
        
        return retValue;
    }
    
    public String updateDriver(Driver driver) throws Exception {
        String retValue;
        
        retValue = service.path("Driver/").type(MediaType.TEXT_XML).put(String.class, driver);
        
        return retValue;
    }*/
        
    public void setUri(String uri) throws Exception {
        this.uri = uri;
        config = new DefaultClientConfig();
        
        client = Client.create(config);
        service = client.resource(getBaseURI());
    }
    
    private URI getBaseURI() {
        return UriBuilder.fromUri(uri).build();
    }
}
