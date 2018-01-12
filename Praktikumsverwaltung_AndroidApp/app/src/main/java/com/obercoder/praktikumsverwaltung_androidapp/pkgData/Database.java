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
import com.obercoder.praktikumsverwaltung_androidapp.pkgController.ControllerEntry;
import com.obercoder.praktikumsverwaltung_androidapp.pkgController.ControllerPupil;
import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.WebResource;
import com.sun.jersey.api.client.config.ClientConfig;
import com.sun.jersey.api.client.config.DefaultClientConfig;
import com.sun.jersey.multipart.impl.MultiPartWriter;

import org.bson.Document;

import java.lang.reflect.Type;
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
    public ArrayList<Entry> listEntries = new ArrayList<>();
    public ArrayList<Class> listClass = new ArrayList<>();

    private static Database db = null;
    private String uri = null;
    private ClientConfig config = null;
    private Client client = null;
    private WebResource service = null;
    private static final String URL = "http://10.0.0.178:8080/PraktikumsverwaltungWebService/resources/";
    private static final String ContentType = "application/json";
    private ControllerPupil controllerPupil;
    private ControllerEntry controllerEntry;
    private Gson gson;

    public static Database newInstance(){
        if (db == null) {
            db = new Database();
        }
        return  db;
    }

    public void addPupil (Pupil p) {
        listPupil.add(p);
    }

    public ArrayList<Pupil> getListPupil(){
        return listPupil;
    }

    public ArrayList<Entry> getListEntries(){
        return listEntries;
    }

    public ArrayList<String> getEntriesAsStrings() {
        ArrayList<String> listEntriesAsStrings = new ArrayList<>();
        for (Entry e : listEntries) {
            String entry = e.getTitle() + "\n\n" + e.getStartDate().toString() + "   -   " +
                    e.getEndDate().toString() + "\n\n" +
                    listClass.get(listClass.indexOf(new Class(e.getIdClass()))) + "     " + e.getSalary();

            listEntriesAsStrings.add(entry);
        }
        return listEntriesAsStrings;
    }

    public static String getContentType(){
        return ContentType;
    }

    public void loadPupils() throws Exception {
            controllerPupil = new ControllerPupil();

            String paras[] = new String[2];
            paras[0] = "GET";
            paras[1] = "Pupil";
            controllerPupil.execute(paras);
            final String result = controllerPupil.get();
            if(result == null){
                throw new Exception("webservice problem --getAllPupils");
            }

            Thread t = new Thread(new Runnable() {
                public void run() {
                    Type pupilListType = new TypeToken<ArrayList<Pupil>>(){}.getType();
                    gson = new Gson();
                    listPupil = gson.fromJson(result, pupilListType);
                }
            });
            t.start();
            t.join();
    }

    public void loadEntries() throws Exception{
        controllerEntry = new ControllerEntry();

        String paras[] = new String[2];
        paras[0] = "GET";
        paras[1] = "Entry";
        controllerEntry.execute(paras);
        final String result = controllerEntry.get();
        if(result == null){
            throw new Exception("webservice problem --getEntries");
        }

        Thread t = new Thread(new Runnable() {
            public void run() {
                Type entryListType = new TypeToken<ArrayList<Entry>>(){}.getType();
                gson = new Gson();
                listEntries = gson.fromJson(result, entryListType);
            }
        });
        t.start();
        t.join();
    }

    public boolean checkLogin(Pupil p) throws Exception {
        controllerPupil = new ControllerPupil();

        String paras[] = new String[5];
        paras[0] = "GET";
        paras[1] = "Login";
        paras[2] = "?username=" + p.getUsername() + "&password=" + p.getPassword();

        controllerPupil.execute(paras);
        final String result = controllerPupil.get();

        if(result == null) {
            throw new Exception ("webservice problem --checkLogin");
        }

        return Boolean.valueOf(result);
    }

    public void signUp() throws Exception {
        controllerPupil = new ControllerPupil();

        String paras[] = new String[2];
        paras[0] = "POST";
    }
    public static String getUrl(){
        return URL;
    }
}
