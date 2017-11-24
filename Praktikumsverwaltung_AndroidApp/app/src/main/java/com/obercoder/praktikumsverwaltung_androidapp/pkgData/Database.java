package com.obercoder.praktikumsverwaltung_androidapp.pkgData;


import android.util.Log;

import com.mongodb.BasicDBList;
import com.mongodb.MongoClient;
import com.mongodb.MongoClientURI;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoCursor;
import com.mongodb.client.MongoDatabase;
import com.mongodb.util.JSON;

import org.bson.Document;

import java.util.ArrayList;
import java.util.List;
import java.util.TreeSet;

/**
 * Created by Sasa on 01.10.2017.
 */

public class Database{

    private TreeSet<Pupil> tsPupil = new TreeSet<Pupil>();
    private static Database singletonDB = null;
    MongoCollection<Document> collection;

    public static Database newInstance() {
        if (singletonDB == null) {
            singletonDB = new Database();
        }
        return  singletonDB;
    }

    public void addPupil (Pupil p) {
        tsPupil.add(p);
    }

    public TreeSet<Pupil> getTsPupil (){
        return tsPupil;
    }


    //1.VERSUCH LIST zu füllen
    public List<Document> getAllCompanies(MongoCollection<Document> c) {
        FindIterable<Document> cur = c.find();
        List<Document> companies = new ArrayList<>();
        Log.d("WHILEAUSGABE2", "Test");
        for (Document d : cur) {
            Log.d("WHILEAUSGABE", d.toString());
            companies.add(d);
        }
        return companies;
    }

    public void connect() {
        MongoConnect mc = new MongoConnect();
        mc.execute();

       /* MongoClientURI mongoUri = new MongoClientURI("mongodb://192.168.196.38");
        MongoClient mongoClient = new MongoClient(mongoUri);
        Log.d("ABCDFGH", "Connected to the database successfully");
        MongoDatabase db = mongoClient.getDatabase("5BHIFS_BSD_Praktikumsverwaltung");

        collection = db.getCollection("Pupil");
        List<Document> listCompany = this.getAllCompanies();
        Log.d("SELECTCOLLECTION", "Collection myCollection selected successfully");      //1. VERSUCH mit List<Document>

        for(Document c : listCompany) {
            Log.d("LISTCOMP", c.toString());
        }

/*
        MongoCursor<Document> iterator = collection.find().iterator();

        BasicDBList list = new BasicDBList();                   //2.VERSUCH WEIL MONGOCOLLECTION IN NORMALE LISTE NICHT MÖGLICH
        while (iterator.hasNext()) {
            Document doc = iterator.next();
            list.add(doc);
        }
        System.out.println(JSON.serialize(list));
*/
    }
}
