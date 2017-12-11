package com.obercoder.praktikumsverwaltung_androidapp.pkgData;


import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.mongodb.BasicDBList;
import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoCursor;
import com.mongodb.client.MongoDatabase;
import com.mongodb.util.JSON;
import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.WebResource;
import com.sun.jersey.api.client.config.ClientConfig;
import com.sun.jersey.api.client.config.DefaultClientConfig;
import com.sun.jersey.multipart.impl.MultiPartWriter;

import org.bson.Document;

import java.net.URI;
import java.util.ArrayList;
import java.util.List;
import java.util.TreeSet;
import java.util.concurrent.ExecutionException;

import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.UriBuilder;

/**
 * Created by Sasa on 01.10.2017.
 */

public class Database{

    private ArrayList<Pupil> listPupil = new ArrayList<Pupil>();
    private static Database db = null;
    private String uri = null;
    private ClientConfig config = null;
    private Client client = null;
    private WebResource service = null;

    /*public Database(String _uri) throws Exception{
        setUri(_uri);
    }
*/
    public static Database newInstance(){
        if (db == null) {
            db = new Database();
        }
        return  db;
    }

    public void addPupil (Pupil p) {
        listPupil.add(p);
    }

    public ArrayList<Pupil> getListPupil (){
        return listPupil;
    }

    public void setUri(String uri) throws Exception {
        this.uri = uri;
        config = new DefaultClientConfig();
        config.getClasses().add(MultiPartWriter.class);

        client = Client.create(config);
        service = client.resource(getBaseURI());
    }

    public URI getBaseURI() {
        return UriBuilder.fromUri(uri).build();
    }

    public String getPupils() throws Exception{
        String strFromServer = service.path("Pupil/").accept(MediaType.APPLICATION_JSON).get(String.class);
        return strFromServer;
    }

    public void loadPupils() throws Exception {
        Gson gson = new Gson();
        listPupil = gson.fromJson(this.getPupils(), new TypeToken<ArrayList<Pupil>>(){}.getType());     //HIER ABBRUCH
        Log.d("GSONTEST", "HALLO");
    }

    public void connect() throws Exception{
        setUri("http://localhost:8080/PraktikumsverwaltungWebService/resources/");
        Log.d("TestNEW11", "Hallo");
        loadPupils();

        for(Pupil p : listPupil)
        Log.d("FINISHEDLIST", p.toString());
    }
}
